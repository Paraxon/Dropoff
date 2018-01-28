﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public enum ContactState
{
    Arriving,
    Waiting,
    Triggered,
    PickingUp,
    Leaving
}

[RequireComponent(typeof(NavMeshAgent))]
public class ContactController : MonoBehaviour {

    public ContactState currentState;
    public Transform dropoffPoint, exitPoint;
    public GameObject package;
    public float stateTimer = 0.0f;
    public float maxTime = 3.0f;
    public bool triggeredFlag = false;
    public float pickupRadius = 0.5f;
    public float visibilityRadius = 2.5f;
    public float pointRadius = 0.25f;
    public bool recieving = true;
    Actor actor;

	// Use this for initialization
	void Start () {
        actor = GetComponent<Actor>();
        Respawn();
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case ContactState.Arriving:
                if (!recieving && GetComponent<Inventory>().IsEmpty())
                {
                    Leave();
                    GameObject.Find("Radio").GetComponent<RadioHints>().OnPickup();
                }
                if (Vector3.Distance(transform.position, dropoffPoint.position) < pointRadius)
                    Wait();
                break;
            case ContactState.Waiting:
                if (!recieving && GetComponent<Inventory>().IsEmpty())
                {
                    Leave();
                    GameObject.Find("Radio").GetComponent<RadioHints>().OnPickup();
                }
                GameObject player = GameObject.Find("Player");
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (triggeredFlag && distance < visibilityRadius)
                    Alert();
                break;
            case ContactState.Triggered:
                if (recieving)
                {
                    stateTimer -= Time.deltaTime;
                    if (stateTimer <= 0)
                        Wait();
                    if (Vector3.Distance(transform.position, package.transform.position) < visibilityRadius)
                        PickUp();
                }
                else
                {
                    GetComponent<Inventory>().DropItem();
                    Leave();
                }
                break;
            case ContactState.PickingUp:
                if (Vector3.Distance(transform.position, package.transform.position) < pickupRadius)
                {
                    GetComponent<Inventory>().PickUp(package);
                    Leave();
                }
                break;
            case ContactState.Leaving:
                if (Vector3.Distance(transform.position, exitPoint.position) < pointRadius)
                {
                    ToggleRecieving();
                    Respawn();
                }
                break;
            default:
                break;
        }
    }

    private void Respawn()
    {
        actor.Respawn();
        GameObject.Find("Radio").GetComponent<RadioHints>().Reset();

        dropoffPoint = actor.GetDrop();
        exitPoint = actor.GetSpawn();

        GetComponent<AICharacterControl>().SetTarget(dropoffPoint);
        currentState = ContactState.Arriving;
    }

    private void ToggleRecieving()
    {
        recieving = !recieving;
    }

    private void PickUp()
    {
        GetComponent<AICharacterControl>().SetTarget(package.transform);
        currentState = ContactState.PickingUp;
    }

    private void Alert()
    {
        currentState = ContactState.Triggered;
        triggeredFlag = false;
    }

    private void Wait()
    {
        stateTimer = maxTime;
        currentState = ContactState.Waiting;
    }

    private void Leave()
    {
        GetComponent<AICharacterControl>().SetTarget(exitPoint);
        currentState = ContactState.Leaving;
    }

    void Respond()
    {
        Debug.Log("Contact responds!");
    }
}
