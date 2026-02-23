using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Ship : MonoBehaviour
{
    static Ship Instance;

    public float thrust = 8f;
    public float rotationSpeed = 200f;
    public float maxSpeed = 10f;

    public int money = 0;

    [Header("Ship Sprites")]
    [SerializeField] Sprite baseSprite;
    [SerializeField] Sprite shooting1Sprite;
    [SerializeField] Sprite engine2Sprite;


    [Header("Engine Upgrade")]
    [SerializeField] Sprite flameSprite;
    [SerializeField] Transform flamePoint;

    Rigidbody2D rb;
    SpriteRenderer sr;
    PlayerInputActions inputActions;

    float rotateInput;
    bool thrusting;

    PlayerSkills playerSkills;
    bool lastShooting1State;
    bool lastEngine1State;
    bool lastEngine2State;

    GameObject flameObject;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        inputActions = new PlayerInputActions();
        playerSkills = new PlayerSkills();

        if (flamePoint != null && flameSprite != null)
        {
            flameObject = new GameObject("EngineFlame");
            flameObject.transform.SetParent(flamePoint);
            flameObject.transform.localPosition = Vector3.zero;
            flameObject.transform.localRotation = Quaternion.identity;

            var flameSR = flameObject.AddComponent<SpriteRenderer>();
            flameSR.sprite = flameSprite;
            flameSR.sortingLayerID = sr.sortingLayerID;
            flameSR.sortingOrder = sr.sortingOrder + 2;
            flameObject.transform.localScale = Vector3.one * 1.2f;
            flameObject.SetActive(false);
        }
    }

    public PlayerSkills GetPlayerSkills()
    {
        return playerSkills;
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

    void Update()
    {
        bool shooting1 = canUseShooting1();
        bool engine1 = canUseEngine1();
        bool engine2 = canUseEngine2();

        if (shooting1 != lastShooting1State)
        {
            sr.sprite = shooting1 ? shooting1Sprite : baseSprite;
            lastShooting1State = shooting1;
        }
        if (engine2 != lastEngine2State)
        {
            sr.sprite = engine2 ? engine2Sprite : baseSprite;
            lastEngine2State = engine2;
        }

        if (engine1 != lastEngine1State && !engine2)
        {
            if (engine1)
            {
                thrust = 11f;
                maxSpeed = 14f;
            }
            else
            {
                thrust = 8f;
                maxSpeed = 10f;
            }

            lastEngine1State = engine1;
        }

        if(engine2 != lastEngine2State){
            thrust = 15f;
            maxSpeed=20f;
            lastEngine2State = engine2;
        }

        if (flameObject != null)
            flameObject.SetActive(engine1 && thrusting);
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

    public bool canUseShooting1()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Shooting1);
    }

    public bool canUseEngine1()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Engine1);
    }
    public bool canUseEngine2()
    {
        return playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Engine2);
    }
}