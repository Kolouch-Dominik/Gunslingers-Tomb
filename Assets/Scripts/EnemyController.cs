using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [field: SerializeField]
    public Rigidbody2D Rigidbody2D { get; set; }
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

    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) < RangeToChase)
            moveDirection = PlayerController.Instance.transform.position - transform.position;
        else moveDirection = Vector3.zero;

        moveDirection.Normalize();

        Rigidbody2D.velocity = moveDirection * MoveSpeed;

        Anim.SetBool("isMoving", moveDirection != Vector3.zero);
    }

    public void DamageEnemy(int damage)
    {
        Health -= damage;
        Instantiate(HitEffect, transform.position, transform.rotation);
        if (Health > 0) return;

        Destroy(gameObject);
        Instantiate(Splatters[Random.Range(0,Splatters.Count)], transform.position, Quaternion.Euler(0f,0f,Random.Range(0,360)));
    }
}