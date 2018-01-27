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
	private float radius;
	Rigidbody body;
	CapsuleCollider capsule;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		capsule = GetComponent<CapsuleCollider>();
		distToGround = capsule.bounds.extents.y;
		radius = capsule.bounds.extents.x;
	}

	bool isGrounded() {
		return Physics.Raycast(transform.position, -Vector3.up, distToGround + raycastMargin);
	}
	bool canWallJump() {
		bool right = Physics.Raycast(transform.position, Vector3.right, radius + raycastMargin);
		bool left = Physics.Raycast(transform.position, Vector3.left, radius + raycastMargin);
		return right || left;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyDown("space") && (isGrounded() || canWallJump())) {
			body.velocity = Vector3.up * jumpSpeed;
		}
		float move = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
		transform.Translate(move, 0, 0);

		if(transform.position.y < dieLine) {
			Die();
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Respawn") {
			Die();
		}
	}

	public void Die() {
		SceneManager.LoadScene("platform");
	}
}
