using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Heroes")) {
			Hero hero = other.gameObject.GetComponent<Hero> ();
			//call this function below after certain time (time explosion animation takes to reach radius)
			hero.Freeze(200);
		}
	}
}
