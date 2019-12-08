using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCrate : Fighter {

	[SerializeField]
	private Door door;
	private CheckForCrates check;
	private void Awake()
	{
		check = door.GetComponent<CheckForCrates>();
	}
	protected override void Death()
	{
		check.RemoveCrate();
		Destroy(gameObject);
	}

}
