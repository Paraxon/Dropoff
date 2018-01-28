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

[RequireComponent(typeof(AICharacterControl))]
[RequireComponent(typeof(Inventory))]
public class ContactController : MonoBehaviour {

    public ContactState currentState;
    Transform dropoffPoint, exitPoint;
    public float stateTimer = 0.0f;
    public float maxTime = 3.0f;
    public bool triggeredFlag = false;
    public float pickupRadius = 0.5f;
    public float visibilityRadius = 2.5f;
    public float pointRadius = 0.25f;
    public bool recieving = true;
    Actor actor;
    AICharacterControl aiController;
    Inventory inventory;
    RadioHints radio;
    GameObject player, package;

	// Use this for initialization
	void Start () {
        actor = GetComponent<Actor>();
        aiController = GetComponent<AICharacterControl>();
        inventory = GetComponent<Inventory>();
        radio = GameObject.Find("Radio").GetComponent<RadioHints>();
        player = GameObject.Find("Player");
        package = GameObject.Find("Package");
        Respawn();
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case ContactState.Arriving:
                if (!recieving && inventory.IsEmpty())
                {
                    Leave();
                    radio.OnPickup();
                }
                if (Vector3.Distance(transform.position, dropoffPoint.position) < pointRadius)
                    Wait();
                break;
            case ContactState.Waiting:
                if (!recieving && inventory.IsEmpty())
                {
                    Leave();
                    radio.OnPickup();
                }
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
                   inventory.DropItem();
                   Leave();
                }
                break;
            case ContactState.PickingUp:
                if (Vector3.Distance(transform.position, package.transform.position) < pickupRadius)
                {
                    inventory.PickUp(package);
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

    public void Trigger(Transform player)
    {
        triggeredFlag = Vector3.Distance(transform.position, player.position) < visibilityRadius;
    }

    private void Respawn()
    {
        actor.Respawn();
        radio.Reset();

        dropoffPoint = actor.GetDrop();
        exitPoint = actor.GetSpawn();

        aiController.SetTarget(dropoffPoint);
        currentState = ContactState.Arriving;
    }

    private void ToggleRecieving()
    {
        recieving = !recieving;
    }

    private void PickUp()
    {
        aiController.SetTarget(package.transform);
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
        aiController.SetTarget(exitPoint);
        currentState = ContactState.Leaving;
    }

    void Respond()
    {
        Debug.Log("Contact responds!");
    }
}
