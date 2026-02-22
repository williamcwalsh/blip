using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime = 1.5f;
    [SerializeField] int damage = 1;

    void OnEnable()
    {
        Invoke(nameof(Kill), lifeTime);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var asteroid = other.GetComponent<AsteroidHandler>();
        if (asteroid != null)
        {
            asteroid.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
