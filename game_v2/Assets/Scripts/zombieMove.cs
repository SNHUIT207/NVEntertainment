using UnityEngine;
using System.Collections;

public class zombieMove : MonoBehaviour {

	Transform player;
	playerHealth playerHealth;
	zombieHealth enemyHealth;
	NavMeshAgent nav;
	
	
	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		playerHealth = player.GetComponent <playerHealth> ();
		enemyHealth = GetComponent <zombieHealth> ();
		nav = GetComponent <NavMeshAgent> ();
	}
	
	
	void Update ()
	{
		if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		{
			nav.SetDestination (player.position);

		}
		else
		{
			nav.enabled = false;
		}
	}
		

}
