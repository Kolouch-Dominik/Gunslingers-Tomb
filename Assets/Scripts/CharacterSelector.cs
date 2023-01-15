using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private bool canSelect;

    [field: SerializeField] public GameObject Message { get; set; }
    [field: SerializeField] public PlayerController playerToSpawn { get; set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect && Input.GetKeyDown(KeyCode.E))
        {
            Vector3 playerPos = PlayerController.Instance.transform.position;

            Destroy(PlayerController.Instance.gameObject);

            PlayerController newPlayer = Instantiate(playerToSpawn, playerPos, playerToSpawn.transform.rotation);
            PlayerController.Instance = newPlayer;

            gameObject.SetActive(false);

            CameraController.Instance.Target = newPlayer.transform;

            CharacterSelectManager.Instance.ActivePlayer = newPlayer;
            CharacterSelectManager.Instance.ActiveCharSelect.gameObject.SetActive(true);
            CharacterSelectManager.Instance.ActiveCharSelect = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            canSelect = true;
            Message.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            canSelect = false;
            Message.SetActive(false);
        }
    }


}
