using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Inventory))]
public class PlayerController : MonoBehaviour {

    ContactController contact;
    Inventory inventory;
    Actor actor;

    // Use this for initialization
    void Start () {
        contact = GameObject.Find("Contact").GetComponent<ContactController>();
        inventory = GetComponent<Inventory>();
        actor = GetComponent<Actor>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Drop"))
            inventory.DropItem();
        if (Input.GetButtonDown("Trigger"))
            contact.Trigger(transform);
	}

    void OnTriggerStay(Collider other)
    {
        CostumeSource source = other.GetComponent<CostumeSource>();
        if (source != null && Input.GetButtonDown("Interact"))
            actor.SetCostume(source.costumeName, source.materialNumber);
        else if (other.tag == "Item" && Input.GetButtonDown("Interact"))
            inventory.PickUp(other.gameObject);
    }
}
