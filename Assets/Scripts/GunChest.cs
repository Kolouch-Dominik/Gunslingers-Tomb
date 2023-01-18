using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChest : MonoBehaviour
{
    [field: SerializeField] List<GunPickUp> PotencialGuns { get; set; }
    [field: SerializeField] SpriteRenderer SR { get; set; }
    [field: SerializeField] Sprite ChestOpen { get; set; }
    [field: SerializeField] GameObject Notification { get; set; }
    [field: SerializeField] Transform SpawnPoint { get; set; }
    private bool canOpen;
    private bool isOpened = false;


    // Update is called once per frame
    void Update()
    {
        if (canOpen && Input.GetKeyUp(KeyCode.E) && !isOpened)
        {
            int gunSelect = Random.Range(0, PotencialGuns.Count);

            Instantiate(PotencialGuns[gunSelect], SpawnPoint.position, SpawnPoint.rotation);

            SR.sprite = ChestOpen;

            isOpened = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Notification.SetActive(true);

            canOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Notification.SetActive(false);

            canOpen = false;
        }
    }

}
