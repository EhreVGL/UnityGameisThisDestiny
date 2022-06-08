using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRoutePath : MonoBehaviour
{
    [SerializeField] private Transform[] checkPoints;
    private Vector2 gizmosPosition;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * checkPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * checkPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * checkPoints[2].position + Mathf.Pow(t, 3) * checkPoints[3].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(checkPoints[0].position.x, checkPoints[0].position.y), new Vector2(checkPoints[1].position.x, checkPoints[1].position.y));
        Gizmos.DrawLine(new Vector2(checkPoints[2].position.x, checkPoints[2].position.y), new Vector2(checkPoints[3].position.x, checkPoints[3].position.y));

        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * checkPoints[4].position + 3 * Mathf.Pow(1 - t, 2) * t * checkPoints[5].position + 3 * (1 - t) * Mathf.Pow(t, 2) * checkPoints[6].position + Mathf.Pow(t, 3) * checkPoints[7].position;

            Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        Gizmos.DrawLine(new Vector2(checkPoints[4].position.x, checkPoints[4].position.y), new Vector2(checkPoints[5].position.x, checkPoints[5].position.y));
        Gizmos.DrawLine(new Vector2(checkPoints[6].position.x, checkPoints[6].position.y), new Vector2(checkPoints[7].position.x, checkPoints[7].position.y));
    }

}
