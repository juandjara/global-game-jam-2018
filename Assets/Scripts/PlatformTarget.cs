using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTarget : MonoBehaviour {

	public GameObject[] shapes;
	public Material[] colors;

	// Use this for initialization
	void Start () {
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
		int colorIndex = PlayerPrefs.GetInt("enemyColor");
		Material color = colors[colorIndex];
		Renderer[] childRends = transform.GetComponentsInChildren<Renderer>();
		foreach(Renderer r in childRends) {
			r.material = color;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
