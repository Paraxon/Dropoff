using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(Inventory))]
public class PlayerController : MonoBehaviour {

    ContactController contact;
    Inventory inventory;
    Actor actor;
    public float minHeat, heatDecay, maxHeat;
    public float walkHeat, runHeat, jumpHeat, idleHeat, triggerHeat, currentHeat, heatDuration, heatDecayRate, heatTimer;
    Animator animator;
    AnimationCurve heatCooldown;

    // Use this for initialization
    void Start () {
        contact = GameObject.Find("Contact").GetComponent<ContactController>();
        inventory = GetComponent<Inventory>();
        actor = GetComponent<Actor>();
        animator = GetComponent<Animator>();
	}

    public float GetAnimationHeat()
    {
        if (animator.GetFloat("Forward") > 0.5f) //Player is running
            return runHeat;
        else if (animator.GetFloat("Jump") > 0) //Player is jumping
            return jumpHeat;
        else if (animator.GetFloat("Forward") > 0f) //Player is walking
            return walkHeat;
        else return idleHeat; //Player is not moving
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Drop"))
            inventory.DropItem();
        if (Input.GetButtonDown("Trigger"))
            contact.Trigger(transform);
        heatTimer = Mathf.Max(heatTimer - Time.deltaTime, 0f);
        heatTimer = Mathf.Min(heatTimer, maxHeat);
        if (heatTimer <=0)
            currentHeat = Mathf.Max(currentHeat - heatDecayRate * Time.deltaTime, minHeat);
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
