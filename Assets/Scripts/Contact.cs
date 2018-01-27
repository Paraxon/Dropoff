using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ContactState
{
    Arriving,
    Moving,
    Waiting
}

[RequireComponent(typeof(NavMeshAgent))]
public class Contact : MonoBehaviour {

    public ContactState currentState;
    public Vector3 dropoffPoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case ContactState.Moving:
                break;
            case ContactState.Waiting:
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
