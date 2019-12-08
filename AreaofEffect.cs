using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaofEffect : Collidable {

	public int[] spellDamage;
	public float[] pushForce;
	public float timeBeforeDesturction;
	protected override void Update()
	{
		base.Update();
		Destroy(gameObject, timeBeforeDesturction);
	}

	protected override void OnCollide(Collider2D coll)
	{
		if (coll.name == "Player" || coll.name == "Hitbox" || coll.tag == "Spell")
			return;
		if (coll.tag == "Fighter")
		{

			// Create a new Damage object, then set to fighter that has been hit
			Damage dmg = new Damage
			{

				damageAmount = spellDamage[GameManager.instance.spellManager.spells[2].spellLevel],
				origin = transform.position,
				pushForce = pushForce[GameManager.instance.spellManager.spells[2].spellLevel]
			};
			coll.SendMessage("RecieveDamage", dmg);


			Debug.Log(coll.name);

		}
		Destroy(gameObject);
	}

}
