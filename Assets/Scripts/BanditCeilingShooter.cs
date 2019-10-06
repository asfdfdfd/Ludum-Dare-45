using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditCeilingShooter : MonoBehaviour
{
    public float bulletFlyTime = 7.0f;

    private GameObject prefabBullet;

    void Start()
    {   
        prefabBullet = Resources.Load<GameObject>("Prefabs/Bandit Ceiling Shooter Bullet");

        StartCoroutine(IntroCoroutine());
    }

    private IEnumerator IntroCoroutine()
    {
        var fallTime = 1.0f;
        var startY = transform.position.y;
        var stopY = 1.0f;

        LeanTween.moveY(gameObject, stopY, fallTime).setEaseSpring();
        yield return new WaitForSeconds(fallTime);
        Shoot();
        // TODO: Random amount of waves
        yield return new WaitForSeconds(fallTime);
        LeanTween.moveY(gameObject, startY, fallTime).setEaseOutBack();
    }

    private void Shoot()
    {
        // Sometimes Camera.current is not available so let's hardcode Camera.current.orthographicSize value here.
        var bulletTraceLength = 3.6f * 2f;

        GameObject bulletCenter = Instantiate(prefabBullet);
        GameObject bulletLeft = Instantiate(prefabBullet);
        GameObject bulletRight = Instantiate(prefabBullet);

        Vector2 targetCenter = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - bulletTraceLength);
        Vector2 targetLeft = new Vector2(gameObject.transform.position.x - bulletTraceLength, gameObject.transform.position.y - bulletTraceLength);
        Vector2 targetRight = new Vector2(gameObject.transform.position.x + bulletTraceLength, gameObject.transform.position.y - bulletTraceLength);

        LeanTween.move(bulletCenter, targetCenter, bulletFlyTime).setDestroyOnComplete(true);
        LeanTween.move(bulletLeft, targetLeft, bulletFlyTime).setDestroyOnComplete(true);
        LeanTween.move(bulletRight, targetRight, bulletFlyTime).setDestroyOnComplete(true);
    }
}
