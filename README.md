# Phoenix Point Mod Loader Template Project
This is demo project for creating mod loaders for Phoenix Point.

Mod loaders are special kind of mods that allow discovery & loading of non-standard mods.
Mod loaders are useful for porting existing modding frameworks with their own mod base, or creating new ones.

## How Mod Loaders Work
Mod loaders are first and always loaded before other mods.
Mod loaders are .net assemblies with at least one class that inherits PhoenixPoint.Modding.ModLoader.
Mod loaders' assemblies must have unique file name and assembly name.
To have mod loader detected and loaded by the game, mod loader assembly must be placed at:
**<game-dir>/ModLoaders/<mod-loader-name>/<mod-loader-name>.dll**
Game will load the assembly, create instance for every class that inherits PhoenixPoint.Modding.ModLoader, and ask it to discover mods.
When mod has to be activated/deactivated, the mod loader will be asked to do so. Mod loader will not be asked to work with mods not discovered by him.

## Using This Project
This project is a quick demo of how to respond to game's requests to discover/load/unload mods.
If this project is used to create custom mod loader, assemlby name has to be changed to unique name (from Project Properties).

To build the project you'll need ModSDK provided found in Phoenix Point Steam Workshop Tool or in Steam version of the game.
