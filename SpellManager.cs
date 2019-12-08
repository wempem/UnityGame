using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour {

	// Master list for all spells
	// This will include all information about the spell levels, damage, etc.
	
	public List<Spell> spells;
	private string str;
	private string str2;

	public SpriteRenderer spriteRender;
	public List<GameObject> spellHotbar;
	private GameObject toUpgrade;

	
	
	
	// This function is to be called when the player wants to upgrade a spell.
	public bool TryUpgradeSpell(string spell)
    {	
		
		if (GameManager.instance.player.skillPoints == 0)
		{
			Debug.Log("Failed at 0");
			return false;
		}
        if(GameManager.instance.player.skillPoints >= 1)
        {
			Debug.Log("Made it in");

			
			for (int i = 0; i < spells.Count; i++)
			{
				str = spells[i].ToString();
				for(int j = 0; j < str.Length; j++)
				{
					if(str[j] == ' ')
					{
						break;
					}
					str2 += str[j];
				}
			
				if (str2 == spell)
				{
					if(spells[i].spellLevel == spells[i].maxSpellLevel)
					{
						//Maybe output to the upgrade box that the spell is at max level or something later on
						Debug.Log("Spell At Max Level");
						return false;
					}
					Debug.Log("Spell level increase");
					spells[i].spellLevel++;
					GameManager.instance.player.skillPoints--;
					UpdateSpellMenu();
					str = "";
					str2 = "";
					return true;
				}
			}
        }
		Debug.Log("Failed at end");
		return false;
    }

	public void UpdateSpellMenu()
	{

	}
	
}

