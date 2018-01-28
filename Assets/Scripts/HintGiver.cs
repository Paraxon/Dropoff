using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintGiver : MonoBehaviour {

	public string HeldHint = "";

	public void ReceiveHint(string Hint) {
		HeldHint = Hint;
		GetComponent<AudioSource> ().clip = Resources.Load ("Audio/Hints/" + HeldHint) as AudioClip;
	}

	public void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.name == "Player")
			GiveHint ();
	}

	public void GiveHint() {
		//Something needs to trigger this
		if (HeldHint != "") {
			GetComponent<AudioSource> ().Play ();
			Debug.Log ("Telling player " + HeldHint);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
