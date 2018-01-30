using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlatformerController : MonoBehaviour {

    private bool handleControl = false;
	public float jumpSpeed = 10;
	public float moveSpeed = 10;
	public float raycastMargin = 0.1f;
	public float dieLine = -2;
	public GameObject[] shapes;
	public Material playerMaterial;

	private float distToGround;
	private float radius;
	Rigidbody body;
	CapsuleCollider capsule;
    
	void Start () {
		body = GetComponent<Rigidbody>();
		capsule = GetComponent<CapsuleCollider>();
		distToGround = capsule.bounds.extents.y;
		radius = capsule.bounds.extents.x;
		loadShape();
		loadMaterial();
	}

	void loadShape() {
		int shapeIndex = PlayerPrefs.GetInt("shape");
		GameObject shapePrefab = shapes[shapeIndex];
		Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		GameObject shape = Instantiate(shapePrefab, position, Quaternion.identity);
		shape.SetActive(false);
		shape.transform.parent = gameObject.transform;
		shape.SetActive(true);
	}
	void loadMaterial() {
		Renderer[] childRenderers = transform.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in childRenderers) {
			r.material = playerMaterial;
		}
	}

    bool isGrounded () {

        // Aquí he añadido unos raycast extra para poder saltar desde el borde de un bloque
        if (Physics.Raycast(new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), -Vector3.up, distToGround + raycastMargin) || 
            Physics.Raycast(new Vector3(transform.position.x + -1, transform.position.y, transform.position.z), -Vector3.up, distToGround + raycastMargin) ||
            Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), -Vector3.up, distToGround + raycastMargin)) {
            
            return true;
        }
        return false;

        //return Physics.Raycast(transform.position, -Vector3.up, distToGround + raycastMargin);
    }
	bool wallAtRight() {
        if (Physics.Raycast(transform.position, Vector3.right, radius + raycastMargin)) {
            handleControl = true;
            return true;
        }
		return false;
	}
	bool wallAtLeft() {
        if (Physics.Raycast(transform.position, Vector3.left, radius + raycastMargin)) {
            handleControl = true;
            return true;
        }
        return false;	
	}
	
	void FixedUpdate () {
        handleControl = false;
        bool _isGrounded = isGrounded();
		bool _wallAtLeft = wallAtLeft();
		bool _wallAtRight = wallAtRight();
        float axis = 0;
        

        if (Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.W)) {
			if(_isGrounded || _wallAtLeft || _wallAtRight) {
				body.velocity = Vector3.up * jumpSpeed;
			}
			if(_wallAtRight && !_isGrounded) { // He añadido la condición de que no esté en el suelo para salvar un error
				body.velocity += Vector3.left * moveSpeed *0.5f;
			}
			if(_wallAtLeft && !_isGrounded) {
				body.velocity += Vector3.right * moveSpeed *0.5f;
			}
		}
        if (!handleControl) { // Esta condición es para bloquear el control del personaje cuando salta contra un bloque, 
                              // porque como hemos visto, la gente no se entera de que no hay que usar el control del personaje contra los bloques
            axis = Input.GetAxis("Horizontal");
        }
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
		if(other.gameObject.tag == "Finish") {
			Win();
		}
	}

	public void Win() {
		PlayerPrefs.SetInt("winPlatform", 1);
        //PlayerPrefs.SetInt("Lista de infectados", 1);
        SceneManager.LoadScene("1");	
	}

	public void Die() {
		PlayerPrefs.SetInt("winPlatform", 0);
		SceneManager.LoadScene("1");
	}
}
