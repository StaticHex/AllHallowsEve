using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class BeamProjectile : MonoBehaviour
{
    [SerializeField]
    public float Speed;

    [SerializeField]
    public float Direction;
    
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

    public void Reset()
    {
        Speed = 20.0f;
        Direction = 0.0f;
        TrailJitterMagnitude = 0.5f;
        RicochetAmount = 5;
    }

    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = new Vector2(Mathf.Cos(Direction*Mathf.Deg2Rad)*Speed, Mathf.Sin(Direction*Mathf.Deg2Rad)*Speed);

        _collider2D = GetComponent<Collider2D>();
        _collider2D.isTrigger = true;
    }

    public void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,
            transform.eulerAngles.y,
            Mathf.Atan2(_rigidbody2D.velocity.y, _rigidbody2D.velocity.x)*Mathf.Rad2Deg);

        TrailJitter();
    }

    public void TrailJitter()
    {
        var magnitude = Random.Range(-TrailJitterMagnitude, TrailJitterMagnitude);
        var angle = (transform.eulerAngles.z + 90.0f)*Mathf.Deg2Rad;

        TrailHolder.position = transform.position + new Vector3(Mathf.Cos(angle)*magnitude, Mathf.Sin(angle)*magnitude);
    }

    public void OnDestroy()
    {
        for (var i = 0; i < RicochetAmount; i++)
        {
            var ricochet = Instantiate(RicochetEffect);
            ricochet.transform.position = transform.position;
        }
    }
}

