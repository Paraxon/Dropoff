using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioHints : MonoBehaviour {

    public GameObject contact;
    public float timer = 0;
    public float hintInterval = 90;
    public int hintLimit = 2;
    public int hintsGiven = 0;
    public string[] hints;

	// Use this for initialization
	void Start () {
        contact = GameObject.Find("Contact");
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= hintInterval && hintsGiven < hintLimit)
        {
            GiveHint();
            hintsGiven++;
        }
	}

    public void GiveHint()
    {
        
    }

    public void Reset()
    {
        timer = 0f;
        hintsGiven = 0;
        hints = contact.GetComponent<Profile>().GetHints();
    }
}
