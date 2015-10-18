using UnityEngine;
using System.Collections;

public class PlayersDisplay : MonoBehaviour
{
    [SerializeField] private HeroStatDisplay _baseHeroStatDisplay;
    [SerializeField] private GhostStatDisplay _baseGhostStatDisplay;

	// Use this for initialization
	void Start () {
	    foreach (var heroObject in FindObjectsOfType<Hero>())
	    {
	        var hero = heroObject;
	        if (hero.GetComponent<BeamWeapon>() != null)
	        {
                var heroStatDisplay = Instantiate(_baseHeroStatDisplay);
                heroStatDisplay.Player = hero;
                heroStatDisplay.transform.SetParent(null);
	            heroStatDisplay.transform.position = heroObject.transform.position;
	        } else if (hero.GetComponent<GhostInjury>() != null)
	        {
	            var ghostStatDisplay = Instantiate(_baseGhostStatDisplay);
	            ghostStatDisplay.Player = hero;
                ghostStatDisplay.transform.SetParent(null);
                ghostStatDisplay.transform.position = heroObject.transform.position;
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
