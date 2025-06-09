using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour {

	BoxCollider2D legCollider;

	// Use this for initialization
	void Start () {
		legCollider = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (PlayerMovement.isKicking) 
		{
			legCollider.enabled = !legCollider.enabled;
		}
	}
}
