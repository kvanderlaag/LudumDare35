using UnityEngine;
using System.Collections;

public class Billboard2D : MonoBehaviour
{
	void Update()
	{
		transform.LookAt(
				Camera.main.transform.position + 2*(transform.position - Camera.main.transform.position), Vector3.up);
	}
}
