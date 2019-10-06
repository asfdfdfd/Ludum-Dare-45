using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour
{
    private static readonly float PUNCH_TIMER = 0.3f;

    private new Rigidbody2D rigidbody;

    private GameObject leftHand;
    private GameObject rightHand;

    private float punchTimer = 0.0f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        leftHand = GameObject.Find("Left Hand");
        leftHand.SetActive(false);

        rightHand = GameObject.Find("Right Hand");
        rightHand.SetActive(false);
    }

    void Update()
    {   
        if (punchTimer > 0)
        {
            punchTimer -= Time.deltaTime;

            if (punchTimer <= 0)
            {
                FinishPunch();
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                punchTimer = PUNCH_TIMER;
                leftHand.SetActive(true);
                rightHand.SetActive(false);
                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(new Vector2(-200, 0));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                punchTimer = PUNCH_TIMER;
                rightHand.SetActive(true);
                leftHand.SetActive(false);
                rigidbody.velocity = Vector2.zero;
                rigidbody.AddForce(new Vector2(200, 0));
            }
        }
    }

    public void FinishPunch()
    {
        punchTimer = 0;

        rigidbody.velocity = new Vector2(rigidbody.velocity.x * 0.5f, 0);

        leftHand.SetActive(false);
        rightHand.SetActive(false);
    }
}
