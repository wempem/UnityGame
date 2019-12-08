using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable {

	//Damage
	public float cooldown;
	private float lastAttack;
	public int damage = 1;
	public float pushForce = 5;

	protected override void OnCollide(Collider2D coll)
	{
		if (coll.tag == "Fighter" && coll.name == "Player")
		{
			if (Time.time - lastAttack > cooldown)
			{
				lastAttack = Time.time;
				// Create a new damage object, before sending it to the player
				Damage dmg = new Damage
				{
					damageAmount = damage,
					origin = transform.position,
					pushForce = pushForce
				};

				coll.SendMessage("RecieveDamage", dmg);
			}
		}
	}
}
