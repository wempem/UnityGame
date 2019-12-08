using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectiveManager : MonoBehaviour {

	public List<Objective> masterList;
	private Objective currentObjective;
	private bool newObjective = false;
	public bool portalActive = false;
	private Text objectiveText;
	private Text objectiveStatus;

	public void OnObjectiveChange(string scene)
	{
		for(int i = 0; i < masterList.Count; i++)
		{
			if (masterList[i].sceneName == scene)
			{
				currentObjective = masterList[i];
				newObjective = true;

				objectiveText = GameManager.instance.objectiveText.GetComponent<Text>();
				objectiveText.text = currentObjective.objectiveDescription;
				objectiveStatus = objectiveText.transform.GetChild(0).GetComponent<Text>();

				if (currentObjective.currentProgress == currentObjective.completion)
					objectiveStatus.text = "";
				else
					objectiveStatus.text = currentObjective.currentProgress + " / " + currentObjective.completion;
				return;
			}
		}
		return;
	}
	private void Update()
	{
		if (newObjective)
		{
			if (currentObjective.Completed())
			{
				// Activate Portal to next level
				portalActive = true;
				newObjective = false;
			}
		}	
	}
	public void IncreaseProgression()
	{
		if (!currentObjective.Completed())
		{
			Debug.Log("Increasing Progression");
			currentObjective.currentProgress++;
			objectiveStatus.text = currentObjective.currentProgress + " / " + currentObjective.completion;
		}
	}
	public bool EnemyObjectiveCheck()
	{
		return currentObjective.enemyDestory;
	}
}
