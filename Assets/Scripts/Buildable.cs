using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour
{
<<<<<<< HEAD
	//public GameObject tower;
	private Renderer thisRenderer;
=======
	public GameObject tower;
	public Renderer thisRenderer;
	public Renderer towerRenderer;
>>>>>>> origin/master

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
<<<<<<< HEAD
		GameObject newTower = Instantiate(towerPrefab);
		newTower.transform.position = transform.position
			+ new Vector3(0f, thisRenderer.bounds.extents.y, 0f);
=======
		GameObject newTower = Instantiate(tower);
		newTower.transform.position = transform.position +
			new Vector3(
				0f,
				thisRenderer.bounds.extents.y + towerRenderer.bounds.extents.y,
				0f
			);
>>>>>>> origin/master
		newTower.transform.parent = transform;

		newTower.GetComponent<Tower>().SwitchTowerInstant(startState);
	}

	public void DemolishTower()
	{
		Destroy(builtObject);
		builtObject = null;
	}
}
