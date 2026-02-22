using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public Transform player;
    Rigidbody2D ballRb;

    public float influenceRange;
    public float intensity;

    public float shrinkRate = 10f;
    public float minPlayerScale = 0.1f;

    bool isShrinking;

    void Start()
    {
        ballRb = player.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < influenceRange)
        {
            Vector2 pullDir = ((Vector2)transform.position - (Vector2)player.position).normalized;
            float d = Mathf.Max(distanceToPlayer, 0.05f);
            Vector2 pullForce = (pullDir * (intensity / d)) * 16f;
            ballRb.AddForce(pullForce, ForceMode2D.Force);
        }

        if (isShrinking)
        {
            Vector3 s = player.localScale;
            float k = 1f - shrinkRate * Time.fixedDeltaTime;
            k = Mathf.Clamp01(k);
            player.localScale = s * k;

            if (player.localScale.x <= minPlayerScale)
            {
                Destroy(player.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player != null && other.transform == player)
        {
            Debug.Log("Player entered black hole, shrinking...");
            isShrinking = true;

            if (ballRb != null)
            {
                ballRb.linearVelocity = Vector2.zero;
                ballRb.angularVelocity = 0f;
            }
        }
    }
}
