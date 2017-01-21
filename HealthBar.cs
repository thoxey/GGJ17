using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour 
{

	public GameObject EnemyParent;

	void Update()
	{
		transform.localScale = new Vector3(EnemyParent.transform.localScale.z/5, transform.localScale.y, transform.localScale.z);
		if(transform.localScale.x <0)
		{
			transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);
		}
	}
}

