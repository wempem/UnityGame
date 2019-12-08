﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable {

	//damage struct
	public int[] damagePoint = { 1, 2, 3, 4, 5, 6, 7 };
	public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f};

	// Upgrade
	public int weaponLevel = 0;
	public SpriteRenderer spriteRenderer;

	// Swing
	private Animator anim;
	private float cooldown = 0.5f;
	private float lastSwing;

	protected override void Start()
	{
		base.Start();
		anim = GetComponent<Animator>();
	}

	protected override void Update()
	{
		base.Update();

		//if(Input.GetKeyDown(KeyCode.Space))
		//{
			//if(Time.time - lastSwing > cooldown)
			//{
				//lastSwing = Time.time;
				
               // GameManager.instance.player.CastSpell();
               
            //}
		//}
	}

	protected override void OnCollide(Collider2D coll)
	{
		if(coll.tag == "Fighter")
		{
			if (coll.name == "Player")
				return;
          
            // Create a new damage object, then set to fighter that has been hit
            Damage dmg = new Damage
			{
				damageAmount = damagePoint[weaponLevel],
				origin = transform.position,
				pushForce = pushForce[weaponLevel]
			};

			coll.SendMessage("RecieveDamage", dmg);
			Debug.Log(coll.name);
		}
	}

	private void Swing()
	{
		anim.SetTrigger("Swing");
	}
	public void UpgradeWeapon()
	{
		weaponLevel++;
		spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

		// Change Stats %%
	}

	public void SetWeaponLevel(int level)
	{
		weaponLevel = level;
		spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

	}
}
