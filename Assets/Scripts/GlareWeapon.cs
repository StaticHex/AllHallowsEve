using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlareWeapon : MonoBehaviour {

    [SerializeField]
    public float AngleRange;

    [SerializeField]
    public LayerMask LayerMask;

    public void Reset()
    {
        AngleRange = 90.0f;
    }

    public void Update()
    {
        var angleMin = transform.eulerAngles.z - AngleRange / 2.0f;
        var angleMax = angleMin + AngleRange;

        List<Hero> playersHit = new List<Hero>();
        for(var i = angleMin; i <= angleMax; i += 5.0f)
        {
            // This is debug
            Debug.DrawRay(transform.position, new Vector2(Mathf.Cos(i * Mathf.Deg2Rad), Mathf.Sin(i * Mathf.Deg2Rad)), Color.gray); 

            var hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Cos(i*Mathf.Deg2Rad), Mathf.Sin(i*Mathf.Deg2Rad)));
            if (!hit) continue;

            var player = hit.transform.GetComponent<Hero>();
            if (player == null) continue;

            Debug.DrawLine(transform.position, hit.point); // This is debug

            if (!playersHit.Contains(player)) playersHit.Add(player);
        }

        foreach (var player in playersHit)
        {
            // STUN 'EM HERE
            Debug.Log(string.Format("hit {0}!", player.name));
        }
    }
}
