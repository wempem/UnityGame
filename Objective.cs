using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour {

	public string sceneName;
	public string objectiveDescription;
	public int completion;
	public int currentProgress;
	public bool enemyDestory = false;

	public bool Completed()
	{
		if (currentProgress == completion)
			return true;
		else
			return false;
	}
}
