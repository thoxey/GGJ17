using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	public int enemiesKilled = 0;

	bool doubleJump = true;
	public Transform groundCheck;
	float downV;
	float olddownV;

	bool grounded = false;
	bool sided = false;
	private Rigidbody2D rb2d;

	GameObject[] platforms;

	const int LEFT = 0;
	const int RIGHT = 1;
	const int sideBuffer = 1;

	#endregion

	void useWeapon(int _side, int _range)
	{
		int lor;
		int buffer = sideBuffer;
		_range = 2;
		if (_side == RIGHT) 
		{
			Debug.Log("Shot Right");
			lor = _range;
		} 
		else
		{
			Debug.Log("Shot Left");
			buffer *= -1;
			lor = -1*_range;
		}
		Vector3 origin = new Vector3(transform.position.x+buffer, transform.position.y, transform.position.z);
		Vector3 dir = new Vector3(transform.position.x+lor, transform.position.y, transform.position.z);
		Debug.DrawLine (origin, dir, Color.white, 3f, false);
		RaycastHit2D hit = Physics2D.Raycast (origin, dir, 20);
		if(hit.collider != null && hit.collider.tag == "Enemy")
		{
			enemiesKilled++;
			hit.collider.gameObject.SetActive(false);
		}
	}



	// Use this for initialization
	void Awake () 
	{
		rb2d = GetComponent<Rigidbody2D>();
		platforms = GameObject.FindGameObjectsWithTag ("Floor");
	}

	// Update is called once per frame
	void Update () 
	{
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
		if (Input.GetKeyDown (KeyCode.Z)) 
		{
			useWeapon(LEFT, 10);
		}
		if (Input.GetKeyDown (KeyCode.X)) 
		{
			useWeapon(RIGHT, 10);
		}



	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");

		if ((h * rb2d.velocity.x < maxSpeed) && sided)
			rb2d.AddForce(Vector2.right * h * moveForce);

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
	}


	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}