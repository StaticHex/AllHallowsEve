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
    public float AmbientCooldownRate;

    private float _ambientCooldownTimer;

    [SerializeField]
    public float CooldownRate;

    [SerializeField]
    public float FireRate;

    private float _fireTimer;
    private bool _fired;
    
    [HideInInspector]
    public bool CanFire;

    [SerializeField]
    public BeamProjectile Projectile;

    public void Reset()
    {
        MaxHeat = 5.0f;
        FireRate = 10.0f;
        CooldownRate = 2.0f;
        AmbientCooldownRate = 1.0f;
    }

    public void Awake()
    {
        Heat = 0.0f;
        _fireTimer = 0.0f;
        CanFire = false;
        Overheated = false;
        _fired = false;
        _ambientCooldownTimer = 0.0f;
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
        newProjectile.Source = GetComponent<Hero>();

        _fireTimer = 0.0f;
        Heat += 1.0f/FireRate;
        _fired = true;
        _ambientCooldownTimer = 0.5f/FireRate;
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
            if (_fired)
            {
                _fired = false;
            }
            else if (_ambientCooldownTimer <= 0.0f)
            {
                Heat -= timestep*AmbientCooldownRate;
                if (Heat < 0.0f)
                    Heat = 0.0f;
            }
            else
            {
                _ambientCooldownTimer -= Time.deltaTime;
                if (_ambientCooldownTimer < 0.0f) _ambientCooldownTimer = 0.0f;
            }

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
