# ButtSaber

[![Patreon donate button](https://img.shields.io/badge/patreon-donate-yellow.svg)](https://www.patreon.com/qdot)
[![Discourse Forum](https://img.shields.io/badge/discourse-forum-blue.svg)](https://metafetish.club)
[![Discord](https://img.shields.io/discord/353303527587708932.svg?logo=discord)](https://discord.buttplug.io)
[![Twitter](https://img.shields.io/twitter/follow/buttplugio.svg?style=social&logo=twitter)](https://twitter.com/buttplugio)

ButtSaber is a Beat Saber Mod for adding
[Buttplug](https://buttplug.io) haptics routing.

## Table Of Contents

- [Support The Project](#support-the-project)
- [Thanks](#thanks)
- [Installation](#installation)
- [Compiling](#compiling)
- [License](#license)

## Support The Project

If you find this project helpful, you can [support us via
Patreon](http://patreon.com/qdot)! Every donation helps us afford more
hardware to reverse, document, and write code for!

## Installation and Usage

Running ButtSaber requires being on Windows 7/10 (10 if you want to
use Bluetooth hardware) and the following software:

- [Intiface Desktop](https://github.com/intiface/intiface-desktop/releases)
- [Intiface GVR](https://github.com/intiface/intiface-game-vibration-router)
- [SharpMonoInjector](https://github.com/warbler/SharpMonoInjector) (Soon to be integrated in to the GVR)

Steps to run at the moment:

- Start Intiface Desktop, start server with IPC listening.
- Start Intiface GVR. Should connect automatically. Scan and choose
  hardware. Go to GVR - IPC Panel
- Start Beat Saber.
- Start SharpMonoInjector. Choose ButtSaber.dll, fill in class with
  "Main", function with "Load". Hit inject.
- Assuming everything worked, there should be a "Connected" status in
  the GVR - IPC Panel, and toys selected in the Intiface window will
  vibrate when controllers do.

## Compiling

The project should compile with Visual Studio 2019, provided you have
the required DLLs outside the project. The project will fetch Harmony
via NuGet, but the other required modules are:

- Assembly-Csharp.dll from your Beat Saber install
- UnityEngine.dll from your Beat Saber install
- UnityEngine.XRModule from your Beat Saber install

Note that the modules from Beat Saber will need to match the version
you have installed on your system.

## License

Buttplug is BSD 3-Clause licensed. More information is available in
the LICENSE file.
