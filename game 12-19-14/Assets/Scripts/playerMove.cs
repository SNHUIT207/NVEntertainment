using UnityEngine;
using System.Collections;

public class playerMove : MonoBehaviour {
	public float speed = 1f;
	public Boundary boundary;
	public class Boundary
	{
		public float xMin, xMax, zMin, zMax;
	}
	//These four variables are  for the shooting. Everything else is for moving.
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;

	Vector3 movement;
	Rigidbody playerRigidBody;
	Animator anim;
	int floorMask;
	float camRayLength = 100f;
	//things that happens when the game starts
	void Awake ()
	{
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidBody = GetComponent<Rigidbody> ();
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

		//Getting a lot of errors from this, I don't understand why.
		playerRigidBody.position = new Vector3 
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);
	}
	//This is supposed to make the player change direction when they follow the mouse, but it's not working yet.
	public Transform target;
	void Turning ()
	{
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		
		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 1f;
			
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			playerRigidBody.MoveRotation(newRotation);
	}	
	}
	//And now this calls the Move and Turning functions during the fixed update.
	void FixedUpdate ()
	{
		Move ();
		Turning ();
	}
	//This is for shooting, but it's not doing anything so far
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audio.Play ();
		}
	}
}
