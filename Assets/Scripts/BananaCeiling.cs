using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaCeiling : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Man")
        {
            collision.GetComponent<Man>().Damage();
        }
    }
}
