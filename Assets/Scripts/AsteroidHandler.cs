using UnityEngine;

public class AsteroidHandler : MonoBehaviour
{
    public int hp = 5;
    public Ship ship;

    void OnCollisionEnter2D(Collision2D collision)
    {
        var hitShip = collision.collider.GetComponentInParent<Ship>();
        if (hitShip == null) return;

        Destroy(hitShip.gameObject);
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp <= 0)
        {
            if (ship != null)
            {
                ship.money += 1;
                Debug.Log(ship.money);
            }
            SoundManager.instance.PlayAsteroidSound();
            Destroy(gameObject);
        }
    }
}