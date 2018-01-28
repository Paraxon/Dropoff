using System.Collections;
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

	// Use this for initialization
	void Start () {
        Transform spawn = GetSpawn();
        transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        dropoffPoint = GetDrop();
        exitPoint = GetSpawn();
        GetComponent<AICharacterControl>().SetTarget(dropoffPoint);
        currentState = ContactState.Arriving;
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case ContactState.Arriving:
                if (Vector3.Distance(transform.position, dropoffPoint.position) < pointRadius)
                {
                    currentState = ContactState.Waiting;
                }
                break;
            case ContactState.Waiting:
                stateTimer = maxTime;
                GameObject player = GameObject.Find("Player");
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (triggeredFlag && distance < visibilityRadius)
                {
                    currentState = ContactState.Triggered;
                    triggeredFlag = false;
                }
                break;
            case ContactState.Triggered:
                if (recieving)
                {
                    stateTimer -= Time.deltaTime;
                    if (stateTimer <= 0)
                        currentState = ContactState.Waiting;
                    if (Vector3.Distance(transform.position, package.transform.position) < visibilityRadius)
                    {
                        GetComponent<AICharacterControl>().SetTarget(package.transform);
                        currentState = ContactState.PickingUp;
                    }
                }
                else
                {
                    GetComponent<Inventory>().DropItem();
                    GetComponent<AICharacterControl>().SetTarget(exitPoint);
                    currentState = ContactState.Leaving;
                }
                break;
            case ContactState.PickingUp:
                if (Vector3.Distance(transform.position, package.transform.position) < pickupRadius)
                {
                    GetComponent<Inventory>().PickUp(package);
                    GetComponent<AICharacterControl>().SetTarget(exitPoint);
                    currentState = ContactState.Leaving;
                }
                break;
            case ContactState.Leaving:
                if (Vector3.Distance(transform.position, exitPoint.position) < pointRadius)
                {
                    ShuffleCostume();
                    Transform spawn = GetSpawn();
                    transform.SetPositionAndRotation(spawn.position, spawn.rotation);
                    dropoffPoint = GetDrop();
                    exitPoint = GetSpawn();
                    GetComponent<AICharacterControl>().SetTarget(dropoffPoint);
                    recieving = !recieving;
                    currentState = ContactState.Arriving;
                }
                break;
            default:
                break;
        }
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

    public void ShuffleCostume()
    {
        Transform costumes = transform.Find("Costumes");
        foreach (Transform costume in costumes)
            costume.gameObject.SetActive(false);
        int index = Random.Range(0, costumes.childCount);
        costumes.GetChild(index).gameObject.SetActive(true);
    }

    void Respond()
    {
        Debug.Log("Contact responds!");
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
