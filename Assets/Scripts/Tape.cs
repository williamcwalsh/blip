using UnityEngine;
using UnityEngine.SceneManagement;

public class Tape : MonoBehaviour
{
    public Ship ship;
    public Rigidbody2D shipRb;

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

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (shipRb == null) return;

            shipRb.linearVelocity *= slowMultiplier;
            shipRb.angularVelocity *= slowMultiplier;
    }
}