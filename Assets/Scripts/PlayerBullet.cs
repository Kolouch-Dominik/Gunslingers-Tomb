using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed { get; set; } = 7.5f;
    [field: SerializeField]
    public Rigidbody2D BulletRB { get; set; }

    [field: SerializeField] 
    public GameObject ImpactEffect { get; set; }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletRB.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(gameObject); 
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
