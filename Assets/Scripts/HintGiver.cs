using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintGiver : MonoBehaviour {

	public KeyValuePair<string, string> HeldHint = new KeyValuePair<string, string>();

	public void ReceiveHint(KeyValuePair<string, string> Hint) {
		HeldHint = Hint;
		gameObject.AddComponent<AudioSource> ();
		GetComponent<AudioSource> ().clip = Resources.Load ("Audio/Hints/" + HeldHint.Key + "-" + HeldHint.Value) as AudioClip;
	}

	public void GiveHint() {
		//Something needs to trigger this
		GetComponent<AudioSource> ().Play ();
		Debug.Log ("Telling player " + HeldHint.Key + " = " + HeldHint.Value);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
