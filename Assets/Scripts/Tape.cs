using UnityEngine;
using UnityEngine.SceneManagement;

public class Tape : MonoBehaviour
{
    Rigidbody2D shipRb;

    [Range(0f, 1f)]
    public float slowMultiplier = 0.4f;

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
            shipRb = null;
            return;
        }

        shipRb = shipObj.GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (shipRb == null) return;

        if (other.CompareTag("Player"))
        {
            shipRb.linearVelocity *= slowMultiplier;
            shipRb.angularVelocity *= slowMultiplier;
        }
    }
}