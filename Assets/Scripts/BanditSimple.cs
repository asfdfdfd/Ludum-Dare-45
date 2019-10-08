using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditSimple : MonoBehaviour
{
    private EnemyMovable enemyMovable;
    private new Rigidbody2D rigidbody;
    private new PolygonCollider2D collider;

    private bool isDamaging = false;

    void Start()
    {
        enemyMovable = GetComponent<EnemyMovable>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<PolygonCollider2D>();
    }

    public void DamageWithHand()
    {
        isDamaging = true;

        Destroy(rigidbody);
        Destroy(collider);

        int direction = -1;
        if (enemyMovable.IsMovingLeft())
        {
            direction = 1;
        }

        GetComponent<AudioSource>().Play();

        LeanTween.moveY(gameObject, transform.position.y - 3.0f, 1.0f);
        LeanTween.moveX(gameObject, transform.position.x + (6.0f * direction), 2.0f).setDestroyOnComplete(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDamaging)
        {
            if (collision.name == "Man")
            {
                collision.GetComponent<Man>().Damage();

                DamageWithHand();
            }
        }
    }
}
