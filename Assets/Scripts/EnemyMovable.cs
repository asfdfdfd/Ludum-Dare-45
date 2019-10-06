using UnityEngine;

public class EnemyMovable : MonoBehaviour
{
    public float banditSpeed = 1.5f;

    private float currentSpeed = 0.0f;

    void Awake()
    {
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
