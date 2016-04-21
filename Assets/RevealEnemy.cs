using UnityEngine;
using System.Collections;

public class RevealEnemy : MonoBehaviour {

    public Material HiddenMaterialFront;
    public Material HiddenMaterialBack;

    public Material WerewolfMaterialFront;
    public Material WerewolfMaterialBack;

    public Material VampireMaterialFront;
    public Material VampireMaterialBack;

    public Material AlienMaterialFront;
    public Material AlienMaterialBack;

    public Renderer ren;
    public Renderer renBack;

    // Use this for initialization
    void Start () {
        //ren = GetComponent<MeshRenderer>();
        //renBack = GetComponentInChildren<MeshRenderer>();
        HiddenMaterialFront = ren.material;
        HiddenMaterialBack = renBack.material;

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Reveal(EDamageType type)
    {
        Material[] mats = new Material[1];
        if (type == EDamageType.WEREWOLF) {
            //Debug.Log("Revealing " + type);
            mats = ren.materials;
            mats[0] = WerewolfMaterialFront;
            ren.materials = mats;

            mats = renBack.materials;
            mats[0] = WerewolfMaterialBack;
            renBack.materials = mats;
        }
        else if (type == EDamageType.VAMPIRE) {
            //Debug.Log("Revealing " + type);
            mats = ren.materials;
            mats[0] = VampireMaterialFront;
            ren.materials = mats;

            mats = renBack.materials;
            mats[0] = VampireMaterialBack;
            renBack.materials = mats;

        } else if (type == EDamageType.ALIEN) {
            //Debug.Log("Revealing " + type);
            mats = ren.materials;
            mats[0] = AlienMaterialFront;
            ren.materials = mats;

            mats = renBack.materials;
            mats[0] = AlienMaterialBack;
            renBack.materials = mats;
                
        }
        
    }
}
