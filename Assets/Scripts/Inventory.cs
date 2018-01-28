using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public Transform hand;
    public GameObject currentItem;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentItem != null && currentItem.transform.parent != hand)
            currentItem = null;
	}

    public void PickUp(GameObject item)
    {
        Debug.Log(string.Format("{0} picked up {1}", name, item.name));
        item.GetComponent<Rigidbody>().isKinematic = true;
        item.GetComponent<Collider>().enabled = false;
        item.transform.parent = hand;
        item.transform.SetPositionAndRotation(hand.position, hand.rotation);
        currentItem = item;
    }

    public void DropItem()
    {
        if (currentItem)
        {
            Debug.Log(string.Format("{0} dropped {1}", name, currentItem.name));
            currentItem.transform.parent = null;
            currentItem.GetComponent<Rigidbody>().isKinematic = false;
            currentItem.GetComponent<Collider>().enabled = true;
            currentItem = null;
        }
    }
}
