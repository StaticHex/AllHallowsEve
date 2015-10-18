using UnityEngine;
using System.Collections;

public class GhostInjury : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer ("Lasers")) {
			BeamProjectile laser = other.gameObject.GetComponent<BeamProjectile>();
			Hero ghost = this.gameObject.GetComponent<Hero>();
			ghost.SlowDown(.9f);
			ghost.Injure(10);
		}
	}
}
