using UnityEngine;
using System.Collections;

public class WaterController : MonoBehaviour {

    public float displacement = 0.02f;

    private float startOffset;

	// Use this for initialization
	void Start () {
        startOffset = Random.Range(-1, 1);
        transform.position = new Vector3(transform.position.x, startOffset, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
