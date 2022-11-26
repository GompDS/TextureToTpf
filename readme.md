# TextureToTpf
Converts a DDS file into a TPF file. Only built to support PC TPFs.

## Create single-texture TPF
- Drag and drop a DDS file onto the executable.

## Create multi-texture TPF
- Create a folder with the name you want for your TPF. Place all DDS files you want included in the TPF into this folder.
- Drag and drop the folder onto the executable.

## Settings
After dragging and dropping your file or folder onto the executable, you will be prompted to choose which type of compression to use. Choose the type which matches the game you are working with.

You will be asked if you want to turn your TPF into a DCX file as well.

If multiple files and or folders are dragged and dropped onto the executable the settings you choose apply to all of those files and or folders.

## Known Issues
- Formats for dds files may be incorrect in certain cases, but this shouldn't be a major issue. If you find any problems related to this let me know.
- Menu textures for dark souls 3 seem to all use format 0x66, but this program will give them 0x00 instead. I'm not sure what a fix for this could be, so just be aware of that.
- Format settings for ds1/ds2 are untested and are using the same settings as Dark Souls 3 currently.

## Special Thanks
- TKGP/JKAnderson: SoulsFormats
- Rayan: Gave feedback which helped me improve the program.
