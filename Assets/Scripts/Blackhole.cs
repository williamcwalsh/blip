using UnityEngine;
using UnityEngine.SceneManagement;

public class Blackhole : MonoBehaviour
{
    public Ship ship;
    Rigidbody2D shipRb;

    public float influenceRange;
    public float intensity;

    public float shrinkRate = 10f;
    public float minshipScale = 0.1f;

    bool isShrinking;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        BindShip();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BindShip();
    }

    void BindShip()
    {
        if (ship == null)
        {
            var shipObj = GameObject.FindGameObjectWithTag("Player");
            ship = shipObj ? shipObj.GetComponent<Ship>() : null;
        }

        shipRb = ship ? ship.GetComponent<Rigidbody2D>() : null;
        isShrinking = false;
    }

    void FixedUpdate()
    {
        if (ship == null || shipRb == null) return;

        float distanceToShip = Vector2.Distance(transform.position, ship.transform.position);

        if (distanceToShip < influenceRange)
        {
            Vector2 pullDir = ((Vector2)transform.position - (Vector2)ship.transform.position).normalized;
            float d = Mathf.Max(distanceToShip, 0.05f);
            Vector2 pullForce = (pullDir * (intensity / d)) * 16f;
            shipRb.AddForce(pullForce, ForceMode2D.Force);
        }

        if (isShrinking)
        {
            Vector3 s = ship.transform.localScale;
            float k = 1f - shrinkRate * Time.fixedDeltaTime;
            k = Mathf.Clamp01(k);
            ship.transform.localScale = s * k;

            if (ship.transform.localScale.x <= minshipScale)
            {
                SoundManager.instance.PlayDeathSound();
                ship.Respawn();
                SoundManager.instance.SetEngineLoop(false);
                isShrinking = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var hitShip = other.GetComponentInParent<Ship>();
        if (hitShip == null) return;

        ship = hitShip;
        shipRb = ship.GetComponent<Rigidbody2D>();

        isShrinking = true;

        if (shipRb != null)
        {
            shipRb.linearVelocity = Vector2.zero;
            shipRb.angularVelocity = 0f;
        }
    }
}