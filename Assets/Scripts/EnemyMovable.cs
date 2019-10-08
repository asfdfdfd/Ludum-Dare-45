using UnityEngine;

public class EnemyMovable : MonoBehaviour
{
    public float banditSpeed = 1.5f;

    private float currentSpeed = 0.0f;

    void Awake()
    {
        if (Random.Range(0, 10) < 2)
        {
            banditSpeed = 5.0f;
        }
        else
        {
            banditSpeed = Random.Range(1.0f, 2.0f);
        }

        currentSpeed = banditSpeed;
    }

    public void MoveLeft()
    {
        currentSpeed = -banditSpeed;
    }

    public bool IsMovingLeft()
    {
        return currentSpeed < 0;
    }

    public void MoveRight()
    {
        currentSpeed = banditSpeed;
    }

    public bool IsMovingRight()
    {
        return currentSpeed > 0;
    }

    void Update()
    {
        transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
    }
}
