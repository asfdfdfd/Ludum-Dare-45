using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLeg : MonoBehaviour
{
    private new BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Man")
        {       
            Destroy(collider);

            collision.GetComponent<Man>().DamageWithBossLeg();

            GameObject.Find("Root").GetComponent<BossFight>().JumpedOnMan();
        }
    }

    public void DamagedByMan()
    {
        GameObject.Find("AudioSourceBossDamage").GetComponent<AudioSource>().Play();

        Destroy(collider);

        GameObject.Find("Root").GetComponent<BossFight>().DamagedByMan();
    }
}
