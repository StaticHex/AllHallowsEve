using UnityEngine;
using System.Collections;

public class TimerControl : MonoBehaviour {

    // Use this for initialization
    public UnityEngine.UI.Text txt;
    public float sec;
    private string secTxt;
	Hero ghostPlayer;
    private SoundManager _soundManager;

	void Start ()
	{
	    _soundManager = FindObjectOfType<SoundManager>();
        // Set time limit for match (in seconds)
        sec = 300;

        // Format seconds with leading 0 if < 10
        secTxt = formatS(sec);

        // Set time text starting out
        txt = this.gameObject.GetComponent<UnityEngine.UI.Text>();
        txt.text = ((int)(sec / 60)).ToString()+":"+secTxt;
		foreach (var ghost in FindObjectsOfType<GhostInjury>()) {
			ghostPlayer = ((GhostInjury)ghost).gameObject.GetComponent<Hero> ();
		}
    }
	
	// Update is called once per frame
	void Update () {
		if(!ghostPlayer.isDead() && sec > 0) {
            // Format seconds
            secTxt = formatS(sec);

            // Update timer
            sec -= UnityEngine.Time.deltaTime;
            txt.text = ((int)(sec/60)).ToString()+":" +secTxt;
        }
        else
        {
            // ** placeholder for now; will eventually determine victory/defeat
			if (ghostPlayer.isDead()) {
				txt.text = "Humans have won";
			} else {
				txt.text = "Ghost has won";
			}

            _soundManager.StopLoop("raveyard");
            _soundManager.PlaySound("Game Win");

			Invoke("RestartGame", 7f);
        }
    }

    // Format seconds with leading 0 if < 10
    string formatS (double tSec) {
        string temp;
        if((int)(tSec % 60) < 10) {
            temp = "0" + ((int)(tSec % 60)).ToString();
        }
        else {
            temp = ((int)(tSec % 60)).ToString();
        }
        return temp;
    }

	void RestartGame () {
		Application.LoadLevel("ram_scene");
	}
}
