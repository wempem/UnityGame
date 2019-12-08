using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Collectable {

	float hitPointGain = 0;
	int hitPoints = 0;

	protected override void OnCollect()
	{
		if (!collected)
		{
			collected = true;
			hitPointGain = GameManager.instance.player.maxHitPoint *0.2f;
			hitPoints = (int)hitPointGain;
			GameManager.instance.player.Heal(hitPoints);
			Destroy(gameObject);
		}

	}
}
