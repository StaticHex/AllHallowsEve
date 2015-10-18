using UnityEngine;
using System.Collections;

public class GhostCrossPlayer : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Heroes")) {
			Hero hero = other.gameObject.GetComponent<Hero>();
			hero.Freeze(100);
			Debug.Log ("Phase freeze!");
		}
	}
}