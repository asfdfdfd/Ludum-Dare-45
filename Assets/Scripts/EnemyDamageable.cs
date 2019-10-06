using UnityEngine;
using UnityEngine.Events;

public class EnemyDamageable : MonoBehaviour
{
    public UnityEvent eventDamageWithHand;

    public void DamageWithHand()
    {
        if (eventDamageWithHand != null)
        {
            eventDamageWithHand.Invoke();
        }
    }
}
