using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour
{
	//public float moveSpeed = 2f;		// The speed the enemy moves at.
	public float HP = 2f;					// How many times the enemy can be hit before it dies.
	public Sprite deadEnemy;			// A sprite of the enemy when it's dead.
	public Sprite damagedEnemy;			// An optional sprite of the enemy when it's damaged.
//	public AudioClip[] deathClips;		// An array of audioclips that can play when the enemy dies.
//	public GameObject hundredPointsUI;	// A prefab of 100 that appears when the enemy dies.
//	public float deathSpinMin = -100f;			// A value to give the minimum amount of Torque when dying
//	public float deathSpinMax = 100f;			// A value to give the maximum amount of Torque when dying


	private SpriteRenderer ren;			// Reference to the sprite renderer.
//	private Transform frontCheck;		// Reference to the position of the gameobject used for checking if something is in front.
	private bool dead = false;			// Whether or not the enemy is dead.
//	private Score score;				// Reference to the Score script.
	public GameObject healthObj;
	public SpriteRenderer healthBar;
	public bool healthScaleEnabled = false;				// The local scale of the health bar initially (with full health).

	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	public float maxhealth;
	
	void Awake()
	{
		// Setting up the references.
		ren = transform.GetComponent<SpriteRenderer>();
//		frontCheck = transform.Find("frontCheck").transform;
//		score = GameObject.Find("Score").GetComponent<Score>();
		if (healthScaleEnabled) {

			healthBar = healthObj.transform.Find ("HealthBar1").transform. GetComponent<SpriteRenderer>();;
			// Getting the intial scale of the healthbar (whilst the player has full health).
			healthScale = healthBar.transform.localScale;
			maxhealth = HP;
			if (maxhealth ==0){
				maxhealth = 1;
			}
		}
	}

	void FixedUpdate ()
	{
		// Create an array of all the colliders in front of the enemy.
//		Collider2D[] frontHits = Physics2D.OverlapPointAll(frontCheck.position, 1);

		// Check each of the colliders.
//		foreach(Collider2D c in frontHits)
//		{
			// If any of the colliders is an Obstacle...
//			if(c.tag == "Obstacle")
//			{
				// ... Flip the enemy and stop checking the other colliders.
//				Flip ();
//				break;
//			}
//		}

		// Set the enemy's velocity to moveSpeed in the x direction.
//		GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * moveSpeed, GetComponent<Rigidbody2D>().velocity.y);	

		// If the enemy has one hit point left and has a damagedEnemy sprite...
		if(HP == 1f && damagedEnemy != null)
			// ... set the sprite renderer's sprite to be the damagedEnemy sprite.
			ren.sprite = damagedEnemy;
			
		// If the enemy has zero or fewer hit points and isn't dead yet...
		if(HP <= 0 && !dead)
			// ... call the death function.
			Death ();
	}
	
	public void HurtEnemy(float damage)
	{
		// Reduce the number of hit points by one.
		HP = HP -damage;
		// Update what the health bar looks like.
		if(healthScaleEnabled)
		UpdateHealthBar();

	}

	public void HurtFriend(float damage)
	{
		// Reduce the number of hit points by one.
		HP= HP -damage;
		// Update what the health bar looks like.
		if(healthScaleEnabled)
		UpdateHealthBar();
		
	}

	void Death()
	{
		// Find all of the sprite renderers on this object and it's children.
		SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

		// Disable all of them sprite renderers.
		foreach(SpriteRenderer s in otherRenderers)
		{
			s.enabled = false;
		}

		// Re-enable the main sprite renderer and set it's sprite to the deadEnemy sprite.
		//ren.enabled = true;
		//ren.sprite = deadEnemy;

		// Increase the score by 100 points
//		score.score += 100;

		// Set dead to true.
		dead = true;

		// Allow the enemy to rotate and spin it by adding a torque.
//		GetComponent<Rigidbody2D>().fixedAngle = false;
//		GetComponent<Rigidbody2D>().AddTorque(Random.Range(deathSpinMin,deathSpinMax));

		// Find all of the colliders on the gameobject and set them all to be triggers.
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.isTrigger = true;
		}

		// Play a random audioclip from the deathClips array.
//		int i = Random.Range(0, deathClips.Length);
//		AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;
		Destroy (this.gameObject);
		// Instantiate the 100 points prefab at this point.
//		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
	}


	public void Flip()
	{
		// Multiply the x component of localScale by -1.
		Vector3 enemyScale = transform.localScale;
		enemyScale.x *= -1;
		transform.localScale = enemyScale;
	}

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		//healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - HP * 0.01f);
		if (maxhealth > 0) {
			healthBar.material.color = Color.Lerp (Color.green, Color.red, 1 - HP * 1 / maxhealth);

			// Set the scale of the health bar to be proportional to the player's health.
			//healthBar.transform.localScale = new Vector3(healthScale.x * HP * 0.01f, 1, 1);
			healthBar.transform.localScale = new Vector3 (healthScale.x * HP * (1 / maxhealth), 1, 1);
		}
	}

}
