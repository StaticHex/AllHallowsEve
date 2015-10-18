using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public GameObject ghost;
    public GameObject player;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void spawnPlayer(int x, int y) {
       player = (GameObject)GameObject.Instantiate(player, new Vector3(x, y, 0), Quaternion.identity);
    }

    void spawnGhost(int x, int y) {
        player = (GameObject)GameObject.Instantiate(ghost, new Vector3(x, y, 0), Quaternion.identity);
    }
}
