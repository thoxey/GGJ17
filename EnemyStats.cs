using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour 
{
	public GameObject head;
	public GameObject hand;
	public GameObject body;

	public Sprite head1;
	public Sprite head2;

	public Sprite hand1;
	public Sprite hand2;

	public Sprite body1;
	public Sprite body2;
	public Sprite body3;
	public Sprite body4;
	public Sprite body5;

	public int health = 10;

	public void onReactivation()
	{
		int randomBody = (int)Random.Range(0.0f, 5.9f);
		int randomHead = (int)Random.Range(0.0f, 5.9f);
		int randomHand = (int)Random.Range(0.0f, 5.9f);

		if(randomHead %2 == 0)
			head.GetComponent<SpriteRenderer>().sprite = head1;
		else
			head.GetComponent<SpriteRenderer>().sprite = head2;

		if(randomHand %2 == 0)
			hand.GetComponent<SpriteRenderer>().sprite = hand1;
		else
			hand.GetComponent<SpriteRenderer>().sprite = hand2;

		switch(randomBody)
		{
			case 0 : body.GetComponent<SpriteRenderer>().sprite = body1; break;
			case 1 : body.GetComponent<SpriteRenderer>().sprite = body2; break;
			case 2 : body.GetComponent<SpriteRenderer>().sprite = body3; break;
			case 3 : body.GetComponent<SpriteRenderer>().sprite = body4; break;
			case 4 : body.GetComponent<SpriteRenderer>().sprite = body5; break;
		}
	}
}
