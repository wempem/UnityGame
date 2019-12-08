using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : Mover {

	//For moving
	public bool preventMoving = false;

	//For Sprite changes
	private SpriteRenderer spriteRenderer;
	private SpriteRenderer spriteTemp;
	//For respawning
	private bool isAlive = true;
	
	//Casting
    private Vector3 startingPos;
	private Vector3 mousePos;
	private Vector3 size;
	private float distance = 0.16f;
	public bool inMenu = false;
	public bool canAttack = true;

	//private GameObject temp;

	// For spell book
	public List<Spell> spellBook;
	private Image image;
	public int skillPoints;
	private float[] spellCooldowns = { 0, 0, 0, 0, 0 };
	

    protected override void Start()
	{
		size = transform.localScale;
		base.Start();
		spriteRenderer = GetComponent<SpriteRenderer>();
		
	}

	protected override void RecieveDamage(Damage dmg)
	{
		if (!isAlive)
			return;
		base.RecieveDamage(dmg);
		GameManager.instance.OnHitpointChange();
	}

	protected override void Death()
	{
		isAlive = false;
		GameManager.instance.deathMenuAnimator.SetTrigger("Show");
	}

	private void FixedUpdate()
	{
		if (!preventMoving)
		{
			float x = Input.GetAxisRaw("Horizontal");
			float y = Input.GetAxisRaw("Vertical");
			if (isAlive)
			{
				UpdateMotor(new Vector3(x, y, 0));
			}
		}
		if (!GameManager.instance.IfActiveMenu())
		{
			Attack();
		}
	}
	
	public void SwapSprite(int skinId)
	{
		spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
	}
	public void OnLevelUp()
	{
		maxHitPoint++;
		skillPoints++;
		hitPoint = maxHitPoint;

	}
	public void SetLevel(int level)
	{
		for(int i = 0; i < level; i++)
		{
			OnLevelUp();
		}
	}
	public void Heal(int healingAmount)
	{
		if(hitPoint == maxHitPoint)
			return;
		hitPoint += healingAmount;
		if (hitPoint > maxHitPoint)
			hitPoint = maxHitPoint;
		GameManager.instance.ShowText("+" + healingAmount.ToString() + "hp", 25, Color.green, transform.position, Vector3.up, 1.0f);
		GameManager.instance.OnHitpointChange();

	}
	public void Respawn()
	{
		Heal(maxHitPoint);
		isAlive = true;
		lastImmune = Time.time;
		pushDirection = Vector3.zero;
	}
    public void CastSpell(int spellSlot)
    {
		WhereToCast();
        Instantiate(spellBook[spellSlot],startingPos, Quaternion.identity);
    }
	public void PopulateSpellBook(string spell, int slot)
	{
		Debug.Log(slot);
		for (int i = 0; i < spellBook.Count; i++) {
			if(spellBook[i].name == spell)
			{
				
				return;
			}
		}
		for (int i = 0; i < GameManager.instance.spellManager.spells.Count; i++)
		{
			
			if (GameManager.instance.spellManager.spells[i].name == spell)
			{
				
				spellBook[slot - 1] = GameManager.instance.spellManager.spells[i];

				image = GameManager.instance.spellManager.spellHotbar[slot - 1].GetComponent<Image>();
				spriteTemp = spellBook[slot - 1].GetComponent<SpriteRenderer>();

				image.sprite = spriteTemp.sprite;

				
				return;
			}
			
		}
		
		//spellBook[slot] = spell;
		// change the hotbars sprite
		// also change the cooldown of the cooldown bar
	}
	private void Attack()
	{
		if (Input.GetMouseButtonDown(0))
		{

			if (spellBook[0].name != "TestSpell")
			{
				Debug.Log(spellBook[0].name);
				if (Time.time - spellCooldowns[0] > spellBook[0].cooldown)
				{
					spellCooldowns[0] = Time.time;
					CastSpell(0);
				}
			}
		}
		if (Input.GetMouseButtonDown(1))
		{
			if (spellBook[1].name != "TestSpell")
			{
				if (Time.time - spellCooldowns[1] > spellBook[1].cooldown)
				{
					if (spellBook[1] == null)
						return;
					spellCooldowns[1] = Time.time;

					CastSpell(1);
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			if (inMenu == false)
				if (spellBook[2].name != "TestSpell")
				{
					if (Time.time - spellCooldowns[2] > spellBook[2].cooldown)
					{
						if (spellBook[2] == null)
							return;
						spellCooldowns[2] = Time.time;
						CastSpell(2);
					}
				}
		}
	}
	private void WhereToCast()
	{
		mousePos = Input.mousePosition;
		mousePos.z = 0.0f;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		if (left)
		{
			
			if (mousePos.x > transform.position.x)
			{
				left = false;
				transform.localScale = size;
				startingPos = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
				return;
			}
			
			startingPos = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
		}
		else
		{
			if (mousePos.x < transform.position.x)
			{
				left = true;
				transform.localScale = new Vector3(size.x * -1, size.y, size.z);
				startingPos = new Vector3(transform.position.x - distance, transform.position.y, transform.position.z);
				return;
			}
			
			startingPos = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
		}
	}
	public Vector3 WhereIstheMouse()
	{
		return mousePos;
	}
}

