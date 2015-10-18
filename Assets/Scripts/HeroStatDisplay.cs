using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HeroStatDisplay : MonoBehaviour
{
    [SerializeField] public Hero Player;
    [SerializeField] public BeamWeapon Weapon;

    [SerializeField] public Text Name;
    [SerializeField] public Image WeaponBar;

    private Vector3 _positionLock;

	// Use this for initialization
	void Start ()
	{
	    Name.text = Player.name;
	    Weapon = Player.GetComponent<BeamWeapon>();

        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            transform.eulerAngles.y,
            0.0f);

	    _positionLock = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (!Player) Destroy(gameObject);

	    var heatScale = Weapon.Heat/Weapon.MaxHeat;
	    heatScale -= Random.Range(0.0f, 0.075f*heatScale);

        var heatColor = Weapon.Overheated
            ? Color.red
            : Color.Lerp(Color.cyan, Color.red, heatScale);

	    var jitterColor = Random.Range(0.0f, 0.15f)*(Weapon.Heat/Weapon.MaxHeat);
	    heatColor = new Color(heatColor.r - jitterColor, heatColor.g - jitterColor, heatColor.b - jitterColor, heatColor.a);

	    WeaponBar.transform.localScale = Vector3.Lerp(WeaponBar.transform.localScale,
	        new Vector3(
	            heatScale,
	            WeaponBar.transform.localScale.y,
	            WeaponBar.transform.localScale.z),
	        Time.deltaTime*20.0f);

	    WeaponBar.color = heatColor;
	}

    public void LateUpdate()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            transform.eulerAngles.y,
            0.0f);
        transform.position = Player.transform.position;
    }
}
