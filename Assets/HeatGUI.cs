using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatGUI : MonoBehaviour {

    public Transform heatGUI;
    PlayerController player;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        int size = heatGUI.childCount;
        int display = (int)(size * player.currentHeat / player.maxHeat);
        for (int i = 0; i < size; i++)
        {
            heatGUI.GetChild(i).gameObject.SetActive(i < display);
        }
    }
}
