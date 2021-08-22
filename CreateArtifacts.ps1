# Get list of all nupkgs
$nupkgs = Get-ChildItem -Path .\Modules -Filter *.nupkg -Recurse

$basedir = 'MinoriEditorSystem-' + $env:GitVersion_NuGetVersion

# Move each item into artifacts
rm Artifacts -Recurse -Force
mkdir Artifacts\$basedir\Nugets

# Copy nugets to nuget folder
foreach ($nupkg in $nupkgs) {
    $leaf = Split-Path $nupkg -Leaf
    $outFile = "Artifacts\$basedir\Nugets\$leaf"
    echo $nupkg.FullName ' -> ' $outFile
    Copy-Item $nupkg.FullName $outFile
}

# Copy Demo Folder
#mkdir Artifacts\$basedir\Demos
cp Demos\SimpleDemo\SimpleDemo.WPF\bin\Release\net5.0-windows Artifacts\$basedir\Demos\SimpleDemo.WPF -Recurse
cp Demos\SimpleDemo\SimpleDemo.RibbonWPF\bin\Release\net5.0-windows Artifacts\$basedir\Demos\SimpleDemo.RibbonWPF -Recurse
cp Demos\SimpleDemo\SimpleDemo.Avalonia\bin\Release\net5.0 Artifacts\$basedir\Demos\SimpleDemo.Avalonia -Recurse
cp Demos\MinoriDemo\MinoriDemo.WPF\bin\Release\net5.0-windows Artifacts\$basedir\Demos\MinoriDemo.WPF -Recurse
cp Demos\MinoriDemo\MinoriDemo.RibbonWPF\bin\Release\net5.0-windows Artifacts\$basedir\Demos\MinoriDemo.RibbonWPF -Recurse

# Compress folder into 7z file
cd Artifacts
7z a "$basedir.7z" $basedir
cd ..