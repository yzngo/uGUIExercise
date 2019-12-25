baum2
=====

## Photoshop (psd) to Unity (uGUI) Library!

-Photoshop
<img src = "https://user-images.githubusercontent.com/961165/50334464-b9d5e680-054b-11e9-90ce-bfe14518d079.png" width = "480">

-Unity
<img src = "https://user-images.githubusercontent.com/961165/50334465-bb071380-054b-11e9-8c13-e7ce1fbd8a29.png" width = "480">

## Setup ([Video] (https://youtu.be/ugfyO0wRics))

### Photoshop

* Download [Baum.js] (https://github.com/kyubuns/Baum2/releases)
* Copy to Photoshop / Plugins directory Baum.js
    -Mac OS: Applications \ Adobe Photoshop [Photoshop_version] \ Presets \ Scripts
    -Windows 32 bit: Program Files (x86) \ Adobe \ Adobe Photoshop [Photoshop_version] \ Presets \ Scripts
    -Windows 64 bit: Program Files \ Adobe \ Adobe Photoshop [Photoshop_version] (64 Bit) \ Presets \ Scripts

### Unity

* Download & Import [baum2.unitypackage] (https://github.com/kyubuns/Baum2/blob/master/Baum2.unitypackage?raw=true)
* Please put the font used on psd in the directory where BaumFonts file is located.
* (Please import the font used on psd in the directory where "BaumFonts" file is located.)

## How to use ([Video] (https://youtu.be/2pIuC4MWT84))

### Operations on Photoshop 

* Similarly, after creating the intermediate file and throwing it under the Baum2 / Import directory, the prefab is overwritten and updated.
    * At this time, the GUID of the prefab is not changed, so there is no need to re-reference the script.

## How to make psd

### Basic

Basically 1 layer on Photoshop = 1 GameObject on Unity.
If you want to animate part of the UI, separate the layers on Photoshop.

### Text

* ** Text layer ** on Photoshop is converted as UnityEngine.UI.Text on Unity.
* Information on fonts, font sizes, colors, etc. will be set in the same way on the Unity side as much as possible.

### Button

* Groups ** with names ending in "Button" ** on Photoshop will be converted as UnityEngine.UI.Button on Unity.
* Within this group, the image layer drawn at the back is set to the clickable range (UI.Button.TargetGraphic).

### Slider

* Groups with names ending in "Slider" on Photoshop ** will be converted as UnityEngine.UI.Slider on Unity.
* Within this group, the image layer whose name is Fill will be a sliding image (UI.Slider.FillRect).

### Scrollbar

* Groups ** with names ending in "Scrollbar" on Photoshop ** will be converted as UnityEngine.UI.Scrollbar on Unity.
* Within this group, the image layer whose name is Handle will be a handle (UI.Scrollbar.HandleRect) that slides.

### List

* ** Groups whose names end in "List" on Photoshop ** will be converted as Baum2.List on Unity.
* In this group, Item group and Mask layer are required.
    * An element in the Item group becomes one item in the list.
    * The Mask layer is the mask for the list.
* See sample for details.

### Pivot

* Only available for groups directly under the root of Photoshop.
* You can specify Pivot after the name, such as * @ Pivot = TopRight *.

### Comment layer

You can create a layer that is not output by adding # to the beginning of the layer name.

## Developed by

* Unity: Unity2017, Unity2018, Unity2019
* PhotoshopScript: Adobe Photoshop CC 2018, Adobe Photoshop CC 2019