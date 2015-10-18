using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class BeamProjectile : MonoBehaviour
{
    [SerializeField]
    public Hero Source;

	public Explosion blast;

    [SerializeField]
    public float Speed;

    [SerializeField]
    public float Direction;

    [SerializeField]
    public float Life;
    
    [SerializeField]
    public Transform TrailHolder;

    [SerializeField]
    public float TrailJitterMagnitude;

    [SerializeField]
    public BeamRicochet RicochetEffect;

    [SerializeField] 
    public int RicochetAmount;

    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    private float _lastTravelAngle;

    private bool _isQuitting;

    public void Reset()
    {
        Speed = 20.0f;
        Direction = 0.0f;
        Life = 5.0f;
        TrailJitterMagnitude = 0.5f;
        RicochetAmount = 5;
    }

    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(Mathf.Cos(Direction*Mathf.Deg2Rad)*Speed, Mathf.Sin(Direction*Mathf.Deg2Rad)*Speed);
        
        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
        transform.localScale = new Vector3(transform.localScale.x, 
            transform.localScale.y*(TrailJitterMagnitude/_collider2D.bounds.extents.y),
            transform.localScale.z);
    }

    public void Update()
    {
        Life -= Time.deltaTime;
        if (Life <= 0.0f) Destroy(gameObject);

        _lastTravelAngle = Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x)*Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            transform.eulerAngles.y,
            _lastTravelAngle);

        TrailJitter();
    }

    public void TrailJitter()
    {
        var magnitude = Random.Range(-TrailJitterMagnitude, TrailJitterMagnitude);
        var angle = (transform.eulerAngles.z + 90.0f)*Mathf.Deg2Rad;

        TrailHolder.position = transform.position + new Vector3(Mathf.Cos(angle)*magnitude, Mathf.Sin(angle)*magnitude);
    }

    public void OnTriggerEnter2D(Collider2D collider2D)
    {
        var beamProjectile = collider2D.GetComponent<BeamProjectile>();
        if (beamProjectile != null)
        {
            if (Source == beamProjectile.Source) return;

            Debug.Log("I CROSSED THE STREAMSSSSS");
            // Explode here
			Explosion explosion = Instantiate(blast);
			explosion.transform.position = beamProjectile.transform.position;
            Destroy(explosion.gameObject, 2);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    public void OnDestroy()
    {
        if (_isQuitting) return;
        for (var i = 0; i < RicochetAmount; i++)
        {
            var ricochet = Instantiate(RicochetEffect);
            ricochet.transform.position = transform.position;

            var ricochetAngle = _lastTravelAngle;
            ricochetAngle += Random.Range(-45.0f, 45.0f) + 180.0f;
            ricochetAngle *= Mathf.Deg2Rad;

            ricochet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(ricochetAngle), Mathf.Sin(ricochetAngle))
                *Random.Range(10.0f, 20.0f);
        }

        var trail = TrailHolder.GetComponent<TrailRenderer>();
        trail.transform.SetParent(null);
        trail.autodestruct = true;
    }
}

