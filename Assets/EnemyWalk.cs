using UnityEngine;
using System.Collections;

public class EnemyWalk : MonoBehaviour {

    public float stepTime = 0.25f;
    private float stepPosition;
    private float offset;

	// Use this for initialization
	void Awake () {
        offset = Random.Range(-1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        stepPosition += Time.deltaTime;
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 25f * Mathf.Sin((stepPosition / stepTime) + offset)));
	}
}
