using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditTough : MonoBehaviour
{
    private EnemyMovable enemyMovable;
    private new Rigidbody2D rigidbody;

    private int lives = 2;

    void Start()
    {
        enemyMovable = GetComponent<EnemyMovable>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void DamageWithHand()
    {
        lives--;

        if (lives > 0)
        {
            StartCoroutine(DamageWithHandCoroutine());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator DamageWithHandCoroutine()
    {
        enemyMovable.enabled = false;
        float force = -1.0f;
        if (enemyMovable.IsMovingLeft())
        {
            force = force * -1;

        }
        LeanTween.moveX(gameObject, gameObject.transform.position.x + force, 1.0f).setEaseOutExpo();
        //rigidbody.AddForce(new Vector2(force, 0));
        yield return new WaitForSeconds(0.3f);
        enemyMovable.enabled = true;
        yield return null;
    }
}
