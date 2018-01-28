using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Drop"))
            GetComponent<Inventory>().DropItem();
        if (Input.GetButtonDown("Trigger"))
        {
            GameObject contact = GameObject.Find("Contact");
            contact.GetComponent<ContactController>().triggeredFlag = true;
            Debug.Log("Triggering Contact");
        }
	}

    void OnTriggerStay(Collider other)
    {
        CostumeSource source = other.GetComponent<CostumeSource>();
        if (source != null && Input.GetButtonDown("Interact"))
            SwapCostume(source.costumeName);
        else if (other.tag == "Item" && Input.GetButtonDown("Interact"))
            GetComponent<Inventory>().PickUp(other.gameObject);
    }

    void SwapCostume(string costumeName)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(child.name == costumeName);
    }
}
