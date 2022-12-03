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
    public float RangeToChase { get; set; }
    [field: SerializeField]
    public Animator Anim { get; set; }

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Body.isVisible && PlayerController.Instance.gameObject.activeInHierarchy)
        {
            if (GetDistance() < RangeToChase)
                moveDirection = PlayerController.Instance.transform.position - transform.position;
            else moveDirection = Vector3.zero;

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