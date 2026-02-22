using UnityEngine;

public class Tape : MonoBehaviour
{
    public Transform ship;
    Rigidbody2D shiprb;

    public float slowMultiplier = 2.5f; 

    void Start()
    {
        shiprb = ship.GetComponent<Rigidbody2D>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (ship != null && other.transform == ship)
        {
            if (shiprb != null)
            {
                shiprb.linearVelocity *= slowMultiplier;
                shiprb.angularVelocity *= slowMultiplier;
            }
        }
    }
}
