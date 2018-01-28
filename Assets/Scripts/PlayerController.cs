using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Inventory))]
public class PlayerController : MonoBehaviour {

    ContactController contact;
    Inventory inventory;
    Actor actor;
<<<<<<< HEAD
=======
    public float minHeat, heatDecay, maxHeat;
    public float walkHeat, runHeat, jumpHeat, idleHeat, triggerHeat, currentHeat, heatDuration, heatTimer;
    Animator animator;
    AnimationCurve heatCooldown;
>>>>>>> f2693d1146dabb4c86dda6f09d34d32dddf09193

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
<<<<<<< HEAD
=======
        heatTimer = Mathf.Max(heatTimer - Time.deltaTime, 0f);
        heatTimer = Mathf.Min(heatTimer, maxHeat);
        if (heatTimer <=0)
            currentHeat = Mathf.Max(currentHeat - heatDecay * Time.deltaTime, minHeat);
>>>>>>> f2693d1146dabb4c86dda6f09d34d32dddf09193
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
