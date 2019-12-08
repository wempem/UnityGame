using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : Collidable
{
	private Rigidbody2D spellRigid;
    public float speed;
	// Modifiable depending on Spell.
	public int[] spellDamage;
	public float[] pushForce;
	public int spellLevel = 1;
	public int maxSpellLevel = 4;
	private float spellDistance = 0.64f;
	private SpriteRenderer sprite;
	private Vector3 mousePos;

	[SerializeField]
	private float timeBeforeDesturction;
	public float cooldown;

	private Vector3 target2;
    // Use this for initialization
    protected override void Start() {

		mousePos = GameManager.instance.player.WhereIstheMouse();
		sprite = GetComponent<SpriteRenderer>();
		base.Start();
        IsLeft();
        spellRigid = GetComponent<Rigidbody2D>();
		
        //target2 = new Vector3(GameManager.instance.player.transform.position.x + spellDistance, GameManager.instance.player.transform.position.y, GameManager.instance.player.transform.position.z);
		
    }

    // Update is called once per frame
    protected override void Update() {

		base.Update();
        Vector2 direction = (mousePos - transform.position).normalized;
        spellRigid.velocity = direction.normalized * speed;
        Destroy(gameObject, timeBeforeDesturction);    
    }
	
    protected override void OnCollide(Collider2D coll)
    {
		
        if (coll.name == "Player"|| coll.name == "Hitbox" || coll.tag == "Spell")
            return;
        if (coll.tag == "Fighter")
        {

			// Create a new Damage object, then set to fighter that has been hit
				Damage dmg = new Damage
				{

					damageAmount = spellDamage[spellLevel],
					origin = transform.position,
					pushForce = pushForce[spellLevel]
				};
				coll.SendMessage("RecieveDamage", dmg);
			
			
			Debug.Log(coll.name);
			Destroy(gameObject);
		}
      
        Destroy(gameObject);
    }
	
	protected void IsLeft()
    {
		if (GameManager.instance.player.left)
		{
			sprite.flipX = true;
			spellDistance *= -1;
		}
		else if (!GameManager.instance.player.left)
		{
			sprite.flipX = false;
			spellDistance = 0.64f;
		}
    }
}
