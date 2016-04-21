using UnityEngine;
using System.Collections;

public class Buildable : MonoBehaviour
{
	private Renderer thisRenderer;
	public Renderer towerRenderer;

	public GameState gameState;
	public GameObject builtObject; //object currently on the tile
	public GameObject towerPrefab;

    public void Awake()
    {
        thisRenderer = GetComponent<Renderer>();
        towerRenderer = towerPrefab.GetComponent<Renderer>();
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
		newTower.transform.position = transform.position +
			new Vector3(
				0f,
				0f,
				0f
			);
		newTower.transform.parent = transform;

		newTower.GetComponent<Tower>().SwitchTowerInstant(startState);
        
	}

	public void DemolishTower()
	{
		Destroy(builtObject);
		builtObject = null;
	}
}
