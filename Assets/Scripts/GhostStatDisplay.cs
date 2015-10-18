using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GhostStatDisplay : MonoBehaviour {

    [SerializeField]
    public Hero Player;

    [SerializeField]
    public Text Name;
    [SerializeField]
    public Image HealthBar;

	// Use this for initialization
	void Start ()
	{
	    Name.text = Player.name;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(!Player) Destroy(gameObject);

	    var healthScale = Player.health/Player.maxHealth;
        HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x,
            HealthBar.transform.localScale.y,
            healthScale);
    }

    public void LateUpdate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            transform.eulerAngles.y,
            0.0f);
        transform.position = Player.transform.position;
    }
}
