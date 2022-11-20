using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [field: SerializeField]
    public float Speed { get; set; }
    private Vector3 direction;


    // Start is called before the first frame update
    void Start()
    {
        direction = (PlayerController.Instance.transform.position - transform.position);
        direction.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

        }
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
