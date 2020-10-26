# Introduction

This is an attempt to get MvvmCross and Avalonia to play nice togeather.

## Creation steps
Since we need to create our own MvvmCross.Platforms.Wpf 
to Avalonui bridge, we will prefix the bridge with ava.
This is not using the ReactUI helper since we are using
MvvmCross.

1. `dotnet new avalonia.app -o SimpleDemo.Avalonia`
1. Change the `App` class to use MesApplication from 
    `MinoriEditorShell.Platforms.Avalonuia`
    1. Add `xmlns:mes="clr-namespace:MinoriEditorShell.Platforms.Avalonia;assembly=MinoriEditorShell.Platforms.Avalonia"`
    2. Change `Application` to `mes:MesApplication`
    3. update `App.xmal.cs`

    