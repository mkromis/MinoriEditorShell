using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using MinoriEditorStudio.Services;

namespace MinoriEditorStudio.Modules.Services
{
    [Export(typeof(ILayoutItemStatePersister))]
    public class LayoutItemStatePersister : ILayoutItemStatePersister
    {
        private static readonly Type LayoutBaseType = typeof(ILayoutItem);

        public Boolean SaveState(IManager shell, IManagerView shellView, String fileName)
        {
            try
            {
                using (BinaryWriter writer = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                {
                    IEnumerable<ILayoutItem> itemStates = shell.Documents.Concat(shell.Tools.Cast<ILayoutItem>());

                    Int32 itemCount = 0;
                    // reserve some space for items count, it'll be updated later
                    writer.Write(itemCount);

                    foreach (ILayoutItem item in itemStates)
                    {
                        if (!item.ShouldReopenOnStart)
                        {
                            continue;
                        }

                        Type itemType = item.GetType();
                        List<ExportAttribute> exportAttributes = itemType
                                .GetCustomAttributes(typeof(ExportAttribute), false)
                                .Cast<ExportAttribute>().ToList();

                        // get exports with explicit types or names that inherit from ILayoutItem
                        List<Type> exportTypes = new List<Type>();
                        Boolean foundExportContract = false;
                        foreach (ExportAttribute att in exportAttributes)
                        {
                            // select the contract type if it is of type ILayoutitem.
                            Type type = att.ContractType;
                            if (LayoutBaseType.IsAssignableFrom(type))
                            {
                                exportTypes.Add(type);
                                foundExportContract = true;
                                continue;
                            }

                            // select the contract name if it is of type ILayoutItem.
                            type = GetTypeFromContractNameAsILayoutItem(att);
                            if (LayoutBaseType.IsAssignableFrom(type))
                            {
                                exportTypes.Add(type);
                                foundExportContract = true;
                            }
                        }
                        // select the viewmodel type if it is of type ILayoutItem.
                        if (!foundExportContract && LayoutBaseType.IsAssignableFrom(itemType))
                        {
                            exportTypes.Add(itemType);
                        }

                        // throw exceptions here, instead of failing silently. These are design time errors.
                        Type firstExport = exportTypes.FirstOrDefault();
                        if (firstExport == null)
                        {
                            throw new InvalidOperationException(String.Format(
                                "A ViewModel that participates in LayoutItem.ShouldReopenOnStart must be decorated with an ExportAttribute who's ContractType that inherits from ILayoutItem, infringing type is {0}.", itemType));
                        }

                        if (exportTypes.Count > 1)
                        {
                            throw new InvalidOperationException(String.Format(
                                "A ViewModel that participates in LayoutItem.ShouldReopenOnStart can't be decorated with more than one ExportAttribute which inherits from ILayoutItem. infringing type is {0}.", itemType));
                        }

                        String selectedTypeName = firstExport.AssemblyQualifiedName;

                        if (String.IsNullOrEmpty(selectedTypeName))
                        {
                            throw new InvalidOperationException(String.Format(
                                "Could not retrieve the assembly qualified type name for {0}, most likely because the type is generic.", firstExport));
                        }
                        // TODO: it is possible to save generic types. It requires that every generic parameter is saved, along with its position in the generic tree... A lot of work.

                        writer.Write(selectedTypeName);
                        writer.Write(item.ContentId);

                        // Here's the tricky part. Because some items might fail to save their state, or they might be removed (a plug-in assembly deleted and etc.)
                        // we need to save the item's state size to be able to skip the data during deserialization.
                        // Save current stream position. We'll need it later.
                        Int64 stateSizePosition = writer.BaseStream.Position;

                        // Reserve some space for item state size
                        writer.Write(0L);

                        Int64 stateSize;

                        try
                        {
                            Int64 stateStartPosition = writer.BaseStream.Position;
                            item.SaveState(writer);
                            stateSize = writer.BaseStream.Position - stateStartPosition;
                        }
                        catch
                        {
                            stateSize = 0;
                        }

                        // Go back to the position before item's state and write the actual value.
                        writer.BaseStream.Seek(stateSizePosition, SeekOrigin.Begin);
                        writer.Write(stateSize);

                        if (stateSize > 0)
                        {
                            // Got to the end of the stream
                            writer.BaseStream.Seek(0, SeekOrigin.End);
                        }

                        itemCount++;
                    }

                    writer.BaseStream.Seek(0, SeekOrigin.Begin);
                    writer.Write(itemCount);
                    writer.BaseStream.Seek(0, SeekOrigin.End);

                    shellView.SaveLayout(writer.BaseStream);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private static Type GetTypeFromContractNameAsILayoutItem(ExportAttribute attribute)
        {
            String typeName;
            if ((typeName = attribute.ContractName) == null)
            {
                return null;
            }

            Type type = Type.GetType(typeName);
            return typeof(ILayoutItem).IsAssignableFrom(type) ? type : null;
        }

        public Boolean LoadState(IManager shell, IManagerView shellView, String fileName)
        {
            Dictionary<String, ILayoutItem> layoutItems = new Dictionary<String, ILayoutItem>();

            if (!File.Exists(fileName))
            {
                return false;
            }

            try
            {
                using (BinaryReader reader = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read)))
                {
                    Int32 count = reader.ReadInt32();

                    for (Int32 i = 0; i < count; i++)
                    {
                        String typeName = reader.ReadString();
                        String contentId = reader.ReadString();
                        Int64 stateEndPosition = reader.ReadInt64();
                        stateEndPosition += reader.BaseStream.Position;

                        Type contentType = Type.GetType(typeName);
                        Boolean skipStateData = true;

#warning LoadState
#if false
                        if (contentType != null)
                        {
                            var contentInstance = IoC.GetInstance(contentType, null) as ILayoutItem;

                            if (contentInstance != null)
                            {
                                layoutItems.Add(contentId, contentInstance);

                                try
                                {
                                    contentInstance.LoadState(reader);
                                    skipStateData = false;
                                }
                                catch
                                {
                                    skipStateData = true;
                                }
                            }
                        }
#endif

                        // Skip state data block if we couldn't read it.
                        if (skipStateData)
                        {
                            reader.BaseStream.Seek(stateEndPosition, SeekOrigin.Begin);
                        }
                    }

                    shellView.LoadLayout(reader.BaseStream, shell.ShowTool, shell.OpenDocument, layoutItems);
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
