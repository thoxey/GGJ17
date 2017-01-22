using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//Based off of boiler plate from Unity
public class PlayerController : MonoBehaviour 
{
	#region Gloabls
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1f;

	public Collider2D ground;
	public Collider2D leftSide;
	public Collider2D rightSide;

	public Transform sword;

	public int health;

	public GameObject hands;
	public Sprite defualtHands;

	[HideInInspector] public int range, ammo, damage;
	public int enemiesKilled = 0;

	bool doubleJump = true;
	public Transform groundCheck;
	float downV;
	float olddownV;

	int oldTimer = 0;
	int timer = 0;

	bool coolDown = true;
	bool grounded = false;
	bool sided = false;
	bool onHit = false;
	private Rigidbody2D rb2d;

	GameObject[] platforms;

	const int LEFT = 0;
	const int RIGHT = 1;
	const int sideBuffer = 1;

	const int DEFAULT_RANGE = 2;
	const int DEFAULT_DAMAGE = 3;


	#endregion

		

	void rotateSword()
	{
		Vector3 rotation = new Vector3(0, 0, 30);
		sword.Rotate(rotation*Time.deltaTime*30);
		if(sword.localRotation.z < 0f && sword.localRotation.z > -2)
		{
			onHit = false;
			sword.localEulerAngles = new Vector3(0,0,0);
		}
	}

	void useWeapon(int _side, int _range)
	{
		int lor;
		int buffer = sideBuffer;
		if (_side == RIGHT) 
		{
			//Debug.Log("Shot Right");
			lor = _range;
		} 
		else
		{
			//Debug.Log("Shot Left");
			buffer *= -1;
			lor = -1*_range;
		}
		Vector3 origin = new Vector3(transform.position.x+buffer, transform.position.y, 0);
		Vector3 dir = new Vector3(transform.position.x+lor, transform.position.y, 0);
		Debug.DrawLine (origin, dir, Color.white, 3f, false);
		RaycastHit2D[] hit = Physics2D.RaycastAll (origin, dir, Mathf.Abs(_range));
		foreach(RaycastHit2D _hit in hit)
		{
//			Debug.Log(_hit.distance);
//			Debug.Log(origin);
//			Debug.Log(transform.position);
//			Debug.Log(_hit.collider.gameObject.name);
			if(_hit.collider != null && _hit.collider.tag == "Enemy")
			{
				if(_hit.collider.tag == "Enemy")
				{
					onHit = true;
					enemiesKilled++;
					if(coolDown)
					{
						_hit.collider.gameObject.transform.localScale = 
						new Vector3(_hit.collider.gameObject.transform.localScale.x, 
									_hit.collider.gameObject.transform.localScale.y,
									transform.localScale.z-damage);
						_hit.collider.gameObject.GetComponent<EnemyStats>().health -= damage;
						coolDown = false;
						if(ammo != 0)
						{
							ammo--;
						}
					}
				}
			}
		}
	}



	// Use this for initialization
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		platforms = GameObject.FindGameObjectsWithTag ("Floor");
		range = DEFAULT_RANGE;
		damage = DEFAULT_DAMAGE;
	}

	// Update is called once per frame
	void Update () 
	{
		if (oldTimer != (int)Time.realtimeSinceStartup) 
		{
			oldTimer = (int)Time.realtimeSinceStartup;
			//Debug.Log (player.GetComponent<PlayerController> ().enemiesKilled);
			timer++;
		}
		if(timer%2 == 0)
		{
			coolDown = true;
		}
		if(onHit)
		{
			rotateSword();
		}
		
		grounded = false;
		sided = true;
		foreach(GameObject platform in platforms)
		{
			if(rb2d.IsTouching(platform.GetComponent<Collider2D>()))
			{
				grounded = true;
				doubleJump = true;
			}
			if(rb2d.IsTouching(leftSide) || rb2d.IsTouching(rightSide))
			{
				sided = false;
			}
		}
		if (Input.GetKeyDown(KeyCode.UpArrow) && (grounded || doubleJump))
		{
			if(doubleJump)
			{
				doubleJump = false;
			}
			jump = true;
		}
		if(grounded)
			doubleJump = true;
		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			transform.position = new Vector3(transform.position.x,transform.position.y+0.05f,transform.position.z);
		}


	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");

		if ((h * rb2d.velocity.x < maxSpeed) && sided)
		{
			rb2d.AddForce(Vector2.right * h * moveForce);
		}
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2(Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		if (jump)
		{
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
		if (Input.GetKeyDown (KeyCode.Z)) 
		{
			useWeapon(LEFT, range);
		}
		if (Input.GetKeyDown (KeyCode.X)) 
		{
			useWeapon(RIGHT, range);
		}
		if(ammo == 0)
		{
			hands.GetComponent<SpriteRenderer>().sprite = defualtHands;
			range = DEFAULT_RANGE;
			damage = DEFAULT_DAMAGE;
		}
		if(health <= 0)
		{
			SceneManager.LoadScene("Game Over");
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Weapon")
		{
			range = col.gameObject.GetComponent<Weapon>().range;
			ammo = col.gameObject.GetComponent<Weapon>().ammo;
			damage = col.gameObject.GetComponent<Weapon>().damage;
			col.gameObject.GetComponent<Weapon>().spawned = false;
			hands.GetComponent<SpriteRenderer>().sprite = col.gameObject.GetComponent<Weapon>().hands;
			col.gameObject.SetActive(false);
		}
		if(col.gameObject.tag == "Heart")
		{
			health += 400;
			col.gameObject.transform.position = new Vector3(Random.Range(-7.0f, 7.0f), 100, 0);
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}