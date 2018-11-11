using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public Sprite currentSprite;
	public string spriteDesc;
	public Sprite idleSprite;
	public Sprite walkingSprite;
	public int animationCounter;

	// Use this for initialization
	void Start () {
		this.spriteRenderer = GetComponent<SpriteRenderer>(); //current renderer wıth sprıtes loaded
		this.currentSprite = this.spriteRenderer.sprite;
		this.spriteDesc = this.currentSprite.name; //prınts anım name
		this.idleSprite = Resources.Load("princess_walk_1", typeof(Sprite)) as Sprite;
		this.walkingSprite = Resources.Load("princess_walk_2", typeof(Sprite)) as Sprite;
		this.animationCounter = 0;
		//Debug.Log (spriteDesc);
		//print(spriteDesc);
		
	}
	
	// Update is called once per frame
	void Update () {
		//this.currentState;	

		if ((Input.GetKey (KeyCode.UpArrow)) ||
		    (Input.GetKey (KeyCode.DownArrow)) ||
		    (Input.GetKey (KeyCode.LeftArrow)) ||
			(Input.GetKey (KeyCode.RightArrow))) {

			animationCounter++;
			//Debug.Log (animationCounter);

			if (animationCounter >= 15) {
				changeAnim ();
			}

		} else {
			stopAnim ();
		}
	}

	public void changeAnim() {
		//Debug.Log ("inChangeAnim");
		if (this.spriteDesc == "princess_walk_1") 
		{
			this.spriteRenderer.sprite = this.walkingSprite;

			this.spriteDesc = "princess_walk_2";
		} else if (this.spriteDesc == "princess_walk_2") 
		{
			this.spriteRenderer.sprite = this.idleSprite;

			this.spriteDesc = "princess_walk_1";
		}

		animationCounter = 0;
	}

	public void stopAnim() {
		this.spriteRenderer.sprite = this.idleSprite;
		this.currentSprite = this.idleSprite;
		this.spriteDesc = "princess_walk_1";
	}
}
