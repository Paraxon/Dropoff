﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Police : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
        GetComponent<NavMeshAgent>().destination = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}