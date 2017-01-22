using UnityEngine;
using System.Collections;

public class BackGroundWaves : MonoBehaviour {
	public float radius = 1;
	public float speed;
	float angle = 0;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(radius*Mathf.Cos(angle),(radius*Mathf.Sin(angle)/2), transform.position.z);
		angle+=Time.deltaTime*speed;
		if(angle >= 360)
		{
			angle = 0;
		}
	}
}
