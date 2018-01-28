using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour 
{
	public float speed = 10000000.0f;
	public float direction = 1;
	public GameObject drivingPoint;
	public GameObject drivingtoPoint;
	public Vector3 destanation;

	void Start () 
	{
		
	}


	void Update () 
	{
		if(Vector3.Distance(drivingtoPoint.transform.position, transform.position) < 4.0f)
		{
			transform.position = new Vector3(drivingPoint.transform.position.x, transform.position.y, drivingPoint.transform.position.z);

		}
		else
		{
			transform.Translate(new Vector3(0,0,20*speed));
		}

	}
	public void OnTriggerEnter(Collider other)
	{
		
		if(other.name == "Player")
		{
			//other.GetComponent.GameOver();
			print("lol");
		}
	}
}
