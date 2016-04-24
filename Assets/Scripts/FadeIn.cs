using UnityEngine;
using System.Collections;
using Valve.VR;

public class FadeIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SteamVR_Fade.View(Color.black, 0);
        StartCoroutine(FadeInScene());
	}

    IEnumerator FadeInScene()
    {
        var device = SteamVR_Controller.Input((int) OpenVR.k_unTrackedDeviceIndex_Hmd);
        while (device == null)
            yield return null;
        while (device.calibrating || device.outOfRange || !device.connected || device.uninitialized)
            yield return null;

        
        SteamVR_Fade.View(Color.clear, 3);
    }
	
}
