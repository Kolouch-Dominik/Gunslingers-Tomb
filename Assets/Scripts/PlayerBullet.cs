using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Start is called before the first frame update
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public Rigidbody2D BulletRB { get; set; }
    [field: SerializeField] public GameObject ImpactEffect { get; set; }
    [field: SerializeField] public int BulletDamage { get; set; }

    // Update is called once per frame
    void Update()
    {
        BulletRB.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(ImpactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
        AudioManager.Instance.PlaySFX(4);
        if (other.tag.Equals("Enemy") || other.tag.Equals("Boss"))
            if (other.GetComponent<EnemyController>() != null)
                other.GetComponent<EnemyController>().DamageEnemy(BulletDamage);
            else if (other.GetComponent<BossController>() != null)
            {
                other.GetComponent<BossController>().TakeDamage(BulletDamage);

                Instantiate(BossController.Instance.HitEffect, transform.position, transform.rotation);
            }
    }

    /*private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }*/
}
