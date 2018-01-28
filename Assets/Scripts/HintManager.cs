using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HintManager : MonoBehaviour {

	public int HintsPerDropoff = 5;
	public Dictionary<string, string> CurrentHints = new Dictionary<string, string>();
	public string FirstContact;

	public void GenerateNewHints(GameObject Contact) {
		CurrentHints.Clear ();
		if (Contact.GetComponent<Profile> () != null) {
			foreach (KeyValuePair<string, string> NewHint in Contact.GetComponent<Profile>().ProvideHints(HintsPerDropoff)) {
				CurrentHints.Add (NewHint.Key, NewHint.Value);
			}
		}
	}

	public void DistributeHints(Dictionary<string, string> currentHints) {
		//Spawn NPC
		//Add HintGiver script
		//Send hint via ReceiveHint
	}

	// Use this for initialization
	void Start () {
		if (GameObject.Find (FirstContact) != null) {
			GenerateNewHints (GameObject.Find (FirstContact));
			DistributeHints (CurrentHints);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
