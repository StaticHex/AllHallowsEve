using UnityEngine;
using System.Collections;

public class BeamWeapon : MonoBehaviour {

    [SerializeField]
    public float MaxHeat;

    [HideInInspector]
    public float Heat;

    [HideInInspector]
    public bool Overheated;

    [SerializeField]
    public float CooldownRate;

    [SerializeField]
    public float FireRate;

    private float _fireTimer;
    
    [HideInInspector]
    public bool CanFire;

    [SerializeField]
    public BeamProjectile Projectile;

    public void Reset()
    {
        MaxHeat = 5.0f;
        FireRate = 10.0f;
        CooldownRate = 2.0f;
    }

    public void Awake()
    {
        Heat = 0.0f;
        _fireTimer = 0.0f;
        CanFire = false;
        Overheated = false;
    }

    public void Update()
    {
        UpdateTimer();
        if (Heat > MaxHeat)
        {
            CanFire = false;
            Overheated = true;
        }
    }

    public void Fire()
    {
        Fire(transform.eulerAngles.z);
    }

    public void Fire(float direction)
    {
        if (!CanFire) return;

        var newProjectile = Instantiate(Projectile).GetComponent<BeamProjectile>();
        newProjectile.Direction = direction;
        newProjectile.transform.position = transform.position;
        newProjectile.SourceID = GetInstanceID();

        _fireTimer = 0.0f;
        Heat += 1.0f/FireRate;
    }

    public void UpdateTimer()
    {
        UpdateTimer(Time.deltaTime);
    }

    public void UpdateTimer(float timestep)
    {
        _fireTimer += timestep;
        if (Overheated)
        {
            Heat -= timestep*CooldownRate;
            if (Heat <= 0.0f)
                Overheated = false;
        }
        else
        {
            if (_fireTimer >= 1.0f / FireRate)
            {
                CanFire = true;
                _fireTimer = 1.0f / FireRate;
            }
            else
            {
                CanFire = false;
            }
        }
    }
}
