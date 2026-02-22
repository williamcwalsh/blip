using UnityEngine;
using UnityEngine.InputSystem;

public class ShipShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed = 18f;
    [SerializeField] float fireRate = 1f;

    Rigidbody2D rb;
    Collider2D shipCol;

    PlayerInputActions inputActions;
    bool firing;
    float nextShotTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shipCol = GetComponent<Collider2D>();
        inputActions = new PlayerInputActions();
        nextShotTime = 0f;
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Shoot.performed += OnShootPerformed;
        inputActions.Player.Shoot.canceled += OnShootCanceled;
    }

    void OnDisable()
    {
        inputActions.Player.Shoot.performed -= OnShootPerformed;
        inputActions.Player.Shoot.canceled -= OnShootCanceled;
        inputActions.Disable();
    }

    void OnShootPerformed(InputAction.CallbackContext ctx)
    {
        firing = true;

        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + 1f / fireRate;
        }
    }

    void OnShootCanceled(InputAction.CallbackContext ctx)
    {
        firing = false;
    }

    void Update()
    {
        if (!firing) return;

        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        var b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bRb = b.GetComponent<Rigidbody2D>();

        if (bRb != null)
            bRb.linearVelocity = rb.linearVelocity + (Vector2)firePoint.up * bulletSpeed;

        var bCol = b.GetComponent<Collider2D>();
        if (shipCol != null && bCol != null)
            Physics2D.IgnoreCollision(shipCol, bCol, true);
    }
}
