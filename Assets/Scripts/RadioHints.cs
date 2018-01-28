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
	public GameObject[] hintGivers = new GameObject[10];

	// Use this for initialization
	void Start () {
        contact = GameObject.Find("Contact");
<<<<<<< HEAD
		GameObject[] hintGivers = new GameObject[GameObject.FindGameObjectsWithTag ("HintGiver").Length];
		int i = 0;
		foreach (GameObject HintGiver in GameObject.FindGameObjectsWithTag("HintGiver")) {
			hintGivers [i] = HintGiver;
			i++;
		}
		SetHintGivers (hintGivers, contact.GetComponent<Profile>().GetHints());
=======
        audioSource = GetComponent<AudioSource>();
>>>>>>> 3f8e564f9deb51fd60389da13d401e80e9de41b4
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

	GameObject[] reshuffle(GameObject[] gameobjects)
	{
		for (int t = 0; t < gameobjects.Length; t++ )
		{
			GameObject tmp = gameobjects[t];
			int r = UnityEngine.Random.Range(t, gameobjects.Length);
			gameobjects[t] = gameobjects[r];
			gameobjects[r] = tmp;
		}
		return gameobjects;
	}

	public void SetHintGivers(GameObject[] HintGivers, string[] Hints) {
		foreach (GameObject HintGiver in HintGivers) {
			HintGiver.GetComponent<HintGiver> ().ReceiveHint ("");
			HintGiver.GetComponent<HintGiver>().enabled = false;
			HintGiver.GetComponent<Light> ().enabled = false;
		}
		hintGivers = reshuffle (HintGivers);
		int h = 0;
		while (h < 3) {
			HintGivers[h].GetComponent<HintGiver>().enabled = true;
			HintGivers[h].GetComponent<Light> ().enabled = true;

			HintGivers [h].GetComponent<HintGiver> ().ReceiveHint (Hints [h + 2]);
			h++;
		}
	}

    public void Reset()
    {
        timer = 0f;
        hintsGiven = 0;
        hints = contact.GetComponent<Profile>().GetHints();
		SetHintGivers (hintGivers, hints);

        //foreach (string hint in hints) Debug.Log(hint);
    }
}
