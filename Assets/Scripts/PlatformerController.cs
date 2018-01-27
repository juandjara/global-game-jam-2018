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
	bool wallAtRight() {
		return Physics.Raycast(transform.position, Vector3.right, radius + raycastMargin);
	}
	bool wallAtLeft() {
		return Physics.Raycast(transform.position, Vector3.left, radius + raycastMargin);		
	}
	
	// Update is called once per frame
	void Update () {
		bool _isGrounded = isGrounded();
		bool _wallAtLeft = wallAtLeft();
		bool _wallAtRight = wallAtRight();
		if(Input.GetKeyDown("space")) {
			if(_isGrounded || _wallAtLeft || _wallAtRight) {
				body.velocity = Vector3.up * jumpSpeed;
			}
			if(_wallAtRight) {
				body.velocity += Vector3.left * moveSpeed * 0.5f;
			}
			if(_wallAtLeft) {
				body.velocity += Vector3.right * moveSpeed * 0.5f;
			}
		}
		float axis = Input.GetAxis("Horizontal");
		float move = axis * moveSpeed * Time.deltaTime;
		if(Mathf.Abs(axis) > 0.05 && Mathf.Abs(body.velocity.x) > 0) {
			body.velocity.Set(0, body.velocity.y, 0);
		}
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
