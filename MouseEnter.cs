using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEnter : MonoBehaviour {

	public void OnMouseEnter()
	{
		GameManager.instance.TurnOffMouse();
	}
	public void OnMouseExit()
	{
		GameManager.instance.TurnOffMouse();
	}
}
