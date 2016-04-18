using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour
{
	//public GameObject tower;
	private Renderer thisRenderer;

	public GameState gameState;
	public GameObject builtObject; //object currently on the tile
	public GameObject towerPrefab;

    public void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
    }

	// selects this tile
	public void SelectTile()
	{
		if (builtObject == null && gameState.phaseState == EPhaseState.UPKEEP)
		{
			//bring up build options
		}

		else if (builtObject != null)
		{
			//select tower and bring up shapeshift options
		}
	}

	public void BuildTower(ETowerState startState)
	{
		GameObject newTower = Instantiate(towerPrefab);
		newTower.transform.position = transform.position
			+ new Vector3(0f, thisRenderer.bounds.extents.y, 0f);
		newTower.transform.parent = transform;

		newTower.GetComponent<Tower>().SwitchTowerInstant(startState);
	}

	public void DemolishTower()
	{
		Destroy(builtObject);
		builtObject = null;
	}
}
