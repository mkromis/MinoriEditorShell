# MinoriEditorStudio

## Contact

 [![Join the chat at https://gitter.im/MinoriEditorShell/community](https://badges.gitter.im/MinoriEditorShell/community.svg)](https://gitter.im/MinoriEditorShell/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Status

[![Build status](https://dev.azure.com/TorisanKitsune/MinoriEditorShell/_apis/build/status/MinoriEditorShell)](https://dev.azure.com/TorisanKitsune/MinoriEditorShell/_build/latest?definitionId=3)
[![NuGet](https://img.shields.io/nuget/v/MinoriEditorShell.svg)](https://www.nuget.org/packages/MinoriEditorShell/)
[![Downloads](https://img.shields.io/nuget/dt/MinoriEditorShell.svg)](https://www.nuget.org/packages/MinoriEditorShell/)

[![Open Issues](https://img.shields.io/github/issues-raw/TorisanKitsune/MinoriEditorShell.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/issues)
[![Closed Issues](https://img.shields.io/github/issues-closed-raw/TorisanKitsune/MinoriEditorShell.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/issues)
[![Open Pull Requests](https://img.shields.io/github/issues-pr-raw/TorisanKitsune/MinoriEditorShell.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/issues)
[![Closed Pull Requests](https://img.shields.io/github/issues-pr-closed-raw/TorisanKitsune/MinoriEditorShell.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/issues)

Dual-Licensed with either
[![Apache](https://img.shields.io/badge/license-Apache-blue.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/blob/master/LICENCE.txt) or
[![MS-PL](https://img.shields.io/badge/license-MsPL-blue.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/blob/master/LICENCE.txt)

## What is this

MinoriEditorShell is a IDE framework designed specifically for building multi document editor applications with MvvmCross. It builds on some excellent libraries:

* [AvalonDock](https://github.com/Dirkster99/AvalonDock)
* [MvvmCross](https://www.mvvmcross.com/)

MinoriEditorStudio ships with three themes: a Blue theme(Default), a Light theme, and a Dark theme.

![Screenshot - Blue theme](https://raw.github.com/TorisanKitsune/MinoriEditorShell/master/Images/BlueDemoApp.png)

## Modules used

* [MinoriEditorShell](http://nuget.org/packages/MinoriEditorShell/)
* [MinoriEditorShell.Ribbon](http://nuget.org/packages/MinoriEditorShell.Ribbon/)
* [MinoriEditorShell.VirtualCanvas](http://nuget.org/packages/MinoriEditorShell.VirtualCanvas/)

## Continuous builds

We use DevOps to build MinoriEditorShell after every pull request to the master branch.

## What does it do

MinoriEditorShell allows you to build your WPF application by composing separate modules. This provides a nice
way of separating out the code for each part of your application.

## More Documentation

Doumentation can be found on the github [wiki](https://github.com/TorisanKitsune/MinoriEditorShell/wiki/)

## Acknowledgements

* Many of the original ideas, and much of the early code came from [Tim Jones](https://github.com/tgjones/), 
  creator of the [Gemini](https://github.com/tgjones/gemini/) framework. I have extended and modified 
  his code to integrate better with MvvmCore, and AvalonDock 2.0, which natively supports MVVM-style binding.

MinoriEditorStudio is not the only WPF framework for building IDE-like applications. Here are some others:

* [Gemini](https://github.com/tgjones/gemini/) - Basis of this project
* [SoapBox Core](http://soapboxautomation.com/products/soapbox-core-2/) - source [here](http://svn.soapboxcore.com/svn/),
  but I think this project might be dead.
* [Wide](https://github.com/chandramouleswaran/Wide/) - looks promising, and has a 
  [CodeProject article](http://www.codeproject.com/Articles/551885/How-to-create-a-VS-2012-like-application-Wide-IDE).
* [Wider](https://github.com/TorisanKitsune/Wider) - Based on project wide with update Prism and Fluent.Ribbon
