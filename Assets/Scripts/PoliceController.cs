using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

public enum PoliceState
{
    Patrolling,
    Chasing
}

[RequireComponent(typeof(Actor))]
[RequireComponent(typeof(AICharacterControl))]
public class PoliceController : MonoBehaviour {

    PlayerController player;
    public float visibilityRadius = 10, pointRadius = 2, maxSpeed = 3, filedOfView = 60f;
    public float sightTimer, sightDuration = 5, chaseThreshold, globalChaseThrehsold;
    public AnimationCurve speedByHeat;
    Transform patrolPoint;
    public PoliceState currentState;
    Actor actor;
    AICharacterControl aiController;
    NavMeshAgent navAgent;

	// Use this for initialization
	void Start () {
        actor = GetComponent<Actor>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        aiController = GetComponent<AICharacterControl>();
        navAgent = GetComponent<NavMeshAgent>();
        actor.Respawn();
        Patrol();
	}

    // Update is called once per frame
	void Update () {
        navAgent.speed = speedByHeat.Evaluate(player.currentHeat / player.maxHeat) * maxSpeed;
        switch (currentState)
        {
            case PoliceState.Patrolling:
                if (Vector3.Distance(transform.position, patrolPoint.position) < pointRadius)
                    patrolPoint = actor.GetDrop();
                if (CanSeePlayer())
                {
                    player.currentHeat += player.GetAnimationHeat() * Time.deltaTime;
                    sightTimer = sightDuration;
                    if (player.currentHeat > chaseThreshold)
                        ChasePlayer();
                }
                else if (player.currentHeat > globalChaseThrehsold)
                {
                    ChasePlayer();
                }
                else
                {
                    sightTimer -= Time.deltaTime;
                    sightTimer = Mathf.Max(sightTimer, 0f);
                }
                break;
            case PoliceState.Chasing:
                if (CanSeePlayer())
                {
                    player.currentHeat += player.GetAnimationHeat() * Time.deltaTime;
                    sightTimer = sightDuration;
                }
                else if (sightTimer > 0)
                {
                    sightTimer -= Time.deltaTime;
                    sightTimer = Mathf.Max(sightTimer, 0f);
                }
                else
                {
                    Patrol();
                }
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player" && currentState == PoliceState.Chasing)
            GameOver();
    }

    private void GameOver()
    {
        GameObject.Find("Canvas").GetComponent<GUIManager>().OnGameOver();
    }

    private void Patrol()
    {
        patrolPoint = actor.GetDrop();
        aiController.SetTarget(patrolPoint);
        currentState = PoliceState.Patrolling;
    }

    private void ChasePlayer()
    {
        aiController.SetTarget(player.transform);
        currentState = PoliceState.Chasing;
    }

    private bool CanSeePlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (Vector3.Angle(transform.forward, direction) > filedOfView)
            return false;
        if (direction.magnitude > visibilityRadius)
            return false;
        return true;
    }
}
