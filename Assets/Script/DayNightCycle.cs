using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    [Header("Sun Light")]
    public Light directionalLight;

    [Header("Time Settings")]
    public float currentHour = 8f;

    [Header("Light Colors")]
    public Color morningColor = new Color(1f, 0.95f, 0.8f);
    public Color afternoonColor = new Color(1f, 1f, 1f);
    public Color eveningColor = new Color(1f, 0.7f, 0.4f);
    public Color nightColor = new Color(0.1f, 0.1f, 0.3f);

    [Header("Ambient Colors")]
    public Color morningAmbient = new Color(0.4f, 0.4f, 0.5f);
    public Color afternoonAmbient = new Color(0.5f, 0.5f, 0.5f);
    public Color eveningAmbient = new Color(0.3f, 0.2f, 0.2f);
    public Color nightAmbient = new Color(0.05f, 0.05f, 0.1f);

    [Header("Fog Settings")]
    public bool useFog = true;
    public Color morningFog = new Color(0.8f, 0.8f, 0.9f);
    public Color afternoonFog = new Color(0.9f, 0.9f, 1f);
    public Color eveningFog = new Color(0.6f, 0.4f, 0.3f);
    public Color nightFog = new Color(0.02f, 0.02f, 0.05f);

    private LightController lightController;

    private void Start()
    {
        lightController = FindFirstObjectByType<LightController>();
        RenderSettings.fog = useFog;
        RenderSettings.fogMode = FogMode.ExponentialSquared;
        RenderSettings.fogDensity = 0.01f;
        UpdateLighting();
    }

    public void AdvanceTime(float hours)
    {
        currentHour += hours;
        Debug.Log("Time advanced to: " + currentHour + ":00");
        UpdateLighting();
    }

    public void SetNewDawn()
    {
        currentHour = 6f;
        directionalLight.color = new Color(1f, 0.8f, 0.6f);
        directionalLight.intensity = 0.6f;
        RenderSettings.ambientLight = morningAmbient;
        RenderSettings.fogColor = morningFog;
        SetRoomLights(false);
        Debug.Log("New dawn — a fresh start!");
    }

    private void UpdateLighting()
    {
        float sunAngle = (currentHour / 24f) * 360f - 90f;
        directionalLight.transform.rotation =
            Quaternion.Euler(sunAngle, 170f, 0f);

        if (currentHour < 10f)
        {
            // Morning
            directionalLight.color = morningColor;
            directionalLight.intensity = 0.8f;
            RenderSettings.ambientLight = morningAmbient;
            RenderSettings.fogColor = morningFog;
            RenderSettings.fogDensity = 0.01f;
            SetRoomLights(false);
            Debug.Log("Morning lighting applied");
        }
        else if (currentHour < 15f)
        {
            // Afternoon
            directionalLight.color = afternoonColor;
            directionalLight.intensity = 1f;
            RenderSettings.ambientLight = afternoonAmbient;
            RenderSettings.fogColor = afternoonFog;
            RenderSettings.fogDensity = 0.008f;
            SetRoomLights(false);
            Debug.Log("Afternoon lighting applied");
        }
        else if (currentHour < 19f)
        {
            // Evening
            directionalLight.color = eveningColor;
            directionalLight.intensity = 0.5f;
            RenderSettings.ambientLight = eveningAmbient;
            RenderSettings.fogColor = eveningFog;
            RenderSettings.fogDensity = 0.02f;
            SetRoomLights(false);
            Debug.Log("Evening lighting applied");
        }
        else if (currentHour < 22f)
        {
            // Late evening
            directionalLight.color = nightColor;
            directionalLight.intensity = 0.2f;
            RenderSettings.ambientLight = nightAmbient;
            RenderSettings.fogColor = nightFog;
            RenderSettings.fogDensity = 0.03f;
            SetRoomLights(true);
            Debug.Log("Late evening lighting applied");
        }
        else
        {
            // Night
            directionalLight.color = nightColor;
            directionalLight.intensity = 0.05f;
            RenderSettings.ambientLight = nightAmbient;
            RenderSettings.fogColor = nightFog;
            RenderSettings.fogDensity = 0.05f;
            SetRoomLights(true);
            Debug.Log("Night lighting applied");
        }
    }

    private void SetRoomLights(bool on)
    {
        if (lightController != null)
        {
            if (on) lightController.TurnOnLights();
            else lightController.TurnOffLights();
        }
        else
        {
            Debug.LogWarning("LightController not found!");
        }
    }
}