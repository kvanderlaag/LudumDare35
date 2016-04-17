using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour
{
	public GameState gameState;
	public GameObject builtObject;

	// selects this tile
	public void SelectTile()
	{
		if (builtObject == null && gameState.phaseState == EPhaseState.UPKEEP)
		{
			//bring up build options
		}

		else if (buildObject != null)
		{
			//select tower and bring up shapeshift options
		}
	}
}
