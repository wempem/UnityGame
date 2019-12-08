using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gosu : Enemy {

	[SerializeField]
	private GameObject gosuSmall;
	private BoxCollider2D smallCollider;
	private Transform gosuTransform;
	private Vector3 gosuSpawn;
	private Vector3 temp;
	protected RaycastHit2D placeable;
	private float increment;

	protected override void Death()
	{
		gosuTransform = transform;
		smallCollider = gosuSmall.GetComponent<BoxCollider2D>();
		
		for (int i = 0; i < 3; i++)
		{
			findSpawningPosition(i);
			Debug.Log(gosuSpawn);
			Instantiate(gosuSmall, gosuSpawn, Quaternion.identity);
		}
		base.Death();
	}
	private void findSpawningPosition(int num)
	{
		
		for (int i = 1; i < 100; i++)
		{
			//increment = i / 100;

			if (num == 0)
			{
				temp = gosuTransform.position;
				placeable = Physics2D.BoxCast(temp, smallCollider.size, 0, new Vector2(0, 0), 0, LayerMask.GetMask("Actor", "Blocking"));
				
			}
			else if (num == 1) {

				temp = new Vector3(gosuTransform.position.x - 0.2f - (0.1f * i), gosuTransform.position.y, gosuTransform.position.z + increment);
				placeable = Physics2D.BoxCast(temp, smallCollider.size, 0, new Vector2(0, 0), 0, LayerMask.GetMask("Actor", "Blocking"));
			
			}
			else if(num == 2)
			{

				temp = new Vector3(gosuTransform.position.x, gosuTransform.position.y + 0.2f + (0.1f * i), gosuTransform.position.z - increment);
				placeable = Physics2D.BoxCast(temp, smallCollider.size, 0, new Vector2(0, 0), 0, LayerMask.GetMask("Actor", "Blocking"));
			}
			if (placeable.collider == null)
			{
				gosuSpawn = temp;
				return;
			}
		}
		for (int i = 1; i < 100; i++)
		{
			//increment = i / 100;

			if (num == 0)
			{
				temp = gosuTransform.position;
				placeable = Physics2D.BoxCast(temp, smallCollider.size, 0, new Vector2(0, 0), 0, LayerMask.GetMask("Actor", "Blocking"));

			}
			else if (num == 1)
			{

				temp = new Vector3(gosuTransform.position.x + 0.2f + (0.1f * i), gosuTransform.position.y, gosuTransform.position.z + increment);
				placeable = Physics2D.BoxCast(temp, smallCollider.size, 0, new Vector2(0, 0), 0, LayerMask.GetMask("Actor", "Blocking"));

			}
			else if (num == 2)
			{

				temp = new Vector3(gosuTransform.position.x, gosuTransform.position.y - 0.2f - (0.1f * i), gosuTransform.position.z - increment);
				placeable = Physics2D.BoxCast(temp, smallCollider.size, 0, new Vector2(0, 0), 0, LayerMask.GetMask("Actor", "Blocking"));
			}
			if (placeable.collider == null)
			{
				gosuSpawn = temp;
				return;
			}
		}
	}

}
