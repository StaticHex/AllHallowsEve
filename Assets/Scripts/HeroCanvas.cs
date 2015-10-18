using UnityEngine;
using System.Collections;

public class HeroCanvas : MonoBehaviour
{
    public HeroStatDisplay HeroStatDisplay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            transform.eulerAngles.y,
            0.0f);
	}
}
