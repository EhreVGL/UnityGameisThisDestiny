using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezzierFollow : MonoBehaviour
{

    [SerializeField] private Transform[] checkPoints;
    private Vector2 gizmosPosition;

    [SerializeField] private Transform[] Routes;
    private int routeToGo;
    private float tParam;
    private Vector2 BatPosition;
    private float speedModifier;
    private bool coroutineAllowed;

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * checkPoints[0].position + 3 * Mathf.Pow(1 - t, 2) * t * checkPoints[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * checkPoints[2].position + Mathf.Pow(t, 3) * checkPoints[3].position;

            //Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        //Gizmos.DrawLine(new Vector2(checkPoints[0].position.x, checkPoints[0].position.y), new Vector2(checkPoints[1].position.x, checkPoints[1].position.y));
        //Gizmos.DrawLine(new Vector2(checkPoints[2].position.x, checkPoints[2].position.y), new Vector2(checkPoints[3].position.x, checkPoints[3].position.y));

        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = Mathf.Pow(1 - t, 3) * checkPoints[4].position + 3 * Mathf.Pow(1 - t, 2) * t * checkPoints[5].position + 3 * (1 - t) * Mathf.Pow(t, 2) * checkPoints[6].position + Mathf.Pow(t, 3) * checkPoints[7].position;

            //Gizmos.DrawSphere(gizmosPosition, 0.25f);
        }

        //Gizmos.DrawLine(new Vector2(checkPoints[4].position.x, checkPoints[4].position.y), new Vector2(checkPoints[5].position.x, checkPoints[5].position.y));
        //Gizmos.DrawLine(new Vector2(checkPoints[6].position.x, checkPoints[6].position.y), new Vector2(checkPoints[7].position.x, checkPoints[7].position.y));
    }

    // Start is called before the first frame update
    void Start()
    {
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.2f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (routeToGo == 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else if (routeToGo == 1)
        {
            transform.localScale = new Vector2(-1, 1);
        }

        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector2 p0 = Routes[routeNumber].GetChild(0).position;
        Vector2 p1 = Routes[routeNumber].GetChild(1).position;
        Vector2 p2 = Routes[routeNumber].GetChild(2).position;
        Vector2 p3 = Routes[routeNumber].GetChild(3).position;

        while(tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            BatPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            transform.position = BatPosition;

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGo += 1;

        if(routeToGo > Routes.Length - 1)
        {
            routeToGo = 0;
        }

        coroutineAllowed = true;

    }
}
