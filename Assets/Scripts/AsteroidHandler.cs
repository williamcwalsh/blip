using UnityEngine;

public class AsteroidHandler : MonoBehaviour
{
    public int hp = 5;
    public Ship ship;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var hitShip = collision.collider.GetComponentInParent<Ship>();
        if (hitShip == null) return;
        SoundManager.instance.PlayDeathSound();
        // Destroy(hitShip.gameObject);
        hitShip.Respawn();
        SoundManager.instance.SetEngineLoop(false);

    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        SoundManager.instance.PlayHitSound();

        if (hp <= 0)
        {
            if (ship != null)
            {
                ship.money += 1;
            }
            SoundManager.instance.PlayAsteroidSound();
            Destroy(gameObject);
        }
    }
}