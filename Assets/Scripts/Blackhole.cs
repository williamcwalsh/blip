using UnityEngine;
using UnityEngine.SceneManagement;

public class Blackhole : MonoBehaviour
{
    Transform ship;
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
        var shipObj = GameObject.FindGameObjectWithTag("Player");
        if (shipObj == null)
        {
            ship = null;
            shipRb = null;
            isShrinking = false;
            return;
        }

        ship = shipObj.transform;
        shipRb = shipObj.GetComponent<Rigidbody2D>();
        isShrinking = false;
    }

    void FixedUpdate()
    {
        if (ship == null || shipRb == null) return;

        float distanceToship = Vector2.Distance(transform.position, ship.position);

        if (distanceToship < influenceRange)
        {
            Vector2 pullDir = ((Vector2)transform.position - (Vector2)ship.position).normalized;
            float d = Mathf.Max(distanceToship, 0.05f);
            Vector2 pullForce = (pullDir * (intensity / d)) * 16f;
            shipRb.AddForce(pullForce, ForceMode2D.Force);
        }

        if (isShrinking)
        {
            Vector3 s = ship.localScale;
            float k = 1f - shrinkRate * Time.fixedDeltaTime;
            k = Mathf.Clamp01(k);
            ship.localScale = s * k;

            if (ship.localScale.x <= minshipScale)
            {
                Destroy(ship.gameObject);
                ship = null;
                shipRb = null;
                isShrinking = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (ship == null) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("ship entered black hole, shrinking...");
            isShrinking = true;

            if (shipRb != null)
            {
                shipRb.linearVelocity = Vector2.zero;
                shipRb.angularVelocity = 0f;
            }
        }
    }
}