using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditTough : MonoBehaviour
{
    private EnemyMovable enemyMovable;
    private new Rigidbody2D rigidbody;
    private new PolygonCollider2D collider;

    private int lives = 2;

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

        lives--;

        if (lives > 0)
        {
            transform.Find("AudioSourceDamage").GetComponent<AudioSource>().Play();

            StartCoroutine(DamageWithHandCoroutine());
        }
        else
        {
            transform.Find("AudioSourceDeath").GetComponent<AudioSource>().Play();

            Death();
        }
    }

    private void Death()
    {
        Destroy(rigidbody);
        Destroy(collider);

        int direction = -1;
        if (enemyMovable.IsMovingLeft())
        {
            direction = 1;
        }

        LeanTween.moveY(gameObject, transform.position.y - 3.0f, 1.0f);
        LeanTween.moveX(gameObject, transform.position.x + (6.0f * direction), 2.0f).setDestroyOnComplete(true);
    }

    public IEnumerator DamageWithHandCoroutine()
    {
        enemyMovable.enabled = false;
        float force = -2.0f;
        if (enemyMovable.IsMovingLeft())
        {
            force = force * -1;

        }
        LeanTween.moveX(gameObject, gameObject.transform.position.x + force, 1.0f).setEaseOutExpo();
        yield return new WaitForSeconds(1.0f);
        enemyMovable.enabled = true;
        isDamaging = false;
        yield return null;
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
