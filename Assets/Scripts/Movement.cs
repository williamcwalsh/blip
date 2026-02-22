using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    //movement attributes
    public float thrust = 8f;
    public float rotationSpeed = 200f;
    public float maxSpeed = 10f;

    Rigidbody2D rb;
    PlayerInputActions inputActions;

    float rotateInput;
    bool thrusting;

    private PlayerSkills playerSkills;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();
        playerSkills = new PlayerSkills();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => rotateInput = ctx.ReadValue<float>();
        inputActions.Player.Move.canceled += ctx => rotateInput = 0f;
        inputActions.Player.Thrust.performed += _ => thrusting = true;
        inputActions.Player.Thrust.canceled += _ => thrusting = false;
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void FixedUpdate()
    {
        rb.MoveRotation(rb.rotation + (-rotateInput * rotationSpeed * Time.fixedDeltaTime));

        if (thrusting)
            rb.AddForce(rb.transform.up * thrust, ForceMode2D.Force);

        var v = rb.linearVelocity;
        if (v.magnitude > maxSpeed)
            rb.linearVelocity = v.normalized * maxSpeed;
    }

    public bool canUseShooting1(){
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Shooting1);
    }
}
