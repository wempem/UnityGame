using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour {

	private void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("sWag");
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("DUMP");
	}
}
