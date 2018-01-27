using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlatformerController : MonoBehaviour {

	public float jumpSpeed = 10;
	public float moveSpeed = 10;
	public float raycastMargin = 0.1f;
	public float dieLine = -2;
	private float distToGround;
	Rigidbody body;
	CapsuleCollider capsule;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		capsule = GetComponent<CapsuleCollider>();
		distToGround = capsule.bounds.extents.y;
	}

	bool isGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + raycastMargin);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown("space") && isGrounded()) {
			body.velocity = Vector3.up * jumpSpeed;
		}
		float move = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		transform.Translate(move, 0, 0);

		if(transform.position.y < dieLine) {
			Die();
		}
	}
	void Die() {
		// TODO go back to top view
	}
}
