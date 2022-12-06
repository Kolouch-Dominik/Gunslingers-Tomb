using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    [field: SerializeField]
    public float MoveSpeed { get; private set; }
    [field: SerializeField]
    public Animator Anim { get; private set; }
    public Rigidbody2D theRB;
    public Transform gunArm;
    private Camera cam;

    [field: SerializeField]
    public GameObject BulletToFire { get; set; }
    [field: SerializeField]
    public Transform FirePoint { get; set; }
    [field: SerializeField]
    public float TimeBetweenShots { get; set; }
    private float shotCounter;
    [field: SerializeField]
    public SpriteRenderer Body { get; set; }
    [field: SerializeField]
    public float DashSpeed { get; set; } = 8f;
    [field: SerializeField]
    public float DashLenght { get; set; } = 0.5f;
    [field: SerializeField]
    public float DashCooldown { get; set; } = 1f;
    [field: SerializeField]
    public float DashInvincibility { get; set; } = 0.5f;

    public bool CanMove { get; set; } = true;

    public float DashCounter { get; private set; }
    private float dashCoolCounter;

    private Vector2 moveInput;

    private float ActiveMoveSpeed { get; set; }


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cam = Camera.main; //v updatu volat Camera.Main --> moc nároèná opera, staèí jednou pøi startu

        ActiveMoveSpeed = MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CanMove || LevelManager.Instance.IsPaused)
        {
            theRB.velocity = Vector2.zero;
            Anim.SetBool("isMoving", false);
            return;
        }

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();      //odstranìní chybi pøi držení "w" + smìr do strany (diagonální pohyb) už není rychlejší

        theRB.velocity = moveInput * ActiveMoveSpeed;

        var mousePosition = Input.mousePosition;    //pozice myši
        var screenPoint = cam.WorldToScreenPoint(transform.localPosition);  //pozice hráèe

        //otáèení podle smìru pohledu postavy
        if (mousePosition.x < screenPoint.x)
        {
            ScaleTransform(transform, new Vector3(-1f, 1f, 1f));
            ScaleTransform(gunArm, new Vector3(-1f, -1f, 1f));
        }
        else if (mousePosition.x > screenPoint.x)
        {
            ScaleTransform(transform, Vector3.one);
            ScaleTransform(gunArm, Vector3.one);
        }

        var offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(BulletToFire, FirePoint.position, FirePoint.rotation); //vytvoøení instance objektu (objekt, pozice, rotace)

            shotCounter = TimeBetweenShots;
            AudioManager.Instance.PlaySFX(12);
        }

        if (Input.GetMouseButton(0))
        {
            if ((shotCounter -= Time.deltaTime) <= 0)
            {
                Instantiate(BulletToFire, FirePoint.position, FirePoint.rotation);
                AudioManager.Instance.PlaySFX(12);

                shotCounter = TimeBetweenShots;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && DashCounter <= 0)
            {
                ActiveMoveSpeed = DashSpeed;
                DashCounter = DashLenght;

                Anim.SetTrigger("dashTrigger");
                PlayerHealthController.Instance.MakePlayerInvincible(DashLenght);

                AudioManager.Instance.PlaySFX(8);
            }
        }

        if (DashCounter > 0)
        {
            DashCounter -= Time.deltaTime;
            if (DashCounter <= 0)
            {
                ActiveMoveSpeed = MoveSpeed;
                dashCoolCounter = DashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }


        Anim.SetBool("isMoving", moveInput != Vector2.zero);
    }

    void ScaleTransform(Transform transformToScale, Vector3 scale)
    {
        transformToScale.localScale = scale;
    }
}
