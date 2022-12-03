using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool doorsCloseOnEnter;
    public bool openWhenEnemiesClear;

    [field: SerializeField]
    public List<GameObject> Doors { get; set; } = new List<GameObject>();
    [field: SerializeField]
    public List<GameObject> Enemies { get; set; } = new List<GameObject>();

    void Update()
    {
        if (!(PlayerIsHere() && openWhenEnemiesClear))
        {
            return;
        }

        for (int i = 0; i < Enemies.Count; i++)
        {
            if (Enemies[i] == null)
            {
                Enemies.RemoveAt(i);
                i--;
            }
        }

        if (Enemies.Count == 0)
        {
            RemoveDoors();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        LevelManager.Instance.CurrentRoom = this;
        MoveCameraToHere();
        MaybeActivateDoors();
    }

    private void MaybeActivateDoors()
    {
        if (!doorsCloseOnEnter)
        {
            return;
        }

        Doors.ForEach(x => x.SetActive(true));
    }

    private void MoveCameraToHere()
    {
        CameraController.Instance.SetTarget(transform);
    }

    private void RemoveDoors()
    {
        Doors.ForEach(x=>x.SetActive(false));
        doorsCloseOnEnter = false;
    }

    private bool PlayerIsHere()
    {
        return LevelManager.Instance.CurrentRoom == this;
    }
}