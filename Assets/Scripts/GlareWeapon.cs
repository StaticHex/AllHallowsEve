using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlareWeapon : MonoBehaviour {

    [SerializeField]
    public float AngleRange;

    [SerializeField]
    public float Distance;

    [SerializeField]
    public LayerMask LayerMask;

    [SerializeField]
    private GlareLine _baseGlareLine;

    [HideInInspector]
    public GlareLine MinGlareLine;
    [HideInInspector]
    public GlareLine MaxGlareLine;

    [SerializeField]
    public bool DebugDrawLines;

    public void Reset()
    {
        AngleRange = 90.0f;
        Distance = 10.0f;
        DebugDrawLines = false;
    }

    public void Start()
    {
        InitializeGlareLines();
    }

    public void Update()
    {
        var angleMin = transform.eulerAngles.z - AngleRange / 2.0f;
        var angleMax = angleMin + AngleRange;

        // HACK
        angleMin += 90.0f;
        angleMax += 90.0f;

        List<Hero> playersHit = new List<Hero>();

        RaycastHit2D angleMinHit = default(RaycastHit2D), angleMaxHit = default(RaycastHit2D);

        for(var i = angleMin; i <= angleMax; i += 5.0f)
        {
            var hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(i*Mathf.Deg2Rad), Mathf.Sin(i*Mathf.Deg2Rad)),
                Distance, LayerMask);

            var end = (Vector2) transform.position +
                      new Vector2(Mathf.Cos(i*Mathf.Deg2Rad), Mathf.Sin(i*Mathf.Deg2Rad))*Distance;
            DrawGlareLine(hit, end);

            if (i == angleMin) angleMinHit = hit;
            angleMaxHit = hit;

            if (!hit)
            {
                if (DebugDrawLines)
                    Debug.DrawLine(transform.position,
                        (Vector2)transform.position +
                        new Vector2(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad)) * Distance, Color.gray);
                continue;
            }

            var player = hit.transform.GetComponent<Hero>();
            if (player == null)
            {
                if(DebugDrawLines)
                    Debug.DrawLine(transform.position, hit.point, Color.white);

                continue;
            }
            else
            {
                if(DebugDrawLines)
                    Debug.DrawLine(transform.position, hit.point, Color.green);
            }

            if (!playersHit.Contains(player)) playersHit.Add(player);
        }

        DrawGlareLine(angleMinHit, 
            transform.position + new Vector3(Mathf.Cos(angleMin*Mathf.Deg2Rad), Mathf.Sin(angleMin*Mathf.Deg2Rad))*Distance);
        DrawGlareLine(angleMaxHit, 
            transform.position + new Vector3(Mathf.Cos(angleMax*Mathf.Deg2Rad), Mathf.Sin(angleMax*Mathf.Deg2Rad))*Distance);

        foreach (var player in playersHit)
        {
            // STUN 'EM HERE
			if (!this.gameObject.GetComponent<Hero>().isDead()) {
				player.Freeze();
				Debug.Log(string.Format("hit {0}!", player.name));
			}
        }
    }

    public void InitializeGlareLines()
    {
        MinGlareLine = Instantiate(_baseGlareLine).GetComponent<GlareLine>();
        MaxGlareLine = Instantiate(_baseGlareLine).GetComponent<GlareLine>();
    }

    private void DrawGlareLine(RaycastHit2D hit, Vector2 end = default(Vector2))
    {
        var glareLine = Instantiate(_baseGlareLine);

        if (!hit)
        {
            glareLine.Draw(transform.position, end, GlareLine.Mode.Idle);
            return;
        }

        if (hit.transform.GetComponent<Hero>() != null)
        {
            glareLine.Draw(transform.position, hit.point, GlareLine.Mode.Seen);
            return;
        }

        glareLine.Draw(transform.position, hit.point, GlareLine.Mode.Wall);
    }
}
