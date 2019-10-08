using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceToPlay : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("AudioSourceButton").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();

            GameObject backgroundSound = GameObject.Find("BackgroundSound");
            if (backgroundSound != null)
            {
                DontDestroyOnLoad(backgroundSound);
            }

            SceneManager.LoadScene("SampleScene");
        }
    }
}
