using Duckov.Utilities;
using HarmonyLib;
using UnityEngine;

namespace BiggerCritTextSize
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        Harmony _h;

        void OnEnable() {
            if (_h == null) _h = new Harmony("biggercrittextsize");
            _h.PatchAll();
            Debug.Log("[BCTS] patched");
        }

        void OnDisable() {
            _h?.UnpatchAll("biggercrittextsize");
            Debug.Log("[BCTS] unpatched");
        }
    }

    [HarmonyPatch(typeof(GameplayDataSettings.UIStyleData),
                  nameof(GameplayDataSettings.UIStyleData.GetElementDamagePopTextLook))]
    static class Patch_UIStyleData_GetElementDamagePopTextLook
    {
        static void Postfix(ref GameplayDataSettings.UIStyleData.DisplayElementDamagePopTextLook __result)
        {
            var src = __result;
            __result = new GameplayDataSettings.UIStyleData.DisplayElementDamagePopTextLook {
                elementType = src.elementType,
                normalSize  = src.normalSize,
                critSize    = src.critSize   * 2f,
                color       = src.color
            };
        }
    }
}