using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	public AudioSource[] ThreatSources = new AudioSource[4];
	//TODO Probably some kinda time delta time would be better...
	public float VolumeIncrement = 0.01f;
	//Corresponding to above sources, 0 means no change, -1 means decreasing, 1 means increasing
	private int[] ThreatState = new int[4] { 0, 0, 0, 0 };

	public float CurrentThreat = 0;
	public float AutoIncrement = 0;
	public bool Testing = false;

	public void ChangeVolume () {
		int i = 0;
		while (i < ThreatState.Length) {
			if (ThreatSources [i].volume != 1 && ThreatState [i] == 1) {
				//Increment up
				ThreatSources[i].volume += VolumeIncrement;
			} else if (ThreatSources [i].volume != 0 && ThreatState [i] == -1) {
				//Increment down
				ThreatSources[i].volume -= VolumeIncrement;
			} else {
				//No need to change
				ThreatState[i] = 0;
			}
			i++;
		}
	}

	public void CheckThreat(float Threat) {
		//This should allow for threat level changes
		//Transition points are 25%, 50%, 75%, 100%
		if (CurrentThreat < 25 && Threat >= 25) {
			//Increasing from 0 to 1
			Debug.Log("Threat increasing to low");
			ThreatState [0] = 1;
		}
		if (CurrentThreat < 50 && Threat >= 50) {
			//Increasing from 1 to 2
			Debug.Log("Threat increasing to mid");

			ThreatState[1] = 1;
		}
		if (CurrentThreat < 75 && Threat >= 75) {
			//Increasing from 2 to 3
			Debug.Log("Threat increasing to high");

			ThreatState[2] = 1;
		}
		if (CurrentThreat < 100 && Threat >= 100) {
			//Increasing from 3 to 4
			Debug.Log("Threat increasing to fucked");

			ThreatState[3] = 1;
		}
		if (CurrentThreat >= 100 && Threat < 100) {
			//Decrease from 4 to 3 (shouldn't happen)
			ThreatState[3] = -1;
		}
		if (CurrentThreat >= 75 && Threat < 75) {
			//Decrease from 3 to 2
			ThreatState[2] = -1;
		}
		if (CurrentThreat >= 50 && Threat < 50) {
			//Decrease from 2 to 1
			ThreatState[1] = -1;
		}
		if (CurrentThreat >= 25 && Threat < 25) {
			//Decrease from 1 to 0
			ThreatState[0] = -1;
		}
		CurrentThreat = Threat;

	}

	// Use this for initialization
	void Start () {
		//As we start, all tracks are silent

	}
	
	// Update is called once per frame
	void Update () {
		//This theoretically keeps tracks in sync
		ThreatSources[1].timeSamples = ThreatSources[0].timeSamples;
		ThreatSources[2].timeSamples = ThreatSources[0].timeSamples;
		ThreatSources[3].timeSamples = ThreatSources[0].timeSamples;
		if (Testing) {
			if (CurrentThreat > 120 || CurrentThreat < 0) {
				AutoIncrement = -AutoIncrement;
			}
			float NewThreat = CurrentThreat + AutoIncrement;
			CheckThreat (NewThreat);
		}
		ChangeVolume ();

	}
}
