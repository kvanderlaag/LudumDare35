using UnityEngine;
using System.Collections;

public class MenuHandController : MonoBehaviour {

    private SteamVR_TrackedController controller;
    private SteamVR_TrackedObject trackedObject;

    // Use this for initialization
    void OnEnable()
    {
        controller = transform.parent.GetComponent<SteamVR_TrackedController>();
        controller.MenuButtonClicked += MenuClicked;

        trackedObject = transform.parent.GetComponent<SteamVR_TrackedObject>();


    }

    void OnDisable()
    {
        controller = transform.parent.GetComponent<SteamVR_TrackedController>();
        controller.MenuButtonClicked -= MenuClicked;
    }

    void MenuClicked(object sender, ClickedEventArgs e)
    {
        LoadLevel.Load();
    }
}
