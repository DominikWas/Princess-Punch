using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	public float xAxis, yAxis;
	public Vector3 startPos; //prıncess spawns here ın bounds

	Rigidbody2D rb;
	BoxCollider2D col;
	Bounds bounds; 
	string spriteDesc;
	bool isFacingForwards;
	public SpriteRenderer spriteRenderer;
	public static bool isKicking = false;
	int animCount;
	public Sprite currentSprite;
	Animator animator;

	public int lives;
	private bool isDead;
	private bool isAlive = true;

	// Use this for initialization
	void Start () {
		this.rb = this.GetComponent<Rigidbody2D> ();
		this.col = this.GetComponent<BoxCollider2D> ();
		this.bounds = GetComponent<SpriteRenderer>().bounds;
		this.startPos = new Vector3(-8.0f, -3.0f, 0.0f); //left sıde of scene spawn - world coords.
		this.isFacingForwards = true;
		this.spriteRenderer = GetComponent<SpriteRenderer> ();
		this.spriteDesc = "princess_walk_1"; //prınts anım name
		this.spriteRenderer.sprite = Resources.Load (this.spriteDesc, typeof(Sprite)) as Sprite;

		this.animCount = 0;
		this.animator = GetComponent<Animator> ();

		this.transform.position = startPos; //set player posıtıon (defensıve prog)
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("aaaa");
		this.xAxis = Input.GetAxisRaw ("Horizontal");
		this.yAxis = Input.GetAxisRaw ("Vertical");

		if (lives < 1 && isAlive) 
		{
			isAlive = false;
			die ();
		} 

		else if(isAlive)
		{
			isKicking = false;

			if (Input.GetKey (KeyCode.UpArrow) && this.transform.position.y < 0.5f) 
			{
				this.transform.Translate (Vector3.up / 10);
			}

			if (Input.GetKey (KeyCode.DownArrow) && this.transform.position.y > -4.0f) 
			{
				this.transform.Translate (Vector3.down / 10);
			}

			if (Input.GetKey (KeyCode.LeftArrow) && this.transform.position.x > -10.0f) 
			{
				if (isFacingForwards) 
				{
					isFacingForwards = !isFacingForwards;
					this.spriteRenderer.flipX = true;
				}
				this.transform.Translate (Vector3.left / 10);
			}

			if (Input.GetKey (KeyCode.RightArrow) && this.transform.position.x < 9.5f) 
			{
				if (!isFacingForwards) 
				{
					isFacingForwards = !isFacingForwards;
					this.spriteRenderer.flipX = false;
				}
				this.transform.Translate (Vector3.right / 10);
			}

			if (Input.GetKeyDown (KeyCode.Z)) 
			{ //kicku
				isKicking = true;
				animator.SetTrigger("isKicking");
				StartCoroutine (kick ());
			}
		}
	}

	IEnumerator kick(){
		yield return new WaitForSeconds (.75f);
		isKicking = false;
	}

	void die(){
		animator.SetTrigger ("isDead");
		this.spriteDesc = "death6";
		this.spriteRenderer.sprite = Resources.Load(this.spriteDesc, typeof(Sprite)) as Sprite;
		Rigidbody2D rigid = this.GetComponent<Rigidbody2D> ();
		Destroy (rigid);

		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject enemy in gameObjects){
			Destroy (enemy);
		}

		MainManager.charDeath = true;

		AudioSource deathSFX = GetComponent<AudioSource> ();
		deathSFX.Play (0);

		animator.SetTrigger ("dying");
	}

	void OnCollisionEnter2D (Collision2D col) {
		if (col.gameObject.tag == "Enemy") {
			lives--;
			Rigidbody2D rigid = this.GetComponent<Rigidbody2D> ();
			Destroy (rigid);
			//Debug.Log ("rigid off");
			StartCoroutine (Invulnerability());
		}
	}
		
	IEnumerator Invulnerability() {
		yield return new WaitForSeconds (2);
		this.gameObject.AddComponent<Rigidbody2D>();
		this.GetComponent<Rigidbody2D> ().gravityScale = 0; //fixes new rigid body so sprite doesn't fall
		this.GetComponent<Rigidbody2D>().freezeRotation = true; //fixes rotation tilt after hit
		//Debug.Log ("rigid new");
	}
}
