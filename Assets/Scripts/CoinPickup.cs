using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [field:SerializeField]
    public int CoinValue { get; private set; } = 1;
    [field: SerializeField]
    public float WaitToPickUp { get; set; }

    // Start is called before the first frame update
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
            Destroy(gameObject);

            LevelManager.Instance.AddCoins(CoinValue);

            AudioManager.Instance.PlaySFX(5);
        }
    }
}
