using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour {

    public float displacement = 0.02f;

    private float startOffset;
    private float startY;

	// Use this for initialization
	void Start () {
        startY = transform.position.y;
        startOffset = Random.Range(-2 * Mathf.PI, 2* Mathf.PI);
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, startY + (displacement * Mathf.Sin(Time.time + startOffset)), transform.position.z);
	}
}
