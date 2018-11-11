using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed;
	private Transform target;
	public int HP;
	private bool isDying;

	public SpriteRenderer spriteRenderer;
	public Sprite currentSprite;
	public string spriteDesc;
	public Sprite idleSprite;
	public Sprite walkingSprite;
	public int animationCounter;
	public int deathCounter;

	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		isDying = false;

		this.spriteRenderer = GetComponent<SpriteRenderer>(); //current renderer wıth sprıtes loaded
		this.currentSprite = this.spriteRenderer.sprite;
		this.spriteDesc = this.currentSprite.name; //prınts anım name
		this.idleSprite = Resources.Load("sausageMove0", typeof(Sprite)) as Sprite;
		this.walkingSprite = Resources.Load("sausageMove1", typeof(Sprite)) as Sprite;
		this.animationCounter = 0;
		this.deathCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (isDying) {
			this.deathCounter++;

			if (deathCounter % 7 == 0) {
				Death ();
			}
		}
		else {
			animationCounter++;
			if (animationCounter >= 15) {
				ChangeAnim ();
			}

			if (HP > 0) {
				transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			} else {
				isDying = true;
				this.spriteRenderer.sprite = Resources.Load("sausageDie00", typeof(Sprite)) as Sprite;
				this.spriteDesc = "sausageDie00";
			}
		}
	}

	void ChangeAnim() {
		if (this.spriteDesc == "sausageMove0") {
			this.spriteRenderer.sprite = this.walkingSprite;

			this.spriteDesc = "sausageMove1";
		} else if (this.spriteDesc == "sausageMove1") {
			this.spriteRenderer.sprite = this.idleSprite;

			this.spriteDesc = "sausageMove0";
		}

		animationCounter = 0;
	}

	void Death() {
		// animate death
		// disable collider

		if (this.spriteDesc == "sausageDie00") {
			this.spriteRenderer.sprite = Resources.Load("sausageDie02", typeof(Sprite)) as Sprite;			
			this.spriteDesc = "sausageDie02";
		} else if (this.spriteDesc == "sausageDie02") {
			this.spriteRenderer.sprite = Resources.Load("sausageDie04", typeof(Sprite)) as Sprite;					
			this.spriteDesc = "sausageDie04";
		} else if (this.spriteDesc == "sausageDie04") {
			this.spriteRenderer.sprite = Resources.Load("sausageDie06", typeof(Sprite)) as Sprite;		
			this.spriteDesc = "sausageDie06";
		} else if (this.spriteDesc == "sausageDie06") {
			this.spriteRenderer.sprite = Resources.Load("sausageDie08", typeof(Sprite)) as Sprite;		
			this.spriteDesc = "sausageDie08";
		} else if (this.spriteDesc == "sausageDie08") {
			this.spriteRenderer.sprite = Resources.Load("sausageDie10", typeof(Sprite)) as Sprite;		
			this.spriteDesc = "sausageDie10";
		} else if (this.spriteDesc == "sausageDie10") {
			this.spriteRenderer.sprite = Resources.Load("sausageDie12", typeof(Sprite)) as Sprite;		
			this.spriteDesc = "sausageDie12";
		} else if (this.spriteDesc == "sausageDie12") {
			MainManager.DeathIncrement ();
			MainManager.Destroy(transform.gameObject);
		} else {
			this.spriteDesc = "sausageDie00";
		}
	}
}
