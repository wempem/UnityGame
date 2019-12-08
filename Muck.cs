using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muck : Enemy {

	private List<Muck> muck;
	private int position = 0;
	private float range = 1.0f;
	private bool isMuck = false;
	public Vector3 currentPos;
	private Vector3 toNorm;
	private List<float> distances;
	private float distance = 0;
	[SerializeField]
	private GameObject bigBoiMuck;

	protected override void Start()
	{
		MuckInstantiate();
		DistanceCalculator();
		base.Start();

	}
	protected override void FixedUpdate()
	{
		if (isMuck)
		{
			if (Touching())
			{
				
				currentPos = this.transform.position;
				Destroy(gameObject);
				Destroy(muck[position].gameObject);
				Instantiate(bigBoiMuck, currentPos, Quaternion.identity);
			}
			else if (Vector3.Distance(muck[position].transform.position, gameObject.transform.position) < 0.40f)
			{
				UpdateMotor((muck[position].transform.position - gameObject.transform.position).normalized);
			}
			else base.FixedUpdate();
		}
		else base.FixedUpdate();
	}
	// finds all muck in scene and adds them to list.
	private void MuckInstantiate()
	{
		muck = new List<Muck>(Muck.FindObjectsOfType<Muck>());
		
		muck.Remove(this);
		if (muck.Count == 0)
		{
			isMuck = false;
		}
		else
		{
			isMuck = true;
			distances = new List<float>();
			for(int i = 0; i < muck.Count; i++)
			{
				distances.Add(0);
			}
		}
		
	}
	// Checks if mucks are touching
	private bool Touching()
	{
		if (Vector3.Distance(muck[position].transform.position, gameObject.transform.position) < 0.15f)
		{	
			return true;
		}
		return false;
	}
	// Calculates closes Muck to gameobject
	private void DistanceCalculator()
	{
		// if no mucks, return
		if (muck.Count == 0)
		{
			isMuck = false;
			return;
		}
		
		for (int i = 0; i < muck.Count; i++)
		{
			
			distances[i] = Vector3.Distance(gameObject.transform.position, muck[i].transform.position);
		}

		distance = distances[0];

		for (int i = 0; i < distances.Count; i++)
		{
			if (distance <= distances[i])
			{
				distance = distances[i];
				position = i;
			}
		}
	}
	// Muck death
	protected override void Death()
	{
		
		// Sends message to every muck to tell it that this muck is dead
		for (int i = 0; i < muck.Count; i++) {
			
			muck[i].SendMessage("OnDeath", gameObject.name);
		}
		base.Death();
	}
	// updates muck list and distance list on death. Recalculates closest muck. 
	private void OnDeath(string deadmuck)
	{
		for (int i = 0; i < muck.Count; i++)
		{		
			if (muck[i].name == deadmuck)
			{				
				muck.RemoveAt(i);
				distances.RemoveAt(i);
			}
		}
		DistanceCalculator();
	}
}
	

