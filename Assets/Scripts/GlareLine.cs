using UnityEngine;
using System.Collections;

public class GlareLine : MonoBehaviour {
    public LineRenderer LineRenderer;
    private bool _updatedOnce;

    public enum Mode
    {
        Idle, Wall, Seen
    }

    public void Awake()
    {
        _updatedOnce = false;
    }

    public void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
    }

    public void Draw(Vector2 start, Vector2 end, Mode mode = Mode.Idle)
    {
        LineRenderer.SetVertexCount(2);
        LineRenderer.SetPosition(0, start);
        LineRenderer.SetPosition(1, end);

        switch (mode)
        {
            case Mode.Idle:
                LineRenderer.SetColors(new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.1f), 
                    new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.01f));
                break;

            case Mode.Wall:
                LineRenderer.SetColors(new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.1f),
                    new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.01f));
                break;

            case Mode.Seen:
                LineRenderer.SetColors(new Color(Color.red.r, Color.red.g, Color.red.b, Random.Range(1.00f, 0.75f)), 
                    new Color(Color.red.r, Color.red.g, Color.red.b, Random.Range(0.00f, 0.25f)));
                break;
        }
    }

    public void Update()
    {
        if(_updatedOnce) Destroy(gameObject);
        _updatedOnce = true;
    }
}
