using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [field: SerializeField]
    public List<GameObject> Breakables { get; set; }
    [field: SerializeField]
    public int MaxPieces { get; set; }

    [field:SerializeField]
    public bool ShouldDropItem { get; set; }
    [field: SerializeField] 
    public List<GameObject> ItemsToDrop { get; set; }
    [field: SerializeField]
    public float DropPercentage { get; set; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            if (PlayerController.Instance.DashCounter > 0)
            {
                DestroyBreakable();
            }
        }
        else if(other.tag.Equals("PlayerBullet"))
            DestroyBreakable();
    }

    private void DestroyBreakable()
    {
        Destroy(gameObject);

        AudioManager.Instance.PlaySFX(0);

        for (var i = 0; i < Random.Range(1, MaxPieces); i++)
        {
            Instantiate(Breakables[Random.Range(0, Breakables.Count)], transform.position, transform.rotation);
        }

        if (ShouldDropItem)
        {
            var dropChance = Random.Range(0f, 101f);
            if (dropChance < DropPercentage)
            {
                Instantiate(ItemsToDrop[Random.Range(0, ItemsToDrop.Count)], transform.position, transform.rotation);
            }
        }
    }
}
