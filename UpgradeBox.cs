using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeBox : MonoBehaviour {


	public Text spellName, spellText;

	private Text gameText;
	private string objectName;
	private GameObject spell;
   // public int spellLevel;

    public void UpdateMenu()
    {
		objectName = EventSystem.current.currentSelectedGameObject.name;
		gameText = GameObject.Find(objectName).GetComponentInChildren<Text>();
		spellName.text = objectName;	
		spellText.text = gameText.text;
	}
	public void upgradeSpell()
	{
		GameManager.instance.spellManager.TryUpgradeSpell(objectName);
	}
	public void equipSpell(int numSlot)
	{
		Debug.Log("Numslot is: " + numSlot);
		GameManager.instance.player.PopulateSpellBook(objectName, numSlot);
	}
}
