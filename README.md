# Quantum Hierarchy ![](https://img.shields.io/github/issues-closed-raw/QFSW/QuantumHierarchy.svg?color=51c414) ![](https://img.shields.io/github/issues-raw/QFSW/QuantumHierarchy.svg?color=c41414&style=popout)
A simple but effective tool to inspect hidden objects in loaded scenes

### Installation via Package Manager

#### Dependencies

This package requires [**QGUI**](https://github.com/QFSW/QGUI) to be installed first

#### 2019.3+

Starting with Unity 2019.3, the package manager UI has support for git packages

Click the `+` to add a new git package and add `https://github.com/QFSW/QuantumHierarchy.git` as the source

#### 2018.3 - 2019.2
To install via package manager, add the file `Packages/manifest.json` and add the following line to the `"dependencies"`
```
"com.qfsw.qh": "https://github.com/QFSW/QuantumHierarchy.git"
```
Your file should end up like this 
```
{
  "dependencies": {
    "com.qfsw.qh": "https://github.com/QFSW/QuantumHierarchy.git",
    ...
  },
}
```
