using UnityEngine;
using System.Collections;

public class GameSoundManager : MonoBehaviour {
    public SoundManager SoundManager;
	// Use this for initialization
	void Start ()
	{
	    SoundManager.LoopSound("raveyard", 0.25f);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
