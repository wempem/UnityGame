using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeSpell : Spell
{

	private int countDmg = 3;
	private float dmgTime = 1.0f;
	


	protected override void OnCollide(Collider2D coll)
	{
		if (coll.name == "Player" || coll.name == "Hitbox" || coll.tag == "Spell")
			return;
		if (coll.tag == "Fighter")
		{

			// Create a new Damage object, then set to fighter that has been hit

			DamageOverTime dot = new DamageOverTime
			{
				
				damageAmount = spellDamage[spellLevel],
				origin = transform.position,
				pushForce = pushForce[spellLevel],
				time = dmgTime,
				damageCount = countDmg
			};
			coll.SendMessage("StartSwag", dot);

		}

		Destroy(gameObject);
	}

}
