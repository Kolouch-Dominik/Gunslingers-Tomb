using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private bool canSelect;

    [field: SerializeField] public GameObject Message { get; set; }
    [field: SerializeField] public PlayerController PlayerToSpawn { get; set; }
    [field: SerializeField] public bool CanUnlock { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (!CanUnlock) return;

        if (PlayerPrefs.HasKey(PlayerToSpawn.name))
            if (PlayerPrefs.GetInt(PlayerToSpawn.name) == 1)
                gameObject.SetActive(true);
            else gameObject.SetActive(false);
        else gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect && Input.GetKeyDown(KeyCode.E))
        {
            Vector3 playerPos = PlayerController.Instance.transform.position;

            Destroy(PlayerController.Instance.gameObject);

            PlayerController newPlayer = Instantiate(PlayerToSpawn, playerPos, PlayerToSpawn.transform.rotation);
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
