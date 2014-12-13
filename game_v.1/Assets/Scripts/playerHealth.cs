using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour {

	//What I am trying to do here is make three hearts, and when the player gets hit, he loses a heart, and when he loses all the hearts he dies. I'm kind of stuck though.
	public int startingHealth = 100;
	public int currentHealth;
	public AudioClip deathClip;
	public int redScreen = 0;


	
	Animator anim;
	AudioSource playerAudio;
	playerMove playerMovement;
	playerShoot playerShooting;
	bool isDead;
	bool damaged;
	
	
	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <playerMove> ();
		playerShooting = GetComponentInChildren <playerShoot> ();
		currentHealth = startingHealth;


	}

	
	void Update ()
	{
		if(damaged)
		{
			redScreen+=1;
			//screen starts getting red
			startingHealth-=100;
		
		}
		else
		{

		}
		damaged = false;
	}
	
	
	public void TakeDamage (int amount)
	{
		damaged = true;
		
		currentHealth -= amount;

		
		playerAudio.Play ();
		
		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}
	
	
	void Death ()
	{
		isDead = true;
		
		playerShooting.DisableEffects ();
		
		anim.SetTrigger ("Die");
		
		playerAudio.clip = deathClip;
		playerAudio.Play ();
		
		playerMovement.enabled = false;
		playerShooting.enabled = false;
		//screen is completely red

	}
	
	
	public void RestartLevel ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
