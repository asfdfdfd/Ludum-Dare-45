using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFight : MonoBehaviour
{
    private List<int[]> barrelPatterns = new List<int[]>();

    private GameObject prefabBarrelBig;

    private GameObject prefabLegLeft;
    private GameObject prefabLegRight;

    private bool isJumpedOnMan = false;

    private bool isDamagedByMan = false;

    private int lives = 6;

    void Start()
    {
        prefabBarrelBig = Resources.Load<GameObject>("Prefabs/Barrel Big");
        prefabLegLeft = Resources.Load<GameObject>("Prefabs/Boss Leg Left");
        prefabLegRight = Resources.Load<GameObject>("Prefabs/Boss Leg Right");

        barrelPatterns.Clear();
        barrelPatterns.Add(new int[] { 1, 1, 0 });
        barrelPatterns.Add(new int[] { 0, 1, 1 });
        barrelPatterns.Add(new int[] { 1, 0, 1 });

        StartBossFight();
    }

    void StartBossFight()
    {
        StartCoroutine(BossFightCoroutine());
    }

    private IEnumerator BossFightCoroutine()
    {
        while (lives > 0)
        {
            if (lives > 0)
            {
                yield return Boss1Barrels();
            }

            if (lives > 0)
            { 
                yield return BossLegs();
            }

            if (lives > 0)
            {
                yield return Boss2Barrels();
            }

            if (lives > 0)
            {
                yield return BossLegs();
            }
        }
    }

    private IEnumerator Boss1Barrels() 
    {
        GameObject bossSmall1 = Instantiate(Resources.Load<GameObject>("Prefabs/Boss Small 1"));
        var bossSmall1PositionTarget = bossSmall1.transform.position;
        var bossSmall1PositionSource = new Vector2(-2.61f, 4.33f);
        bossSmall1.transform.position = bossSmall1PositionSource;
        LeanTween.move(bossSmall1, bossSmall1PositionTarget, 1.0f).setEaseOutExpo();

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 2; i++)
        {
            GameObject barrel = Instantiate(Resources.Load<GameObject>("Prefabs/Barrel Small"));
            Vector2 barrelSource = new Vector2(bossSmall1.transform.position.x + 0.6f, bossSmall1.transform.position.y + 0.6f);
            Vector2 barrelTarget = new Vector2(barrelSource.x, 4.6f);
            barrel.transform.position = barrelSource;
            LeanTween.move(barrel, barrelTarget, 2.0f).setDestroyOnComplete(true);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(2.0f);

        yield return DropBarrels();

        LeanTween.move(bossSmall1, bossSmall1PositionSource, 1.0f).setEaseInExpo().setDestroyOnComplete(true);

        yield return new WaitForSeconds(1.0f);
    }

    private IEnumerator Boss2Barrels()
    {
        GameObject bossSmall2 = Instantiate(Resources.Load<GameObject>("Prefabs/Boss Small 2"));
        var bossSmall2PositionTarget = bossSmall2.transform.position;
        var bossSmall2PositionSource = new Vector2(3.54f, 4.44f);
        bossSmall2.transform.position = bossSmall2PositionSource;
        LeanTween.move(bossSmall2, bossSmall2PositionTarget, 1.0f).setEaseOutQuad();

        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < 2; i++)
        {
            GameObject barrel = Instantiate(Resources.Load<GameObject>("Prefabs/Barrel Small"));
            Vector2 barrelSource = new Vector2(bossSmall2.transform.position.x + 0.4f, bossSmall2.transform.position.y + 0.8f);
            Vector2 barrelTarget = new Vector2(barrelSource.x, 4.6f);
            barrel.transform.position = barrelSource;
            LeanTween.move(barrel, barrelTarget, 2.0f).setDestroyOnComplete(true);
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(2.0f);

        yield return DropBarrels();

        LeanTween.move(bossSmall2, bossSmall2PositionSource, 1.0f).setEaseInQuad().setDestroyOnComplete(true);

        yield return new WaitForSeconds(1.0f);
    }

    private IEnumerator DropBarrels()
    {
        GameObject.Find("AudioSourceBarrelFall").GetComponent<AudioSource>().Play();

        var patternIndex = Random.Range(0, barrelPatterns.Count);
        var pattern = barrelPatterns[patternIndex];

        float startX = -4.5f;
        float startY = 4.5f;
        float stopY = -4.5f;
        float step = 4.5f;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i] == 1)
            {
                var barrel = Instantiate(prefabBarrelBig);
                barrel.transform.position = new Vector2(startX, startY);

                LeanTween.move(barrel, new Vector2(startX, stopY), 3.0f);
            }

            startX = startX + step;
        }

        yield return new WaitForSeconds(3.0f);
    }

    private IEnumerator BossLegs()
    {
        var patternIndex = Random.Range(0, barrelPatterns.Count);
        var pattern = barrelPatterns[patternIndex];

        GameObject[] legs = { Instantiate(prefabLegLeft), Instantiate(prefabLegRight) };

        float startX = -4.5f;
        float startY = 7.0f;
        float stepX = 4.5f;
        var legIndex = 0;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i] == 1)
            {
                var leg = legs[legIndex];
                leg.transform.position = new Vector2(startX, startY);
                LeanTween.moveY(leg, 0, 3).setEaseInExpo();

                legIndex++;
            }

            startX += stepX;
        }

        StartCoroutine(PlaySoundAfterDelay());

        var startCooldownTime = Time.time;

        while (Time.time - startCooldownTime < 6.0f)
        {
            if (isJumpedOnMan || isDamagedByMan)
            {
                break;
            }

            yield return null;
        }

        isJumpedOnMan = false;
        isDamagedByMan = false;

        for (int i = 0; i < legs.Length; i++)
        {
            LeanTween.moveY(legs[i], 7, 3).setEaseOutQuint();
        }

        yield return new WaitForSeconds(3.0f);
    }

    public void JumpedOnMan()
    {
        isJumpedOnMan = true;
    }

    public void DamagedByMan()
    {
        lives--;

        isDamagedByMan = true;

        if (lives == 0)
        {
            StartCoroutine(MoveToTheFinal());
        }
    }

    private IEnumerator MoveToTheFinal()
    {
        GameObject.Find("AudioSourceBossDeath").GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.0f);

        GameObject backgroundSound = GameObject.Find("BackgroundSound");
        if (backgroundSound != null) 
        {
            Destroy(backgroundSound);
        }

        SceneManager.LoadScene("Finish");

        yield return null;
    }

    private IEnumerator PlaySoundAfterDelay()
    {
        yield return new WaitForSeconds(3.0f);

        if (!isDamagedByMan)
        {
            GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake(1.0f);

            GameObject.Find("AudioSourceBossStomp").GetComponent<AudioSource>().Play();
        }    
    }
}