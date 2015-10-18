using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BeamRicochet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private bool _isQuitting;

    public void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = Random.insideUnitCircle*5.0f;
        
        Destroy(gameObject, 1.0f);
    }

    public void FixedUpdate()
    {
        _rigidbody2D.velocity *= 0.95f;
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
