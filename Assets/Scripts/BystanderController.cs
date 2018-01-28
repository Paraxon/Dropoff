using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public enum BystanderState
{
    Moving,
    Idle,
    Leaving
}

public class BystanderController : MonoBehaviour {

    public int remainingStops, minStops, maxStops;
    public float stopDuration, minDuration, maxDuration;
    public Transform idlePoint, exitPoint;
    public float pointRadius = 2.0f;
    public BystanderState currentState;
    Actor actor;

	// Use this for initialization
	void Start () {
        actor = GetComponent<Actor>();
        actor.Respawn();
        remainingStops = Random.Range(minStops, maxStops);
        idlePoint = actor.GetDrop();
        GetComponent<AICharacterControl>().SetTarget(idlePoint);
        currentState = BystanderState.Moving;
    }
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
            case BystanderState.Moving:
                if (Vector3.Distance(transform.position, idlePoint.position) < pointRadius)
                {
                    remainingStops--;
                    stopDuration = Random.Range(minDuration, maxDuration);
                    GetComponent<AICharacterControl>().SetTarget(transform);
                    currentState = BystanderState.Idle;
                }
                break;
            case BystanderState.Idle:
                stopDuration -= Time.deltaTime;
                if (stopDuration <= 0)
                {
                    if (remainingStops > 0)
                    {
                        idlePoint = actor.GetDrop();
                        GetComponent<AICharacterControl>().SetTarget(idlePoint);
                        currentState = BystanderState.Moving;
                    }
                    else
                    {
                        exitPoint = actor.GetSpawn();
                        GetComponent<AICharacterControl>().SetTarget(exitPoint);
                        currentState = BystanderState.Leaving;
                    }
                }
                break;
            case BystanderState.Leaving:
                if (Vector3.Distance(transform.position, exitPoint.position) < pointRadius)
                {
                    actor.Respawn();
                    remainingStops = Random.Range(minStops, maxStops);
                    idlePoint = actor.GetDrop();
                    GetComponent<AICharacterControl>().SetTarget(idlePoint);
                    currentState = BystanderState.Moving;
                }
                break;
            default:
                break;
        }
    }
}
