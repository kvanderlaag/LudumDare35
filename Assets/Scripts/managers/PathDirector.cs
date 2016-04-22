using UnityEngine;
using System.Collections;

public class PathDirector : MonoBehaviour
{
	public PathDirector[] nextPathNodes;

	public PathDirector GetPath()
	{
		//may want to change this to incorperate a weighted system in the
		//future, as well as a property in waves.json which dictates whether
		//there's a path the mob should try to take
		if (nextPathNodes.Length == 0) return null;

		int randPathIdx = Random.Range(0, nextPathNodes.Length);
		return nextPathNodes[randPathIdx];
	}
}
