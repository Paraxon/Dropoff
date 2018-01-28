using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public Transform element;
    public int score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Mathf.Min(16, score); i++)
        {
            element.GetChild(i).gameObject.SetActive(true);
        }
        int diff = score - 15;
        if (diff > 0)
        {
            element.GetChild(15).GetComponent<Text>().text = "+" + diff;
        }
	}
}
