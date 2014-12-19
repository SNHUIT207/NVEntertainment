using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {

	public float speed;

	void Update ()
	{
		rigidbody.velocity = transform.forward * speed;

	}
}
