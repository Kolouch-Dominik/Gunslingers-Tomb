using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [field: SerializeField]
    public int HealthAmount { get; set; }
    [field: SerializeField]
    public float WaitToPickUp { get; set; }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WaitToPickUp > 0)
            WaitToPickUp -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && WaitToPickUp <= 0)
        {
            PlayerHealthController.Instance.CurrentHealth += HealthAmount;
            Destroy(gameObject);
        }
    }
}
