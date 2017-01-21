using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemies : MonoBehaviour {

	[HideInInspector] public List<GameObject> enemyPool;
	public GameObject enemyStorer;
	public GameObject enemyPrefab;
	public GameObject player;

	int maxEnemies = 30; //Max Enemies in the pool
	int enemiesInWave = 0; //Enemies Spawned In a wave
	int wavesize = 3; //Max Enemies to spawn before waiting
	int spawnrate = 3; //Seconds before new spawn
	int timer = 0;
	int oldTimer = 0;
	bool oneAtATime;
	int speed = 3;
	int maxHealth = 10;

	void generateEnemy(GameObject _enemy, int _side)
	{
		GameObject retenemy = (GameObject)Instantiate(_enemy);
		retenemy.transform.parent = enemyStorer.transform;
		if(_side%2 != 0)
			retenemy.transform.position = new Vector3(Random.Range(-7.0f, 0.0f), 7, 0);
		else
			retenemy.transform.position = new Vector3(Random.Range(0.0f, 7.0f), 7, 0);
		retenemy.SetActive (false);
		enemyPool.Add(retenemy);
	}
	void generatePool()
	{
		//Debug.Log ("Generating Pool");
		for (int i = 0; i < maxEnemies; i++)
		{
			generateEnemy (enemyPrefab, i);
		}
	}
	// Use this for initialization
	void Start () 
	{
		generatePool ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float step = speed * Time.deltaTime;
		//Once a second
		if (oldTimer != (int)Time.realtimeSinceStartup) 
		{
			oldTimer = (int)Time.realtimeSinceStartup;
			//Debug.Log (player.GetComponent<PlayerController> ().enemiesKilled);
			oneAtATime = true;
			timer++;
		}
		//New Wave
		if (player.GetComponent<PlayerController> ().enemiesKilled == wavesize) 
		{
			//Debug.Log ("End of Wave");
			timer = 1;
			wavesize ++;
			speed++;
			maxHealth++;
			enemiesInWave = 0;
			player.GetComponent<PlayerController> ().enemiesKilled = 0;
		}
		//Spawn Enemy
		if(timer % spawnrate == 0 && enemiesInWave < wavesize)
		{
			if (oneAtATime) 
			{
				if(enemiesInWave%2 != 0)
					enemyPool [enemiesInWave].transform.position = new Vector3(Random.Range(-7.0f, 0.0f), 7, 0);
				else
					enemyPool [enemiesInWave].transform.position = new Vector3(Random.Range(0.0f, 7.0f), 7, 0);
				enemyPool [enemiesInWave].GetComponent<Enemy>().health = maxHealth;
				enemyPool [enemiesInWave].SetActive (true);
				enemiesInWave++;
				oneAtATime = false;
			}
		}

		foreach (GameObject enemy in enemyPool) 
		{
			if (enemy.transform.position.x > player.transform.position.x) {
				Vector3 target = new Vector3 (player.transform.position.x + 2, player.transform.position.y, player.transform.position.z);
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, target, step);
			} 
			else 
			{
				Vector3 target = new Vector3 (player.transform.position.x - 2, player.transform.position.y, player.transform.position.z);
				enemy.transform.position = Vector3.MoveTowards (enemy.transform.position, target, step);
			}
			enemy.GetComponent<Enemy>().updateHealthBar(maxHealth);
			if(enemy.GetComponent<Enemy>().health <=0)
			{
				enemy.SetActive(false);
			}
		}

	}
}
