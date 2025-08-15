using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using System;
using System.Reflection;

[BepInPlugin("rosa.ou2.speedtweaks", "OU2 Speed Tweaks", "1.0.0")]
public class SpeedTweaks : BaseUnityPlugin
{
    public static ConfigEntry<float> SlowMultiplier;

    private void Awake()
    {
        SlowMultiplier = Config.Bind("Speeds", "Slow", 0.5f, "Time scale when Slow is selected");
        new Harmony("rosa.ou2.speedtweaks").PatchAll(typeof(SpeedTweaks).Assembly);
        Logger.LogInfo($"SpeedTweaks loaded. Slow={SlowMultiplier.Value}");
    }

    [HarmonyPatch]
    class SetSpeedPatch
    {
        static MethodBase TargetMethod()
        {
            var gmType = AccessTools.TypeByName("OU2.Gameplay.GameplayManager");
            var speedTyp = AccessTools.TypeByName("OU2.Engine.GameSpeed");
            return AccessTools.Method(gmType, "SetSpeed", new Type[] { speedTyp });
        }

        static void Postfix(object speed)
        {
            // boxed enum → compare by name
            if (speed != null && speed.ToString() == "Slow")
                Time.timeScale = SlowMultiplier.Value;
        }
    }
}
internal static class SpeedState
{
    public static bool IsSlow;
}

[HarmonyPatch]
class SetSpeedPatch
{
    static MethodBase TargetMethod()
    {
        var gmType = AccessTools.TypeByName("OU2.Gameplay.GameplayManager");
        var speedTyp = AccessTools.TypeByName("OU2.Engine.GameSpeed");
        return AccessTools.Method(gmType, "SetSpeed", new Type[] { speedTyp });
    }

    static void Postfix(object speed)
    {
        SpeedState.IsSlow = (speed != null && speed.ToString() == "Slow");
        if (SpeedState.IsSlow)
            Time.timeScale = SpeedTweaks.SlowMultiplier.Value;
    }
}

[HarmonyPatch(typeof(Time), nameof(Time.timeScale), MethodType.Setter)]
static class TimeScaleSetterPatch
{
    static void Prefix(ref float value)
    {
        // Don’t break pause (value==0). Only override positive values.
        if (SpeedState.IsSlow && value > 0f)
            value = SpeedTweaks.SlowMultiplier.Value;
    }

}
