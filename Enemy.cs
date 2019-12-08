using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover {

    // Experience
    public int xpValue = 1;

	// Logic
	
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
	private bool playerAttacked = false;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
	float randomValue;
    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

	protected override void RecieveDamage(Damage dmg)
	{
		base.RecieveDamage(dmg);
		playerAttacked = true;
	}

	protected virtual void FixedUpdate()
    {
        // Player in range or did the player attack?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
		{
			if ((Vector3.Distance(playerTransform.position, startingPosition) < triggerLength) || playerAttacked)
				chasing = true;

			if (chasing)
			{
				if (!collidingWithPlayer)
				{
					UpdateMotor((playerTransform.position - transform.position).normalized);
				}
			}
			else
			{
				UpdateMotor(startingPosition - transform.position);
			}
		}
		//return to original position
		else
		{
			UpdateMotor(startingPosition - transform.position);
			playerAttacked = false;
			chasing = false;
		}

		// Check for overlaps
		collidingWithPlayer = false;
		boxCollider.OverlapCollider(filter, hits);
		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i] == null)
				continue;

			if (hits[i].tag == "Fighter" && hits[i].name == "Player")
			{
					collidingWithPlayer = true;
			}
			hits[i] = null;
		}
	}

	protected override void Start()
	{
		base.Start();
		playerTransform = GameManager.instance.player.transform;
		startingPosition = transform.position;
		hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
	}
	protected override void Death()
	{
		base.Death();
		SpawnHealthPotion();
		Destroy(gameObject);
		GameManager.instance.GrantXp(xpValue);
		GameManager.instance.ShowText("+ " + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
	}
	private void SpawnHealthPotion()
	{

		randomValue = Random.value;
		Debug.Log(randomValue);
		if (randomValue < 0.3f)
		{
			GameObject HealthPotion = Instantiate(Resources.Load("HealthPotion"), transform.position, Quaternion.identity) as GameObject;
		}
	}

}
