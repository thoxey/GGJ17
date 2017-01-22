using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {

	void Update()
	{
		if(transform.position.y < -3)
		{
			transform.position = new Vector3(Random.Range(-7.0f, 7.0f), 100, 0);
		}
	}
}
