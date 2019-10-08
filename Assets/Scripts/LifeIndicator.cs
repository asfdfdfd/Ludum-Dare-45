using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIndicator : MonoBehaviour
{
    private List<GameObject> hearts = new List<GameObject>();
    private List<GameObject> heartsDeath = new List<GameObject>();

    private Man man;

    void Start()
    {
        GameObject prefabHeart = Resources.Load<GameObject>("Prefabs/Heart");
        GameObject prefabHeartDeath = Resources.Load<GameObject>("Prefabs/Heart Death");

        for (int i = 0; i < 3; i++)
        {
            var heart = Instantiate(prefabHeart, transform);
            heart.transform.Translate(i * 0.6f, 0.0f, 0.0f);

            hearts.Add(heart);

            var heartDeath = Instantiate(prefabHeartDeath, transform);
            heartDeath.transform.Translate(i * 0.6f, 0.0f, 0.0f);

            heartsDeath.Add(heartDeath);
        }

        man = GameObject.Find("Man").GetComponent<Man>();
    }

    void Update()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < man.GetHealth())
            {
                hearts[i].SetActive(true);
                heartsDeath[i].SetActive(false);
            } else
            {
                hearts[i].SetActive(false);
                heartsDeath[i].SetActive(true);
            }
        }
    }
}
