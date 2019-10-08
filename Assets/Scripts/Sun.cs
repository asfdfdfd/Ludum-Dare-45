using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    void Start()
    {
        LeanTween.rotateZ(gameObject, 15, 3.0f).setOnComplete(() => {
            LeanTween.rotateZ(gameObject, -15, 3.0f).setLoopPingPong();
        });
    }
}
