using UnityEngine;
using System.Collections;

public class PlayersDisplay : MonoBehaviour
{
    [SerializeField] private HeroStatDisplay _baseHeroStatDisplay;

	// Use this for initialization
	void Start () {
	    foreach (var heroObject in FindObjectsOfType<Hero>())
	    {
	        var hero = (Hero) heroObject;
	        if (hero.GetComponent<BeamWeapon>() != null)
	        {
                var heroStatDisplay = Instantiate(_baseHeroStatDisplay);
                heroStatDisplay.Player = hero;
                heroStatDisplay.transform.SetParent(heroObject.transform);
	            heroStatDisplay.transform.position = heroObject.transform.position;
	        }
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
