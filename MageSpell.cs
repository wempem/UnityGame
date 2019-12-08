using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpell : Collidable {

	private Rigidbody2D spellRigid;
	public float speed;
	private float closest = 1000;
	private float distanceFrom;
	// Modifiable depending on Spell.
	private MageScript[] gameObjects;
	private MageScript mage;
	public int[] spellDamage;
	public float[] pushForce;
	public int spellLevel = 1;
	public int maxSpellLevel = 4;
	
	private SpriteRenderer sprite;
	
	[SerializeField]
	private float timeBeforeDesturction;

	
	private Vector3 target2;
	// Use this for initialization
	protected override void Start()
	{
		
		sprite = GetComponent<SpriteRenderer>();
		IsLeft();
		base.Start();
		spellRigid = GetComponent<Rigidbody2D>();
		
	}

	// Update is called once per frame
	protected override void Update()
	{

		base.Update();
		
		Vector2 direction = target2 - transform.position;
		spellRigid.velocity = direction.normalized * speed;
		
		Destroy(gameObject, timeBeforeDesturction);
	}

	protected override void OnCollide(Collider2D coll)
	{
		if(coll.name != "Player" || coll.tag == "Spell")
		//if (coll.name == "Mage" || coll.name == "Hitbox")
			return;
		if (coll.name == "Player") {

			// Create a new Damage object, then set to fighter that has been hit
			Damage dmg = new Damage
			{

				damageAmount = spellDamage[spellLevel],
				origin = transform.position,
				pushForce = pushForce[spellLevel]
			};
			coll.SendMessage("RecieveDamage", dmg);


			Debug.Log(coll.name);

		}

		Destroy(gameObject);
	}

	protected void IsLeft()
	{
		gameObjects = FindObjectsOfType<MageScript>();
		mage = null;
		for (int i = 0; i < gameObjects.Length; i++){
			
			distanceFrom = Vector3.Distance(gameObject.transform.position, gameObjects[i].transform.position);
			//distanceFrom = Vector3.Distance(gameObjects[i].transform.position, gameObject.transform.position);
			if(distanceFrom < closest)
			{
				closest = distanceFrom;
				mage = gameObjects[i];
				//Debug.Log(mage.transform.position);
			}
		}

		if (GameManager.instance.player.transform.position.x > transform.position.x)
		{
			sprite.flipX = false;
			
			target2 = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, GameManager.instance.player.transform.position.z);
		}
		else
		{
			sprite.flipX = true;
			
			target2 = new Vector3(GameManager.instance.player.transform.position.x, GameManager.instance.player.transform.position.y, GameManager.instance.player.transform.position.z);
		}
	}

}

