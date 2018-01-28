using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
	public int healt = 5;

	public void Damage(int damage)
	{
		healt -= damage;

	}
	public void Kill()
	{
		healt = 0;
	}
	public bool IsDeath()
	{
		return healt<= 0;
	}

}
