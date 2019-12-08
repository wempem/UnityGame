using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {

	// Public
	public int hitPoint = 10;
	public int maxHitPoint = 10;
	public float pushRecoverySpeed = 0.2f;
	

	// Immunity
	protected float immuneTime = 1.0f;
	protected float lastImmune;
	private float elaspedTime;
	private bool onDot = false;
	// Push

	protected Vector3 pushDirection;

	// All fighters can RecieveDamage / Die
	protected virtual void RecieveDamage(Damage dmg)
	{
		//if(Time.time - lastImmune > immuneTime)
		//{
			lastImmune = Time.time; 
			hitPoint -= dmg.damageAmount;
			pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

			GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

			if (hitPoint <= 0)
			{
				hitPoint = 0;
				Death();
			}
		//}
	}
	
	protected virtual void Death()
	{
		if (GameManager.instance.objectiveManager.EnemyObjectiveCheck())
			GameManager.instance.objectiveManager.IncreaseProgression();
	}


	IEnumerator RecieveDamageOver(DamageOverTime dmg)
	{
		onDot = true;
		int currentCount = 0;
		while (currentCount < dmg.damageCount)
		{
			hitPoint -= dmg.damageAmount;
			GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.zero, 0.5f);

			if (hitPoint <= 0)
			{
				onDot = false;
				hitPoint = 0;
				Death();
			}
			yield return new WaitForSeconds(dmg.time);
			currentCount++;
		}
		onDot = false;
	}

	protected void StartSwag(DamageOverTime dmg)
	{
		StartCoroutine(RecieveDamageOver(dmg));
	}
}
