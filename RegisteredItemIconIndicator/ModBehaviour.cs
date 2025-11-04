using Duckov.Utilities;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace RegisteredItemIconIndicator
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        static Harmony _h;
        static bool _patched;

        protected override void OnAfterSetup()
        {
            var id = $"RegisteredItemIconIndicator.{info.name}";
            if (_h == null) _h = new Harmony(id);
            if (_patched) return;

            _h.PatchAll();
            _patched = true;
            Debug.Log("[RegisteredItemIconIndicator] patched via OnAfterSetup");
        }

        protected override void OnBeforeDeactivate()
        {
            if (_h == null || !_patched) return;
            _h.UnpatchAll(_h.Id);
            _patched = false;
            Debug.Log("[RegisteredItemIconIndicator] unpatched via OnBeforeDeactivate");
        }
    }

    [HarmonyPatch(typeof(Duckov.UI.ItemDisplay), "Setup")]
    static class Patch_ItemDisplay_Setup
    {
        const string DotName = "RegisteredLabel_TestDot_TopLeft";

        static Image GetIcon(object inst)
        {
            var fIcon = AccessTools.Field(typeof(Duckov.UI.ItemDisplay), "icon");
            return fIcon?.GetValue(inst) as Image;
        }

        // runs before ItemDisplay.Setup executes
        static void Prefix(object __instance)
        {
            var icon = GetIcon(__instance);
            if (icon == null) return;

            var existing = icon.transform.Find(DotName);
            if (existing != null)
                Object.Destroy(existing.gameObject);
        }

        // runs after ItemDisplay.Setup executes
        static void Postfix(object __instance, ItemStatsSystem.Item target)
        {
            if (target == null || !target.IsRegistered()) return;

            var icon = GetIcon(__instance);
            if (icon == null) return;

            var dotGO = new GameObject(DotName);
            dotGO.transform.SetParent(icon.transform, false);
            var dot = dotGO.AddComponent<Image>();
            dot.raycastTarget = false;
            dot.maskable = true;
            dot.color = Color.green;

            var rt = dot.rectTransform;
            rt.anchorMin = new Vector2(0f, 1f);
            rt.anchorMax = new Vector2(0f, 1f);
            rt.pivot = new Vector2(0f, 1f);
            rt.sizeDelta = new Vector2(10f, 10f);
            rt.anchoredPosition = new Vector2(5f, -5f);
        }
    }
}