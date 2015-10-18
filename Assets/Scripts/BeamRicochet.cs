using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BeamRicochet : MonoBehaviour
{
    [SerializeField] private TrailRenderer _trail;
    private Rigidbody2D _rigidbody2D;

    private bool _isQuitting;

    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1.0f);
    }

    public void FixedUpdate()
    {
        _rigidbody2D.velocity *= 0.95f;
    }

    public void Update()
    {
        var magnitude = Random.Range(-0.1f, 0.1f);
        var angle = (transform.eulerAngles.z + 90.0f) * Mathf.Deg2Rad;

        _trail.transform.position = transform.position + new Vector3(Mathf.Cos(angle) * magnitude, Mathf.Sin(angle) * magnitude);
    }

    public void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    public void OnDestroy()
    {
        if (_isQuitting) return;

        var trail = GetComponent<TrailRenderer>();
        if (trail == null) return;
        trail.transform.SetParent(null);
        trail.autodestruct = true;
    }
}
