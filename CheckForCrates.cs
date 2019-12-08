using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForCrates : MonoBehaviour {

	private List<TutorialCrate> crates;
	private List<Door> doors;
	[SerializeField]
	private TutorialManager tut;
	private void Awake()
	{
		crates = new List<TutorialCrate>(FindObjectsOfType<TutorialCrate>());
		doors = new List<Door>(FindObjectsOfType<Door>());
		FindObjectOfType<Crate>();
	}
	private void Update()
	{
		if(crates.Count == 0)
		{
			for(int i = 0; i < doors.Count; i++)
			{
				doors[i].OpenDoor();
				if(doors.Count - 1 == i)
				{
					Destroy(doors[doors.Count - 1]);
				}
			}
			tut.TutorialCompletion(1);
		}
	}
	public void RemoveCrate()
	{
		
		crates.RemoveAt(crates.Count - 1);
	}
	
}
