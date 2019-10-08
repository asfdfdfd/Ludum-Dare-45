using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Man : MonoBehaviour
{
    private AudioSource audioSourceManDamage;
    private AudioSource audioSourceManPunch;

    private static readonly float PUNCH_TIMER = 0.3f;

    private new Rigidbody2D rigidbody;

    private BoxCollider2D bodyCollider;

    private GameObject leftHand;
    private GameObject rightHand;

    private float punchTimer = 0.0f;

    private int health = 3;

    private bool isAttacking = false;

    private bool isStunned = false;

    private List<AudioClip> audioClipsDamage = new List<AudioClip>();

    private AudioClip audioClipHeroDeath;

    private List<AudioClip> audioClipsPunch = new List<AudioClip>();

    void Start()
    {
        health = 3;

        rigidbody = GetComponent<Rigidbody2D>();

        leftHand = GameObject.Find("Left Hand");
        leftHand.SetActive(false);

        rightHand = GameObject.Find("Right Hand");
        rightHand.SetActive(false);

        bodyCollider = GetComponent<BoxCollider2D>();

        audioSourceManDamage = GameObject.Find("AudioSourceManDamage").GetComponent<AudioSource>();

        audioSourceManPunch = GameObject.Find("AudioSourceManPunch").GetComponent<AudioSource>();

        audioClipsDamage.Add(Resources.Load<AudioClip>("Audio/Hero Damage 1"));
        audioClipsDamage.Add(Resources.Load<AudioClip>("Audio/Hero Damage 2"));
        audioClipsDamage.Add(Resources.Load<AudioClip>("Audio/Hero Damage 3"));

        audioClipHeroDeath = Resources.Load<AudioClip>("Audio/Hero Death");

        audioClipsPunch.Add(Resources.Load<AudioClip>("Audio/Hero Punch 1"));
        audioClipsPunch.Add(Resources.Load<AudioClip>("Audio/Hero Punch 2"));
        audioClipsPunch.Add(Resources.Load<AudioClip>("Audio/Hero Punch 3"));
        audioClipsPunch.Add(Resources.Load<AudioClip>("Audio/Hero Punch 4"));
        audioClipsPunch.Add(Resources.Load<AudioClip>("Audio/Hero Punch 5"));
    }

    public int GetHealth()
    {
        return health;
    }

    void Update()
    {
        if (!isStunned)
        {
            if (punchTimer > 0)
            {
                punchTimer -= Time.deltaTime;

                if (punchTimer <= 0)
                {
                    FinishPunch();
                }
            }
            else
            {
                var cameraHeightUnits = Camera.main.orthographicSize * 2.0f;
                var cameraWidthUnits = cameraHeightUnits * Screen.width / Screen.height;
                var cameraHalfWidthUnits = cameraWidthUnits / 2.0f;

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    PlayPunchSound();

                    isAttacking = true;

                    punchTimer = PUNCH_TIMER;
                    leftHand.SetActive(true);
                    rightHand.SetActive(false);
                    //rigidbody.velocity = Vector2.zero;
                    //rigidbody.AddForce(new Vector2(-200, 0));
                    LeanTween.cancel(gameObject);
                    var newX = gameObject.transform.position.x - 4;
                    if (newX < -cameraHalfWidthUnits)
                    {
                        newX = -cameraHalfWidthUnits;
                    }

                    LeanTween.moveX(gameObject, newX, 1.0f).setEaseOutExpo();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    PlayPunchSound();

                    isAttacking = true;

                    punchTimer = PUNCH_TIMER;
                    rightHand.SetActive(true);
                    leftHand.SetActive(false);
                    //rigidbody.velocity = Vector2.zero;
                    //rigidbody.AddForce(new Vector2(200, 0));
                    LeanTween.cancel(gameObject);

                    var newX = gameObject.transform.position.x + 4;
                    if (newX > cameraHalfWidthUnits)
                    {
                        newX = cameraHalfWidthUnits;
                    }
                    LeanTween.moveX(gameObject, newX, 1.0f).setEaseOutExpo();
                }
            }
        }
    }

    public void FinishPunch()
    {
        punchTimer = 0;

        LeanTween.cancel(gameObject);

        leftHand.SetActive(false);
        rightHand.SetActive(false);

        isAttacking = false;
    }

    public void Damage()
    {
        if (!isAttacking && !isStunned)
        {
            StartCoroutine(DamageCoroutine());
        }
    }

    private IEnumerator DamageCoroutine()
    {
        health -= 1;

        shakeHero();
        isStunned = true;

        if (health > 0)
        {
            PlayDamageSound();
            yield return new WaitForSeconds(0.4f);
            isStunned = false;
        }
        else
        {
            PlayDeathSound();
            yield return new WaitForSeconds(1.0f);

            GameObject backgroundSound = GameObject.Find("BackgroundSound");
            if (backgroundSound != null)
            {
                Destroy(backgroundSound);
            }

            SceneManager.LoadScene("Start");
        }
    }

    public void DamageWithBossLeg()
    {
        if (!isAttacking && !isStunned) { 
            StartCoroutine(DamageWithBossLegCoroutine());
        }
    }

    private IEnumerator DamageWithBossLegCoroutine()
    {
        isStunned = true;
        health -= 1;
        shakeHero();

        if (health > 0)
        {
            PlayDamageSound();
            yield return new WaitForSeconds(1.0f);
            isStunned = false;
        }
        else
        {
            PlayDeathSound();
            yield return new WaitForSeconds(1.0f);

            GameObject backgroundSound = GameObject.Find("BackgroundSound");
            if (backgroundSound != null)
            {
                Destroy(backgroundSound);
            }

            SceneManager.LoadScene("Start");
        }
    }

    private void shakeHero()
    {   
        var position = gameObject.transform.position;

        LeanTween.sequence()
            .append(LeanTween.moveY(gameObject, position.y + 0.1f, 0.1f))
            .append(LeanTween.moveY(gameObject, position.y - 0.1f, 0.05f))
            .append(LeanTween.moveY(gameObject, position.y + 0.1f, 0.1f))
            .append(LeanTween.moveY(gameObject, position.y - 0.1f, 0.05f))
            .append(LeanTween.moveY(gameObject, position.y, 0.1f));
    }

    public void SetStunned(bool value)
    {
        this.isStunned = value;
    }

    public void DamageWithBossBarrel()
    {
        StartCoroutine(DamageWithBossBarrelCoroutine());
    }

    private IEnumerator DamageWithBossBarrelCoroutine()
    {
        isStunned = true;
        health -= 1;
        shakeHero();

        if (health > 0)
        {
            PlayDamageSound();
            yield return new WaitForSeconds(1.0f);
            isStunned = false;
        }
        else
        {
            PlayDeathSound();
            yield return new WaitForSeconds(1.0f);

            GameObject backgroundSound = GameObject.Find("BackgroundSound");
            if (backgroundSound != null)
            {
                Destroy(backgroundSound);
            }

            SceneManager.LoadScene("Start");
        }
    }

    private void PlayDamageSound()
    {
        var audioClipIndex = Random.Range(0, audioClipsDamage.Count);
        audioSourceManDamage.PlayOneShot(audioClipsDamage[audioClipIndex]);
    }

    private void PlayDeathSound()
    {
        audioSourceManDamage.PlayOneShot(audioClipHeroDeath);
    }

    private void PlayPunchSound()
    {
        var audioClipIndex = Random.Range(0, audioClipsPunch.Count);
        audioSourceManPunch.PlayOneShot(audioClipsPunch[audioClipIndex]);
    }
}
