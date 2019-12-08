using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public List <Tutorial> tutorials;
	public List<GameObject> buttons;
	private List<Button> buttonScripts;
	public GameObject toggle;
	private bool timeApplied = false;
	private float time = 0.0f;
	private Button temp;
	public int currentTut = 0;
	
	
	public Text tutorialDescription;
	private bool tutorialCompleted = false;
	
	private void Start()
	{
		
		buttonScripts = new List<Button>();
		for (int i = 0; i < buttons.Count; i++)
		{
			buttonScripts.Add(buttons[i].GetComponent<Button>());
		}
		
		currentTut = NextTutorial();
		if (currentTut != -1)
		{	
			tutorialDescription.text = tutorials[currentTut].description;
		}
	}

	private void Update()
	{

		if (currentTut == -1)
		{
			return;
		}
		if (tutorials[currentTut].wait)
		{
			if (!timeApplied)
			{
				time = Time.time;
				timeApplied = true;
			}
			DisplayMessage();
		}
		if (tutorials[currentTut].skippable)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				TutorialCompletion(currentTut);
			}
		}
	}

	private void Complete(int index)
	{
		//tutorials[index].runTutorial = false;
		tutorials[index].completed = true;
		currentTut = NextTutorial();
		if (currentTut != -1)
		{
			tutorialDescription.text = tutorials[currentTut].description;
			GameManager.instance.player.preventMoving = false;
			if (tutorials[currentTut].disable)
			{
				DisableButtons();
			}
			if (!tutorials[currentTut].disable)
			{
				EnableButtons();
			}
		}
	}
	private void ShowTutorial(int index)
	{
		//tutorials[index].runTutorial = true;
	}
	private int NextTutorial()
	{
		for (int i = 0; i < tutorials.Count; i++)
		{
		
			if(tutorials[i].completed == false)
			{
				if (tutorials[i].preventMoving)
					GameManager.instance.player.preventMoving = true;
				else
					GameManager.instance.player.preventMoving = false;
				return i;
			}
		}		
		tutorialDescription.text = "";
		return -1;
	}
	public void TutorialCompletion(int num)
	{		
		if (num == currentTut)
		{
			Complete(num);
		}
		else
			return;
	}
	private void DisableButtons()
	{
		
		for(int i = 0; i < buttonScripts.Count; i++)
		{
			buttonScripts[i].interactable = false;
			
		}

	}
	public void EnableButtons()
	{
		for (int i = 0; i < buttonScripts.Count; i++)
		{
			buttonScripts[i].interactable = true;
		}
	}
	private void DisplayMessage()
	{
		if (Time.time - time > 3.0f)
		{
			time = 0.0f;
			Complete(currentTut);
			timeApplied = false;
		}
	}
}


