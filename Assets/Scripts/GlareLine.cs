using UnityEngine;
using System.Collections;

public class GlareLine : MonoBehaviour {
    public LineRenderer LineRenderer;

    public enum Mode
    {
        Idle, Wall, Seen
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
                LineRenderer.SetColors(Color.gray, new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.25f));
                break;

            case Mode.Wall:
                LineRenderer.SetColors(Color.gray, new Color(Color.gray.r, Color.gray.g, Color.gray.b, 0.25f));
                break;

            case Mode.Seen:
                LineRenderer.SetColors(Color.red, new Color(Color.red.r, Color.red.g, Color.red.b, 0.25f));
                break;
        }
    }
}
