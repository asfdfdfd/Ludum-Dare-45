using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky1 : MonoBehaviour
{
    void Start()
    {
        var position = gameObject.transform.position;

        Vector3[] vectors = new Vector3[]
        {
            position, 
            new Vector3(position.x - 0.1f, position.y, 0),
            new Vector3(position.x - 0.1f, position.y + 0.1f, 0),
            new Vector3(position.x, position.y + 0.1f, 0),
            position,
        };

        LeanTween.moveSpline(gameObject, vectors, 10).setLoopPingPong();
    }
}
