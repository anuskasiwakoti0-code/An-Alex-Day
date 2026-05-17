using UnityEngine;

public class LightSetup : MonoBehaviour
{
    private void Start()
    {
        SetupAllLights();
    }

    public void SetupAllLights()
    {
        Light deskLamp = CreateLight(
            "DeskLamp",
            new Vector3(-2.813f, 1.052f, 4.524f),
            LightType.Spot,
            new Color(1f, 0.96f, 0.75f),
            2f, 3f, 60f
        );

        Light bedsideLamp = CreateLight(
            "BedsideLamp",
            new Vector3(4.699f, 0.85f, -0.505f),
            LightType.Point,
            new Color(1f, 0.96f, 0.75f),
            1.5f, 2.5f, 0f
        );

        Light roomLight = CreateLight(
            "RoomLight",
            new Vector3(-0.51f, 3.12f, 0.309f),
            LightType.Point,
            new Color(1f, 0.98f, 0.9f),
            3f, 8f, 0f
        );

        Light floorLamp = CreateLight(
            "FloorLamp",
            new Vector3(-4.863f, 1.5f, 1.995f),
            LightType.Point,
            new Color(1f, 0.96f, 0.75f),
            2f, 4f, 0f
        );

        LightController lc = FindFirstObjectByType<LightController>();
        if (lc != null)
        {
            lc.deskLamp = deskLamp;
            lc.bedsideLamp = bedsideLamp;
            lc.ceilingBulb = roomLight;
            lc.floorLamp = floorLamp;
            Debug.Log("LightController wired successfully!");
        }
        else
        {
            Debug.LogWarning("LightController not found!");
        }

        deskLamp.enabled = false;
        bedsideLamp.enabled = false;
        roomLight.enabled = false;
        floorLamp.enabled = false;

        Debug.Log("All lights set up successfully!");
    }

    private Light CreateLight(string lightName, Vector3 position,
        LightType type, Color color, float intensity,
        float range, float spotAngle)
    {
        GameObject lightObj = new GameObject(lightName);
        lightObj.transform.position = position;

        Light light = lightObj.AddComponent<Light>();
        light.type = type;
        light.color = color;
        light.intensity = intensity;
        light.range = range;

        if (type == LightType.Spot)
        {
            light.spotAngle = spotAngle;
            lightObj.transform.rotation =
                Quaternion.Euler(90f, 0f, 0f);
        }

        Debug.Log(lightName + " created at " + position);
        return light;
    }
}