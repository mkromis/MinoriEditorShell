namespace MinoriEditorShell.Services
{
    public class MesEditorFileType
    {
        public string Name { get; set; }
        public string FileExtension { get; set; }

        public MesEditorFileType(string name, string fileExtension)
        {
            Name = name;
            FileExtension = fileExtension;
        }

        public MesEditorFileType()
        {
            
        }
    }
}
