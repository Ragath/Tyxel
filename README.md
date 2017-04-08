# Tyxel
Pyxel->Tiled Converter. Observes changes in *.pyxel file and (re-)generates a Tiled-tilesheet.  
Built ontop of [PyxelParser.Net](https://github.com/Ragath/PyxelParser.Net/) Sample project included in Sample.json and Sample.pyxel.

![tyxel](https://cloud.githubusercontent.com/assets/1191717/22309491/befe966c-e34b-11e6-8636-240b0d4a3001.png)

## Instructions
1. Create a `<name>.pyxel` file in Pyxel edit
2. Create a representation of your sheet in the canvas on a layer called `Sheet` (Tyxel will look for this layer and create Tiled's sheet from it)
3. Create a `<name>.json` project file (I recommend copying Sample.json and editing it)
4. Open `<name>.json` using Tyxel.exe to begin observing changes in `<name>.pyxel`
