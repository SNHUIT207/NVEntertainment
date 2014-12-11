using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {
	public float speed = 6f;
	Vector3 movement;
	Rigidbody2D playerRigidBody2D;
	Animator anim;
	int floorMask;
	float camRayLength = 100f;
	//things that happens when the game starts
	void Awake ()
	{
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidBody2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent <Animator> ();
	}
	//This huge pile of code is the Move function.
	void Move () {
		Vector3 direction = Vector3.zero;
		//this makes the guy walk when you press keys.
		if (Input.GetKey (KeyCode.UpArrow) == true) {
			direction.y += 0.1f;

		} else if (Input.GetKey (KeyCode.DownArrow) == true) {
			direction.y -= 0.1f;
		}
		else if (Input.GetKey (KeyCode.RightArrow) == true) {
			direction.x += 0.1f;
		}
		else if (Input.GetKey (KeyCode.LeftArrow) == true) {
			direction.x -= 0.1f;

		} else {
			direction.x= 0;
			direction.y=0;
		//That turns everything off if nothing is being pressed.
		}
		gameObject.transform.Translate (direction);
	}
	//This is supposed to make the player change direction when they follow the mouse, but it's not working yet.
	public Transform target;
	void Turning ()
	{
		Ray2D camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D floorHit;
		
		if (Physics2D.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidBody2D.MoveRotation(newRotation);
	}	
	}
	//And now this calls the Move and Turning functions during the fixed update.
	void FixedUpdate ()
	{
		Move ();
		Turning ();
	}

}
