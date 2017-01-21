using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[HideInInspector] public int damage;
	[HideInInspector] public int health = 10;
	[HideInInspector] public float healthBarSize;
	public Transform healthBar;
	public void updateHealthBar(int _max)
	{
		Debug.Log(_max+"/"+health);
		healthBarSize = _max/health*2;
		healthBar.localScale = new Vector3(healthBarSize, healthBar.localScale.y, healthBar.localScale.z);
	}
}
