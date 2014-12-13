using UnityEngine;
using System.Collections;

public class zombieAttack : MonoBehaviour {
	public float timeBetweenAttacks = 0.5f;
	public int attackDamage = 10;
	
	
	Animator anim;
	GameObject player;
	playerHealth playerHealth;
	zombieHealth zombieHealth;
	bool playerInRange;
	float timer;
	
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent <playerHealth> ();
		zombieHealth = GetComponent<zombieHealth>();
		anim = GetComponent <Animator> ();
	}
	
	
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && playerInRange && zombieHealth.currentHealth > 0)
		{
			Attack ();
		}
		
		if(playerHealth.currentHealth <= 0)
		{
			anim.SetTrigger ("PlayerDead");
		}
	}
	
	
	void Attack ()
	{
		timer = 0f;
		
		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage (attackDamage);
		}
	}
}
