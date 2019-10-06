using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Man man;

    void Start()
    {
        man = transform.parent.GetComponent<Man>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        man.FinishPunch();

        EnemyDamageable enemyDamageable;
        if (collision.gameObject.TryGetComponent(out enemyDamageable))
        {
            enemyDamageable.DamageWithHand();
        }
    }
}
