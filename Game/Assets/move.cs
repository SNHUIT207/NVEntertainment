using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	Animator anim;
	Rigidbody2D playerRigidBody2D;
	int floorMask;
	float camRayLength = 100f;
	//running into an error here, it says "unexpected symbol '0'". Not sure why.
	public Quaternion LookRotation (0f,0f,0f,0f);
	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		anim.SetBool ("isMove", false);
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidBody2D = GetComponent<Rigidbody2D> ();
	}
	
	//This huge pile of code is the Move function.
	void Move () {
		Vector3 direction = Vector3.zero;
		//this makes the guy walk when you press keys.
		if (Input.GetKey (KeyCode.UpArrow) == true) {
			direction.y += 0.1f;
			anim.SetBool ("isMove", true);
		} else if (Input.GetKey (KeyCode.DownArrow) == true) {
			direction.y -= 0.1f;
			anim.SetBool ("isMove", true);
		}

		else if (Input.GetKey (KeyCode.RightArrow) == true) {
			direction.x += 0.1f;
			anim.SetBool ("isMove", true);
		}
		else if (Input.GetKey (KeyCode.LeftArrow) == true) {
			direction.x -= 0.1f;
			anim.SetBool ("isMove", true);
		} else {
			direction.x= 0;
			direction.y=0;
			anim.SetBool ("isMove",false);
		//That turns everything off if nothing is being pressed.
		}
	gameObject.transform.Translate (direction);
	}
	//This is supposed to make the player change direction when they follow the mouse, but it's not working yet.
	void Turning ()
	{
		Ray2D camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit2D floorHit;
		
		if (Physics2D.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidBody2D.MoveRotation (newRotation);
		}
	}
	//And now this calls the Move and Turning functions during the fixed update.
	void FixedUpdate ()
	{
		Move ();
		Turning ();
	}
}

