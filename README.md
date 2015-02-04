# **DotNet Renamer** #

# Description

DNR is a open source obfuscator/Renamer which use Dnlib library for .NET applications !
This project is based on the [DotNet Patcher](http://3dotdevcoder.blogspot.fr/2014/04/dotnet-patcher.html)'s renamer library 

You can use these 2 libraries separately without the DotNetRenamer GUI

1. **Core20Reader** : a simple PE DotNet Parser/Reader
2. **The renamer based on** : Core20Reader.dll, DotNetRenamer.Helper.dll, DotNetRenamer.Implementer.dll

# Screenshot

![DotNetRenamer.png](http://www.imabox.fr/15/01/181737mBm9m549.jpg)

![tmp4F24.png](http://www.imabox.fr/a4/1420101313V37mIj53.png)


# Features

* Doesn't support WPF .exe !
* English UI language only
* Drag & Drop 
* Displays selected .exe informations (assembly name, Version, TargetRuntime, TargetCPU, SubSystemType)
* Selecting presets : Full, Medium, Customize
* Selecting output protected file path
* Selecting encoding chars type : Alphabetic, Dots, Invisible, Chinese, Japanese
* Renaming : Namespaces, Types, Methods, Properties, Fields, Custom Attributes, Events, Parameters, Resources content ..... 
* Displays number of renamed members
* Managing exclusion rules (exclusion by types and entities)
* Command line usage with 3 presets : Full, Medium, Customize (Drag&Drop, cmd or .bat file)
* Retrieve a renamer scheme from the DotNetRenamer.ini file


# Prerequisites

* Compatible VS2013 EDI
* All Windows OS
* DotNet Framework 4.0
* The binary doesn't require installation


# WebSite

* [http://3dotdevcoder.blogspot.fr/](http://3dotdevcoder.blogspot.fr/)


# Credits

* 0xd4d : for [dnlib](https://github.com/0xd4d/dnlib) library
* Xertz : for his [Login GDI+ theme](http://xertzproductions.weebly.com/login-gdi-theme.html) which I modified a little bit
* [Motuslechat](https://bitbucket.org/motuslechat) : for improvement ideas and pullrequests


# Copyright

Copyright © 3DotDev 2008-2015


# Licence

[MIT/X11](http://en.wikipedia.org/wiki/MIT_License)