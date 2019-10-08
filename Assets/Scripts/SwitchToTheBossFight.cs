using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToTheBossFight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchToTheBossFightCoroutine());
    }

    private IEnumerator SwitchToTheBossFightCoroutine()
    {
        yield return new WaitForSeconds(60);

        GameObject.Find("Man").GetComponent<Man>().SetStunned(true);

        yield return GetComponent<BanditManager>().SwitchToTheBossFightCoroutine();

        GameObject.Find("Man").GetComponent<Man>().SetStunned(false);

        GetComponent<BanditManager>().enabled = false;

        GetComponent<BossFight>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
