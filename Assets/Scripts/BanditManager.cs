using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditManager : MonoBehaviour
{
    private const float BANDIT_SPAWN_TIME_MAX = 3.0f;
    private const float BANDIT_SPAWN_TIME_MIN = 2.0f;

    private static readonly int BANDIT_TOUGH_SPAWN_COUNTER = 5;
    private static readonly float BANDIT_TOUGH_SPAWN_PROBABILITY = 0.7f;

    private float banditSpawnTimeReduceSpeed = 0.03f;
    private float banditSpawnTime = BANDIT_SPAWN_TIME_MAX;
    private float banditSpawnTimer = BANDIT_SPAWN_TIME_MAX;

    private int banditToughSpawnCounter = BANDIT_TOUGH_SPAWN_COUNTER;

    private bool shouldSpawnRight = false;

    private GameObject prefabBandit;
    private GameObject prefabBanditTough;
    private GameObject prefabBanditCeillingShooter;

    private GameObject bandits;

    private bool isFinished = false;

    void Start()
    {
        bandits = GameObject.Find("Bandits");

        prefabBandit = Resources.Load<GameObject>("Prefabs/Bandit Simple 1");
        prefabBanditTough = Resources.Load<GameObject>("Prefabs/Bandit Tough");
        prefabBanditCeillingShooter = Resources.Load<GameObject>("Prefabs/Bandit Ceiling Shooter");

        StartCoroutine(BanditCeilingShooterSpawnCoroutine());
    }

    void Update()
    {
        if (!isFinished)
        {
            banditSpawnTimer -= Time.deltaTime;
            if (banditSpawnTimer <= 0)
            {
                SpawnBandit();
                banditSpawnTimer += banditSpawnTime;
            }
            banditSpawnTime -= Time.deltaTime * banditSpawnTimeReduceSpeed;
            if (banditSpawnTime < BANDIT_SPAWN_TIME_MIN)
            {
                banditSpawnTime = BANDIT_SPAWN_TIME_MIN;
            }
        }
    }

    private void SpawnBandit()
    {
        GameObject gameObjectBandit;

        if (banditToughSpawnCounter <= 0 && Random.value >= BANDIT_TOUGH_SPAWN_PROBABILITY)
        {
            banditToughSpawnCounter = BANDIT_TOUGH_SPAWN_COUNTER;

            gameObjectBandit = Instantiate(prefabBanditTough, bandits.transform);
        }
        else
        {
            gameObjectBandit = Instantiate(prefabBandit, bandits.transform);
        }

        EnemyMovable enemyMovable = gameObjectBandit.GetComponent<EnemyMovable>();
        Vector3 positionOld = gameObjectBandit.transform.position;

        if (shouldSpawnRight)
        {
            gameObjectBandit.transform.position = new Vector3(7, positionOld.y, 0);
            enemyMovable.MoveLeft();
        }
        else
        {
            gameObjectBandit.transform.position = new Vector3(-7, positionOld.y, 0);
            enemyMovable.MoveRight();
        }

        shouldSpawnRight = !shouldSpawnRight;

        banditToughSpawnCounter -= 1;
    }

    private IEnumerator BanditCeilingShooterSpawnCoroutine()
    {
        while (!isFinished)
        {
            GameObject banditCeilingshooter = Instantiate(prefabBanditCeillingShooter);
            banditCeilingshooter.transform.position = new Vector3(Random.value * 10.0f - 5, 4.6f, 0);
            yield return new WaitForSeconds(5);
        }
    }

    public IEnumerator SwitchToTheBossFightCoroutine()
    {
        isFinished = true;

        for (int i = 0; i < bandits.transform.childCount; i++)
        {
            var child = bandits.transform.GetChild(i);
            ShutOffBandit(child.gameObject);
        }

        yield return new WaitForSeconds(5);
    }

    private void ShutOffBandit(GameObject gameObject)
    {
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        Destroy(gameObject.GetComponent<EnemyMovable>());

        var position = gameObject.transform.position;

        LeanTween.sequence()
            .append(LeanTween.moveX(gameObject, position.x + 0.1f, 0.1f))
            .append(LeanTween.moveX(gameObject, position.x - 0.1f, 0.05f))
            .append(LeanTween.moveX(gameObject, position.x + 0.1f, 0.1f))
            .append(LeanTween.moveX(gameObject, position.x - 0.1f, 0.05f))
            .append(LeanTween.moveX(gameObject, position.x, 0.1f));

        LeanTween.moveY(gameObject, -5, 5).setDestroyOnComplete(true);
    }
}
