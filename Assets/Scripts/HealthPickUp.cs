using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [field: SerializeField] public int HealthAmount { get; set; }
    [field: SerializeField] public float WaitToPickUp { get; set; }

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
            Destroy(gameObject);

            PlayerHealthController.Instance.HealPlayer(HealthAmount);

            AudioManager.Instance.PlaySFX(7);
        }
    }
}
