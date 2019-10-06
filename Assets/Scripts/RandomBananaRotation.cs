using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBananaRotation : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(new Vector3(0, 0, Random.value * 360.0f));
    }
}
