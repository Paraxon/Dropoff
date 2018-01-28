using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour 
{
	public float speed = 0.02f;
	public float direction = 1;
	public GameObject drivingPoint;
	public Vector3 destanation;

	void Start () 
	{
		drivingPoint = GameObject.FindGameObjectWithTag("Driving Point");
		destanation = drivingPoint.transform.position;
		print(destanation); 
		print(transform.position);
	}
	

	void Update () 
	{
		if(Vector3.Distance(destanation, transform.position) < 1.5f)
		{

		}
		else
		{
			transform.Translate(transform.forward*speed*Time.deltaTime);
		}

	}
	public void OnTriggerEnter(Collider other)
	{
		Health otherhealth = other.GetComponent<Health>();
		if(otherhealth != null)
		{
			otherhealth.Kill();
		}
	}
}
