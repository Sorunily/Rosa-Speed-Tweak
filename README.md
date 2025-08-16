# OU2 Speed-Tweak
This OU2 mod makes it so the slowest speed is a bit slower. Exact speed can be changed in the config.

## Requirements
- BepInEx 5 x64 (Mono). Download from the official BepInEx releases. https://github.com/bepinex/bepinex/releases

## Install
1. Extract the BepInEx zip into the game folder (next to the exe). Run the game once.
2. Extract **this mod’s zip** into the plugins location so that:
   - BepInEx/plugins/SpeedTweaks/SpeedTweaks.dll exists
3. Run the game. Check `BepInEx/LogOutput.log` for “OU2 Speed Tweaks loaded…”. After the first launch with the plugin installed you can find the created config in BepInEx/config/rosa.ou2.speedtweaks.cfg if you want to change the speed value. Vanilla value is 1. Default for the mod is 0.5.

## Configure
Edit `BepInEx/config/ou2.speedtweaks.cfg`:
