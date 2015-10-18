using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public string StartScene;
	
    public void Play()
    {
        Application.LoadLevel(StartScene);
    }
}
