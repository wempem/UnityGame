using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
	
    public static GameManager instance;
	private bool onFirstLoad = true;
    private void Awake()
    {
		if(GameManager.instance != null)
		{
			Destroy(gameObject);
			Destroy(player.gameObject);
			Destroy(floatingTextManager.gameObject);
			Destroy(hud);
			Destroy(menu);
            Destroy(spellMenu);
            return;
		}
        instance = this;
		SceneManager.sceneLoaded += LoadState;
		SceneManager.sceneLoaded += OnSceneLoaded;


	}


	//Resources
	public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
	
	// References
	public Player player;
	
	public Weapon weapon;
	public FloatingTextManager floatingTextManager;
	public SpellManager spellManager;
	public ObjectiveManager objectiveManager;
	public RectTransform hitpointBar;
	public GameObject hud;
	public GameObject menu;
    public GameObject spellMenu;
    public Animator deathMenuAnimator;
	public GameObject objectiveText;
	// Logic
	public int gold;
	public int experience;
	
	// Floating Text
	public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
	{
		floatingTextManager.Show(msg, fontSize, color,position, motion, duration);
	}

    // Upgrade Weapon
    
    public bool TryUpgradeWeapon()
	{
		// is the weapon max Level?
		if (weaponPrices.Count  <= weapon.weaponLevel)
			return false;
		if(gold >= weaponPrices[weapon.weaponLevel])
		{
			gold -= weaponPrices[weapon.weaponLevel];
			weapon.UpgradeWeapon();
			return true;
		}
		return false;
	}

	// Hitpoint Bar
	public void OnHitpointChange()
	{
		float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
		hitpointBar.localScale = new Vector3(1, ratio, 1);
	}

	public int GetXpToLevel(int level)
	{
		int r = 0;
		int xp = 0;

		while(r < level)
		{
			xp += xpTable[r];
			r++;
		}
		return xp;
	}
	public void GrantXp(int xp)
	{
		int currLevel = GetCurrentLevel();
		experience += xp;
		if(currLevel < GetCurrentLevel())
		{
			OnLevelUp();
		}
	}
	public void OnLevelUp()
	{
		Debug.Log("Level Up!");
		player.OnLevelUp();
		OnHitpointChange();
	}
	//XP System
	public int GetCurrentLevel()
	{
		int r = 0;
		int add = 0;

		while(experience >= add)
		{
			add += xpTable[r];
			r++;

			if (r == xpTable.Count) // Max Level
				return r;
		}

		return r;
	}

	//Respawn

	public void Respawn()
	{
		deathMenuAnimator.SetTrigger("Hide");
		UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
		player.Respawn();
	}

	/*
	 * INT preferedSkin
	 * INT gold
	 * INT experience
	 * INT weaponlevel
	 */
	public void OnSceneLoaded(Scene s, LoadSceneMode mode)
	{
		objectiveManager.OnObjectiveChange(s.name);
		player.transform.position = GameObject.Find("SpawnPoint").transform.position;
		
	}

	public void SaveState()
	{
		string s = "";

		s += "0" + "|";
		s += gold.ToString() + "|";
		s += experience.ToString() + "|";
		//s += weapon.weaponLevel.ToString();
		// Change later to spellManager.spells.Count but for now, it is 2.
		for (int i = 0; i < 3; i++)
		{
			Debug.Log(spellManager.spells[i].spellLevel.ToString() + "|");
			s += spellManager.spells[i].spellLevel.ToString() + "|";
		}
		PlayerPrefs.SetString("SaveState", s);
	}

	public void LoadState(Scene s, LoadSceneMode mode)
	{
		if (onFirstLoad)
		{
			PlayerPrefs.DeleteKey("SaveState");
			onFirstLoad = false;
		}
		SceneManager.sceneLoaded -= LoadState;
		for (int i = 0; i < spellManager.spells.Count; i++)
		{
			spellManager.spells[i].spellLevel = 0;
		}
		menu.SetActive(false);
		if (!PlayerPrefs.HasKey("SaveState"))
			return;
		string[] data = PlayerPrefs.GetString("SaveState").Split('|');
		for(int i = 0; i < data.Length; i++)
		{
			Debug.Log("Data: " + data[i]);
		}
		// Change player skin
		gold = int.Parse(data[1]);
		// xp
		experience = int.Parse(data[2]);
		if(GetCurrentLevel() != 1)
			player.SetLevel(GetCurrentLevel());

		// Change the weapon level
		//weapon.SetWeaponLevel(int.Parse(data[3]));
		for (int i = 0; i < 3; i++)
		{
			// Change later on when more spells are added
			Debug.Log("Spell Level: " + (int.Parse(data[2 + i])));
			spellManager.spells[i].spellLevel = (int.Parse(data[3 + i]));
		}
	}
	public void TurnOffMouse()
	{
		if (GameManager.instance.player.inMenu == false)
		{	
			GameManager.instance.player.inMenu = true;
		}
		else
		{
			GameManager.instance.player.inMenu = false;
		}
	}
	public bool IfActiveMenu()
	{
		if (menu.activeSelf)
		{
			return true;
		}
		return false;
	}
	
}
