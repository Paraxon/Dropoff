using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
            GetComponent<Equip>().Drop();
	}

    void OnTriggerStay(Collider other)
    {
        Interactive interaction = other.GetComponent<Interactive>();
        if (interaction && Input.GetButtonDown("Fire1"))
        {
            
        }
    }
}
