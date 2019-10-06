using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditManager : MonoBehaviour
{
    private const float BANDIT_SPAWN_TIME = 2.0f;

    private static readonly int BANDIT_TOUGH_SPAWN_COUNTER = 0; // 5
    private static readonly float BANDIT_TOUGH_SPAWN_PROBABILITY = 0.0f;//0.7f;

    private float banditSpawnTimer = BANDIT_SPAWN_TIME;

    private int banditToughSpawnCounter = BANDIT_TOUGH_SPAWN_COUNTER;

    private bool shouldSpawnRight = false;

    private GameObject prefabBandit;
    private GameObject prefabBanditTough;
    private GameObject prefabBanditCeillingShooter;

    void Start()
    {
        prefabBandit = Resources.Load<GameObject>("Prefabs/Bandit Simple 1");
        prefabBanditTough = Resources.Load<GameObject>("Prefabs/Bandit Tough");
        prefabBanditCeillingShooter = Resources.Load<GameObject>("Prefabs/Bandit Ceiling Shooter");

        StartCoroutine(BanditCeilingShooterSpawnCoroutine());
    }

    void Update()
    {
        banditSpawnTimer -= Time.deltaTime;
        if (banditSpawnTimer <= 0)
        {
            SpawnBandit();
            banditSpawnTimer += BANDIT_SPAWN_TIME;
        }
    }

    private void SpawnBandit()
    {
        GameObject gameObjectBandit;

        if (banditToughSpawnCounter <= 0 && Random.value >= BANDIT_TOUGH_SPAWN_PROBABILITY)
        {
            banditToughSpawnCounter = BANDIT_TOUGH_SPAWN_COUNTER;

            gameObjectBandit = Instantiate(prefabBanditTough);
        }
        else
        {
            gameObjectBandit = Instantiate(prefabBandit);
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
        while (true)
        {
            GameObject banditCeilingshooter = Instantiate(prefabBanditCeillingShooter);
            yield return new WaitForSeconds(5);
            Destroy(banditCeilingshooter);
        }
    }
}
