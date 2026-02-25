using UnityEngine;
using UnityEngine.InputSystem;

public class ShipShooter : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    [Header("Base Gun")]
    [SerializeField] Transform firePoint;

    [Header("Shooting1 Upgrade (alternate barrels)")]
    [SerializeField] Transform firePointLeft;
    [SerializeField] Transform firePointRight;

    [SerializeField] float bulletSpeed = 18f;
    [SerializeField] float fireRate = 1f;
    [SerializeField] Ship ship;

    Rigidbody2D rb;
    Collider2D shipCol;

    PlayerInputActions inputActions;
    bool firing;
    float nextShotTime;

    bool hasShooting1;
    bool shootLeftNext = true;

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

    void Update()
    {
        hasShooting1 = ship != null && ship.canUseShooting1();
        if (hasShooting1){
            fireRate = 3.5f;
        }

        if (!firing) return;

        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + 1f / fireRate;
        }
    }

    void OnShootPerformed(InputAction.CallbackContext ctx)
    {
        firing = true;
        hasShooting1 = ship != null && ship.canUseShooting1();

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

    void Shoot()
    {
        Transform fp = firePoint;
        SoundManager.instance.PlayShootSound();

        if (hasShooting1 && firePointLeft != null && firePointRight != null)
        {
            fp = shootLeftNext ? firePointLeft : firePointRight;
            shootLeftNext = !shootLeftNext;
        }

        var b = Instantiate(bulletPrefab, fp.position, fp.rotation);
        var bRb = b.GetComponent<Rigidbody2D>();

        if (bRb != null)
            bRb.linearVelocity = rb.linearVelocity + (Vector2)fp.up * bulletSpeed;

        var bCol = b.GetComponent<Collider2D>();
        if (shipCol != null && bCol != null)
            Physics2D.IgnoreCollision(shipCol, bCol, true);
    }
}