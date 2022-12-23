using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [field: SerializeField]
    public Rigidbody2D EnemyBody { get; set; }
    [field: SerializeField]
    public float MoveSpeed { get; set; }
    
    [field: SerializeField]
    public Animator Anim { get; set; }

    [field: SerializeField]
    public int Health { get; set; } = 150;

    [field: SerializeField]
    public List<GameObject> Splatters { get; set; }
    [field: SerializeField]
    public GameObject HitEffect { get; set; }
    [field: SerializeField]
    public bool ShouldShoot { get; set; }
    [field: SerializeField]
    public GameObject Bullet { get; set; }
    [field: SerializeField]
    public Transform FirePoint { get; set; }
    [field: SerializeField]
    public float FireRate { get; set; }
    [field: SerializeField]
    public SpriteRenderer Body { get; set; }
    [field: SerializeField]
    public float ShootRange { get; set; }

    private float fireCounter;
    private Vector3 moveDirection;

    [field: SerializeField, Header("Chase Player")]
    public bool ShouldChasePlayer { get; set; }
    [field: SerializeField]
    public float RangeToChase { get; set; }

    [field: SerializeField, Header("Run away")]
    public bool ShouldRunAway { get; set; }
    [field: SerializeField]
    public float runawayRange { get; set; }

    [field: SerializeField, Header("Wander")]
    public bool ShouldWander { get; set; }
    [field: SerializeField]
    public float WanderLenght { get; set; }
    [field: SerializeField]
    public float PauseLenght { get; set; }
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    [field: SerializeField, Header("Patrol")]
    public bool ShouldPatrol { get; set; }
    [field: SerializeField]
    public List<Transform> PatrolPoints { get; set; }
    private int currentPatrolPoint;

    // Start is called before the first frame update
    void Start()
    {
        if (ShouldWander)
        {
            pauseCounter = Random.Range(PauseLenght * 0.75f, PauseLenght * 1.25f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Body.isVisible && PlayerController.Instance.gameObject.activeInHierarchy)
        {

            moveDirection = Vector3.zero;

            if (GetDistance() < RangeToChase && ShouldChasePlayer)
                moveDirection = PlayerController.Instance.transform.position - transform.position;
            else if (ShouldWander)
            {
                if (wanderCounter > 0)
                {
                    wanderCounter -= Time.deltaTime;

                    //move the enemy
                    moveDirection = wanderDirection;


                    if (wanderCounter <= 0)
                    {
                        pauseCounter = Random.Range(PauseLenght * 0.75f, PauseLenght * 1.25f);
                    }
                }
                if (pauseCounter > 0)
                {
                    pauseCounter -= Time.deltaTime;

                    if (pauseCounter <= 0)
                    {
                        wanderCounter = Random.Range(WanderLenght * 0.75f, WanderLenght * 1.25f);

                        wanderDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                    }
                }
            }
            else if (ShouldPatrol)
            {
                moveDirection = PatrolPoints[currentPatrolPoint].position - transform.position;

                if (Vector3.Distance(transform.position, PatrolPoints[currentPatrolPoint].position) < .2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= PatrolPoints.Count)
                    {
                        currentPatrolPoint = 0;
                    }
                }

            }

            if (ShouldRunAway && GetDistance() < runawayRange)
            {
                moveDirection = transform.position - PlayerController.Instance.transform.position;
            }

            moveDirection.Normalize();

            EnemyBody.velocity = moveDirection * MoveSpeed;


            if (ShouldShoot && GetDistance() < ShootRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = FireRate;
                    Instantiate(Bullet, FirePoint.position, FirePoint.rotation);
                    AudioManager.Instance.PlaySFX(12);
                }
            }
        }
        else EnemyBody.velocity = Vector3.zero;

        Anim.SetBool("isMoving", moveDirection != Vector3.zero);
    }

    public void DamageEnemy(int damage)
    {
        Health -= damage;
        Instantiate(HitEffect, transform.position, transform.rotation);
        AudioManager.Instance.PlaySFX(2);
        if (Health > 0) return;

        Destroy(gameObject);
        AudioManager.Instance.PlaySFX(1);
        Instantiate(Splatters[Random.Range(0, Splatters.Count)], transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360)));
    }

    private float GetDistance()
    {
        return Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
    }
}