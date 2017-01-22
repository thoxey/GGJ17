using UnityEngine;
using System.Collections;

public class WeaponSpin : MonoBehaviour {

	public float radius = 1;
	float speed = 20;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(0, 0, Time.deltaTime*speed);
	}
}
