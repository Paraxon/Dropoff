using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Respawn()
    {
        Transform spawn = GetSpawn();
        transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        ShuffleCostume();
    }

    public void ShuffleCostume()
    {
        Transform costumes = transform.Find("Costumes");
        foreach (Transform costume in costumes)
            costume.gameObject.SetActive(false);
        int index = Random.Range(0, costumes.childCount);
        costumes.GetChild(index).gameObject.SetActive(true);
    }

    public Transform GetDrop()
    {
        Transform drops = GameObject.Find("Drops").transform;
        int index = Random.Range(0, drops.childCount);
        return drops.GetChild(index);
    }

    public Transform GetSpawn()
    {
        Transform spawns = GameObject.Find("Spawns").transform;
        int index = Random.Range(0, spawns.childCount);
        return spawns.GetChild(index);
    }
}
