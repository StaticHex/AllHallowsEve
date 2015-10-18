using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour {

    private HeroController _controller;
    private Rigidbody2D _rigidbody2D;

    [SerializeField]
    public float Speed;

    public void Reset()
    {
        Speed = 5.0f;
    }

	// Use this for initialization
	public void Start () {
        _controller = GetComponent<HeroController>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
        _rigidbody2D.velocity = new Vector2(Speed * _controller.HorizontalMovementAxis, Speed * _controller.VerticalMovementAxis);
	}
}
