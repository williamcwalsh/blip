using UnityEngine;

public class AsteroidHandler : MonoBehaviour
{
    public int hp = 5;

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0) Destroy(gameObject);
    }
}
