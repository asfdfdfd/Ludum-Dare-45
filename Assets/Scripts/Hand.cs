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
            GameObject.Find("Main Camera").GetComponent<CameraShake>().ShakeABit();

            enemyDamageable.DamageWithHand();

            LeanTween.sequence()
                .append(LeanTween.color(collision.gameObject, Color.red, 0.2f))
                .append(LeanTween.color(collision.gameObject, Color.white, 0.2f))
                .append(LeanTween.color(collision.gameObject, Color.red, 0.2f))
                .append(LeanTween.color(collision.gameObject, Color.white, 0.2f));
        }
    }
}
