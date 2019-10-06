using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSimple : MonoBehaviour
{
    public void DamageWithHand()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("MAKAKA");
    }
}
