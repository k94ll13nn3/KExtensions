# KExtensions

[![Build status](https://ci.appveyor.com/api/projects/status/vd8jqd3xwxsvsw3j?svg=true)](https://ci.appveyor.com/project/k94ll13nn3/kextensions)
[![Release](https://img.shields.io/github/release/k94ll13nn3/KExtensions.svg)](https://github.com/k94ll13nn3/KExtensions/releases/latest)

Personal library with extension methods, custom controls, attached properties, ...

## KGrid

The `KGrid` class contains two attached properties for the `Grid` class :

- `Columns`
- `Rows`

These two properties can be used to create rows and columns definition in only one line.

### Usage

These two properties take a string of the following format as value : a list of valid width/height values (`100`, `auto`, `2*`, `20px`, ...) separated by a `,` or a space.

The list of valid width/height values can be found [here](https://msdn.microsoft.com/fr-fr/library/system.windows.gridlength(v=vs.110).aspx)

In addition to this list, the `!` value can be used in place of `auto` in order to have a more concise way of writing when multiple `auto` are present.

### Example

Possible value for the properties :

- `50,2*,3*`
- `auto,2px,3*`
- `*,*,50,90,9in,auto`
- `*,!,5,!`

The following xaml

```xaml
<Window x:Class="Example.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:kgrid="clr-namespace:KExtensions.Core;assembly=KExtensions.Core"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d">
    <Grid ShowGridLines="True"
          kgrid:KGrid.Columns="50,2*,3*"
          kgrid:KGrid.Rows="*,200,3*" />
</Window>
```

will produce

![example-kgrid.png](example-kgrid.png)