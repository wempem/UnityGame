using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeSpell : Spell {

	[SerializeField]
	private Collidable aoeSpell;
	private Transform position;

	protected override void OnCollide(Collider2D coll)
	{
		if (coll.name == "Player" || coll.name == "Hitbox" || coll.tag == "Spell")
			return;
		if (coll.tag == "Fighter")
		{
			position = gameObject.transform;
			// Create a new Damage object, then set to fighter that has been hit
			Damage dmg = new Damage
			{

				damageAmount = spellDamage[spellLevel],
				origin = transform.position,
				pushForce = pushForce[spellLevel]
				
			};
			coll.SendMessage("RecieveDamage", dmg);
			Instantiate(aoeSpell, position.position, Quaternion.identity);

			Debug.Log(coll.name);

		}
		
		Destroy(gameObject);
		
	}
}
