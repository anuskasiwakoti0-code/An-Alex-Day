using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SceneAmbience : MonoBehaviour
{
    [Header("Volume Reference")]
    public Volume globalVolume;

    [Header("Mood Settings")]
    public SceneMood sceneMood = SceneMood.Neutral;

    public enum SceneMood
    {
        Neutral,
        Warm,
        Dark,
        Cozy
    }

    private Bloom bloom;
    private Vignette vignette;
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        if (globalVolume == null)
        {
            Debug.LogWarning("Global Volume not assigned!");
            return;
        }

        globalVolume.profile.TryGet(out bloom);
        globalVolume.profile.TryGet(out vignette);
        globalVolume.profile.TryGet(out colorAdjustments);

        ApplyMood();
    }

    private void ApplyMood()
    {
        switch (sceneMood)
        {
            case SceneMood.Neutral:
                SetPostProcessing(0.3f, 0.2f, 0f, 0f);
                break;
            case SceneMood.Warm:
                SetPostProcessing(0.5f, 0.25f, 10f, 5f);
                break;
            case SceneMood.Dark:
                SetPostProcessing(0.8f, 0.5f, -20f, 15f);
                break;
            case SceneMood.Cozy:
                SetPostProcessing(0.6f, 0.2f, 15f, 10f);
                break;
        }
    }

    private void SetPostProcessing(float bloomVal, float vignetteVal,
                                    float saturationVal, float contrastVal)
    {
        if (bloom != null)
            bloom.intensity.value = bloomVal;
        if (vignette != null)
            vignette.intensity.value = vignetteVal;
        if (colorAdjustments != null)
        {
            colorAdjustments.saturation.value = saturationVal;
            colorAdjustments.contrast.value = contrastVal;
        }
    }

    // Call this to change mood during gameplay
    public void ChangeMood(SceneMood newMood)
    {
        sceneMood = newMood;
        ApplyMood();
    }
}