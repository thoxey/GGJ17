using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	int oldTimer;
	[HideInInspector] public int timer = 0;
	GameObject spawner = null;


	public GameObject   weaponSpawner1;
	public GameObject   weaponSpawner2;
	public GameObject   weaponSpawner3;


	void Update()
	{
		if((int)Time.realtimeSinceStartup != oldTimer)
		{
			oldTimer = (int)Time.realtimeSinceStartup;
			timer++;
			Debug.Log(timer);
		}
		if(timer % 10 == 0)
			spawner = weaponSpawner1;
		else if(timer % 21 == 0)
			spawner = weaponSpawner2;
		else if(timer % 32 == 0)
			spawner = weaponSpawner3;
		else
			spawner = null;
			
		if(spawner != null)
		{
			Debug.Log(spawner.name);
			spawner.transform.gameObject.SetActive(true);
		}

	}
}
