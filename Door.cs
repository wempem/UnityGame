using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Collidable {
	private BoxCollider2D doorCollider;
	private SpriteRenderer spriteRenderer;
	public Sprite openDoor;
	public bool collisionOn = true;

	protected override void Start()
	{
		base.Start();
		doorCollider = GetComponent<BoxCollider2D>(); 
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	protected override void Update()
	{
		if (!collisionOn)
		{
			return;
		}
		base.Update();
	}
	public void OpenDoor()
	{
		spriteRenderer.sprite = openDoor;
		collisionOn = false;
		doorCollider.enabled = !doorCollider.enabled;
	}
	protected override void OnCollide(Collider2D coll)
	{
		//GameManager.instance.player.OnCollide();
	}
}
