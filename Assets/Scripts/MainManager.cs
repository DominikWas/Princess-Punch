using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

	private static int enemiesDead = 0;
	public static bool charDeath = false;
	public AudioClip deathMusic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(charDeath){
			charDeath = false;
			charIsDead ();
		}
	}

	public void charIsDead() {
		AudioSource bgmusic = GetComponent<AudioSource> ();
		bgmusic.Stop ();
		bgmusic.clip = deathMusic;
		bgmusic.Play ();
	}

	public static void DeathIncrement(){
		enemiesDead++;
		Debug.Log ("ded sosage lulululul");
	}

}
