using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

    Transform costumes;
    Transform drops, spawns;

    // Use this for initialization
    void Awake () {
        costumes = transform.Find("Costumes");
        drops = GameObject.Find("Drops").transform;
        spawns = GameObject.Find("Spawns").transform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Respawn()
    {
        Transform spawn = GetSpawn();
        transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        ShuffleCostume();
        ShuffleMaterial();
    }

    public void ShuffleCostume()
    {
        foreach (Transform costume in costumes)
            costume.gameObject.SetActive(false);
        int index = Random.Range(0, costumes.childCount);
        costumes.GetChild(index).gameObject.SetActive(true);
    }

    public void ShuffleMaterial()
    {
        foreach (Transform costume in costumes)
        {
            if (costume.gameObject.activeInHierarchy)
            {
                int index = Random.Range(1, 4);
                string source = "Polygon Characters/Materials/Polygon_City_Characters_Mat_0" + index + "_A";
                Material newMat = Resources.Load<Material>(source);
                SkinnedMeshRenderer renderer = costume.GetComponent<SkinnedMeshRenderer>();
                renderer.material = newMat;
                break;
            }
        }
        
    }

    public void SetCostume(string name, int material)
    {
        foreach (Transform costume in costumes)
        {
            if (costume.name == name)
            {
                costume.gameObject.SetActive(true);
                string source = "Polygon Characters/Materials/Polygon_City_Characters_Mat_0" + material + "_A";
                Material newMat = Resources.Load<Material>(source);
                SkinnedMeshRenderer renderer = costume.GetComponent<SkinnedMeshRenderer>();
                renderer.material = newMat;
            }
            else
            {
                costume.gameObject.SetActive(false);
            }
        }
    }

    public Transform GetDrop()
    {
        int index = Random.Range(0, drops.childCount);
        return drops.GetChild(index);
    }

    public Transform GetSpawn()
    {
        int index = Random.Range(0, spawns.childCount);
        return spawns.GetChild(index);
    }
}
