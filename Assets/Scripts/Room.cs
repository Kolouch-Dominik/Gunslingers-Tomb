using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    [field: SerializeField] public bool CloseWhenEnter { get; set; }
    [field: SerializeField] public bool OpenWhenEnemiesDied { get; set; }
    [field: SerializeField] public List<GameObject> Doors { get; set; }
    [field: SerializeField] public List<GameObject> Enemies { get; set; } = new List<GameObject>();
    private bool roomActive;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies.Count > 0 && roomActive && OpenWhenEnemiesDied)
        {
            for (var i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].Equals(null))
                {
                    Enemies.RemoveAt(i);
                    --i;
                }
            }

            if (Enemies.Count == 0)
            {
                Doors.ForEach(x => x.SetActive(false));
                CloseWhenEnter = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            CameraController.Instance.SetTarget(transform);

            if (CloseWhenEnter)
                Doors.ForEach(x=>x.SetActive(true));

            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag.Equals("Player"))
            roomActive = false;
    }
}
