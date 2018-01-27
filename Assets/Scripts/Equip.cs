using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour {

    public Transform hand;
    public GameObject inventory;

	// Use this for initialization
	void Start () {
        if (inventory)
            PickUp(inventory);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PickUp(GameObject item) {
        Debug.Log(string.Format("{0} picked up {1}", name, item.name));
        item.transform.parent = hand;
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<Collider>().enabled = false;

    }

    public void Drop() {
        if (inventory)
        {
            Debug.Log(string.Format("{0} dropped {1}", name, inventory.name));
            inventory.transform.parent = null;
            inventory.GetComponent<Rigidbody>().isKinematic = false;
            inventory.GetComponent<Collider>().enabled = true;
            inventory = null;
        }
    }
}
