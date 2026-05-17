using UnityEngine;

public class LightController : MonoBehaviour
{
    [Header("Room Lights")]
    public Light deskLamp;
    public Light bedsideLamp;
    public Light ceilingBulb;
    public Light floorLamp;

    private bool lightsOn = false;

    public void TurnOnLights()
    {
        lightsOn = true;
        if (deskLamp != null) deskLamp.enabled = true;
        if (bedsideLamp != null) bedsideLamp.enabled = true;
        if (ceilingBulb != null) ceilingBulb.enabled = true;
        if (floorLamp != null) floorLamp.enabled = true;
        Debug.Log("Room lights ON");
    }

    public void TurnOffLights()
    {
        lightsOn = false;
        if (deskLamp != null) deskLamp.enabled = false;
        if (bedsideLamp != null) bedsideLamp.enabled = false;
        if (ceilingBulb != null) ceilingBulb.enabled = false;
        if (floorLamp != null) floorLamp.enabled = false;
        Debug.Log("Room lights OFF");
    }

    public bool AreLightsOn()
    {
        return lightsOn;
    }
}