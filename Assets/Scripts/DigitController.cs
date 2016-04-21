using UnityEngine;
using System.Collections;

public class DigitController : MonoBehaviour {

    public Material[] digitMaterials;


    private Renderer render;

    public void Start()
    {
        render = GetComponent<Renderer>();
    }

	// Use this for initialization
	public void Change(int digit)
    {
        digit %= 10;
        render.material = digitMaterials[digit];
    }

    public void Show()
    {
        GetComponent<Renderer>().enabled = true;
    }

    public void Hide()
    {
        GetComponent<Renderer>().enabled = false;
    }
}
