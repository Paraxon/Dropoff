using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RadioHints : MonoBehaviour {

    public GameObject contact;
    AudioSource audioSource;
    public float timer = 0;
    public float hintInterval = 90;
    public int hintLimit = 2;
    public int hintsGiven = 0;
    public string[] hints;
    public int briefcaseLines = 10;
    public int lastBriefcase = -1;

	// Use this for initialization
	void Start () {
        contact = GameObject.Find("Contact");
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= hintInterval && hintsGiven < hintLimit)
            GiveHint(hints[hintsGiven]);
	}

    public void OnPickup()
    {
        int index;
        do
            index = UnityEngine.Random.Range(0, briefcaseLines);
        while (index == lastBriefcase);
        lastBriefcase = index;
        string filename = "Briefcase " + index;
        AudioClip hint = Resources.Load<AudioClip>("Audio/Breifcase/"+filename);
        audioSource.Stop();
        audioSource.PlayOneShot(hint);
    }

    public void GiveHint(string filename)
    {
        hintsGiven++;
        timer = 0;
        Debug.Log("Giving hint '" + filename + "'");
        AudioClip hint = Resources.Load<AudioClip>("Audio/Hints/"+filename);
        audioSource.Stop();
        audioSource.PlayOneShot(hint);
    }

    public void Reset()
    {
        timer = 0f;
        hintsGiven = 0;
        hints = contact.GetComponent<Profile>().GetHints();
        //foreach (string hint in hints) Debug.Log(hint);
    }
}
