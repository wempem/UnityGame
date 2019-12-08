using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageScript : Mover {

	// Experience
	public int xpValue = 1;

	// Logic
	private float cooldown = 2.0f;
	private float lastAttack;
	public float triggerLength = 1;
	public float chaseLength = 5;
	private bool engagement;
	private bool playerAttacked = false;
	private bool collidingWithPlayer = false;
	private bool changeDirection = false;
	private Transform playerTransform;
	private Vector3 startingPosition;
	private SpriteRenderer spriteRenderer;
	private float lastTeleport;
	private float teleportCD = 7.5f;

	
	[SerializeField]
	private MageSpell spell;
	private float distance = 0.16f;
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
		SwapDirection();
		// Player in range?
		if ((Vector3.Distance(playerTransform.position, startingPosition) < triggerLength) || playerAttacked) {
			//Teleport();
			Attack();
		}
		else
			playerAttacked = false;

		// Check for overlaps
		CheckCollision();
	}
	
	protected override void Start()
	{
		base.Start();
		playerTransform = GameManager.instance.player.transform;
		startingPosition = transform.position;
		hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	protected override void Death()
	{
		Destroy(gameObject);
		GameManager.instance.GrantXp(xpValue);
		GameManager.instance.ShowText("+ " + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
	}
	private void SwapDirection()
	{
		if(playerTransform.position.x > transform.position.x)
		{
			spriteRenderer.flipX = true;
			startingPosition = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
		}
		if(playerTransform.position.x < transform.position.x)
		{
			spriteRenderer.flipX = false;
			startingPosition = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
		}
	}
	private void Attack()
	{
		if (Time.time - lastAttack > cooldown)
		{
			lastAttack = Time.time;
			Instantiate(spell, startingPosition, Quaternion.identity);
		}
	}
	private void CheckCollision()
	{
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
	private void Teleport()
	{
		if(Time.time - lastTeleport > teleportCD)
		{
			for (int i = 0; i < 50; i++)
			{
				UpdateMotor(new Vector3(playerTransform.position.x - 0.1f * i, playerTransform.position.y, playerTransform.position.z));
			}
			startingPosition = transform.position;
			lastTeleport = Time.time;
		}
		SwapDirection();
	}
}
