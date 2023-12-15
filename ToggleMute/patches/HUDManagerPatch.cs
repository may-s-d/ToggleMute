using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace ToggleMute.Patches;

[HarmonyPatch(typeof(HUDManager))]
internal class HUDManagerPatch {
    [HarmonyPatch("Awake")]
    [HarmonyPostfix]
    public static void HUDManagerPatch_Awake(HUDManager __instance) {
        ToggleMute.Instance.SpeakingGameObject = createGameObjectFromAssetName("SpeakingSymbol");
        ToggleMute.Instance.MutedGameObject = createGameObjectFromAssetName("SpeakingSymbolXed");

        ToggleMute.Instance.SpeakingGameObject.SetActive(IngamePlayerSettings.Instance.settings.micEnabled);
        ToggleMute.Instance.MutedGameObject.SetActive(!IngamePlayerSettings.Instance.settings.micEnabled);
    }

    private static GameObject createGameObjectFromAssetName(string assetName) {
        var gameObject = new GameObject(assetName);
        RectTransform imageTransform = gameObject.AddComponent<RectTransform>();

        imageTransform.parent = GameObject.Find("IngamePlayerHUD").transform;
        imageTransform.localScale = Vector2.one;
        imageTransform.anchoredPosition = Vector2.zero;
        imageTransform.localPosition = new Vector2(-420f, -230f);
        imageTransform.sizeDelta = new Vector2(48f, 48f);


        var texture2D = Assets.MainAssetBundle.LoadAsset<Texture2D>(assetName);
        var sprite = Sprite.Create(texture2D, new Rect(0f, 0f, 256f, 256f), new Vector2(0f, 0f));
        Image image = gameObject.AddComponent<Image>();
        image.sprite = sprite;
        return gameObject;
    }
}