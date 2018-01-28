using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnStep : MonoBehaviour {

	void PlaySound() {
		int i = Random.Range (1, 7);
		GetComponent<AudioSource>().clip = Resources.Load ("Audio/concrete " + i) as AudioClip;
		GetComponent<AudioSource> ().Play ();
	}

	void OnTriggerEnter(Collider other) {

		PlaySound ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
