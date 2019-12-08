using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuckManager : MonoBehaviour {
	private List<Enemy> enemies;
	public TutorialManager tutorialManager;
	private ObjectiveManager objectiveManager;
	private void Awake () {
		enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
		objectiveManager = FindObjectOfType<ObjectiveManager>();
	}
	// Update is called once per frame
	private void Update () {
		if (GameManager.instance.player.spellBook[0].spellLevel == 1)
		{
			tutorialManager.TutorialCompletion(5);
			objectiveManager.IncreaseProgression();
		}
		if(enemies.Count != 0)
			EnemyUpdate();
		if(enemies.Count == 0)
		{
			tutorialManager.TutorialCompletion(2);
		}
	}
	private void EnemyUpdate()
	{
		enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
	}
}
