# MinoriEditorShell

## Contact

[![Join the chat at https://gitter.im/MinoriEditorShell/community](https://badges.gitter.im/MinoriEditorShell/community.svg)](https://gitter.im/MinoriEditorShell/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Status
[![Verification Build](https://github.com/TorisanKitsune/MinoriEditorShell/actions/workflows/build.yml/badge.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/actions/workflows/build.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=TorisanKitsune_MinoriEditorShell&metric=alert_status)](https://sonarcloud.io/dashboard?id=TorisanKitsune_MinoriEditorShell)
[![Build Coverage](https://img.shields.io/sonar/coverage/TorisanKitsune_MinoriEditorShell?server=https%3A%2F%2Fsonarcloud.io)](https://sonarcloud.io/dashboard?id=TorisanKitsune_MinoriEditorShell)
[![Downloads](https://img.shields.io/nuget/dt/MinoriEditorShell.svg)](https://www.nuget.org/packages/MinoriEditorShell/)

[![Open Issues](https://img.shields.io/github/issues-raw/TorisanKitsune/MinoriEditorShell.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/issues)
[![Closed Issues](https://img.shields.io/github/issues-closed-raw/TorisanKitsune/MinoriEditorShell.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/issues)
[![Open Pull Requests](https://img.shields.io/github/issues-pr-raw/TorisanKitsune/MinoriEditorShell.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/issues)
[![Closed Pull Requests](https://img.shields.io/github/issues-pr-closed-raw/TorisanKitsune/MinoriEditorShell.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/issues)

Dual-Licensed with either
[![Apache](https://img.shields.io/badge/license-Apache-blue.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/blob/master/LICENCE.txt) or
[![MS-PL](https://img.shields.io/badge/license-MsPL-blue.svg)](https://github.com/TorisanKitsune/MinoriEditorShell/blob/master/LICENCE.txt)

## MvvmCross 9 troubleshooting / Known Issues

* dot net v6 or greater to be compatable with dependencies.
* If view does not showing add `[MvxContentPresentation]` for MesDocuments or Main Content view .i.e. MainView.
* Add`[MvxWindowPresentation]` for windows.
* If using logging you only need `ILoggerFactory`, otherwise return null.

## Breaking Changes (For Develop pre-release

The goal is to minimize the main library into a more concise library. Thus the following changes will/have been made.
* Command interface was moved to MinoriEditorShell.Command -- This is currently un-tested and no nuget yet.
* Undo - Redo interface was moved to MinoriEditorShell.History -- This is currently un-tested and no nuget yet.

* IMesManager renamed to IMesDocumentManager to clarify what the manager does, manages documents/persistant documents and tools.
* IMesSettings is for custom setting view models in the settings manager, (Still need to have a view for them in platform target).
* IMesSettingsManger is for managing all of the settings view models. 

## Build environment

For windows this is currently being ran on Visual Studio 2019 Community edition with **.Net Core cross platform development**, and **.Net Desktop Environment**
This project depends on netstandard library for its core build. Future plans involves targeting other platforms.

## What is this

MinoriEditorShell is a IDE framework designed specifically for building multi document editor applications with MvvmCross. It builds on some excellent libraries:

* [AvalonDock](https://github.com/Dirkster99/AvalonDock)
* [MvvmCross](https://www.mvvmcross.com/)

MinoriEditorShell ships with three themes: a Blue theme(Default), a Light theme, and a Dark theme.

![Screenshot - Blue theme](https://raw.github.com/TorisanKitsune/MinoriEditorShell/develop/Images/BlueDemoApp.png)

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

Documentation can be found on the github [wiki](https://github.com/TorisanKitsune/MinoriEditorShell/wiki/)

## Acknowledgements

* Many of the original ideas, and much of the early code came from [Tim Jones](https://github.com/tgjones/), 
  creator of the [Gemini](https://github.com/tgjones/gemini/) framework. I have extended and modified 
  his code to integrate better with MvvmCore, and AvalonDock 2.0, which natively supports MVVM-style binding.

MinoriEditorShell is not the only WPF framework for building IDE-like applications. Here are some others:

* [Gemini](https://github.com/tgjones/gemini/) - Basis of this project
* [SoapBox Core](http://soapboxautomation.com/products/soapbox-core-2/) - source [here](http://svn.soapboxcore.com/svn/),
  but I think this project might be dead.
* [Wide](https://github.com/chandramouleswaran/Wide/) - looks promising, and has a 
  [CodeProject article](http://www.codeproject.com/Articles/551885/How-to-create-a-VS-2012-like-application-Wide-IDE).
* [Wider](https://github.com/TorisanKitsune/Wider) - Based on project wide with update Prism and Fluent.Ribbon


## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FTorisanKitsune%2FMinoriEditorShell.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2FTorisanKitsune%2FMinoriEditorShell?ref=badge_large)