using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool doorsCloseOnEnter;


    [field: SerializeField] public List<GameObject> Doors { get; set; } = new List<GameObject>();
    [field: SerializeField] public GameObject MapHider { get; set; }

    public bool IsActive { get; private set; }
    public bool ShouldFreeze { get; set; }

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ShouldFreeze = true;
            CameraController.Instance.SetTarget(transform);

            if (doorsCloseOnEnter)
            {
                foreach (var door in Doors)
                {
                    door.SetActive(true);
                }
            }
            IsActive = true;

            MapHider.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IsActive = false;
        }
    }

    public void OpenDoors()
    {
        foreach (var door in Doors)
        {
            door.SetActive(false);

            doorsCloseOnEnter = false;
        }
    }
}