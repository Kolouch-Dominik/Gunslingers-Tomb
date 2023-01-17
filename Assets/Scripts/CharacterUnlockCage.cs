using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUnlockCage : MonoBehaviour
{
    private bool canUnlock;
    [field: SerializeField] public GameObject Message { get; set; }
    [field: SerializeField] public List<CharacterSelector> CharacterSelectors { get; set; }
    [field: SerializeField] public SpriteRenderer CagedSR { get; set; }
    private CharacterSelector playerToUnlock;



    void Start()
    {
        playerToUnlock = CharacterSelectors[Random.Range(0, CharacterSelectors.Count)];

        CagedSR.sprite = playerToUnlock.PlayerToSpawn.Body.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (canUnlock && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPrefs.SetInt(playerToUnlock.PlayerToSpawn.name, 1);

            Instantiate(playerToUnlock, transform.position, transform.rotation);

            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            canUnlock = true;
            Message.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            canUnlock = false;
            Message.SetActive(false);
        }
    }
}
