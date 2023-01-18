using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCenter : MonoBehaviour
{
    public bool openWhenEnemiesClear;

    [field: SerializeField] public List<GameObject> Enemies { get; set; } = new List<GameObject>();

    [field: SerializeField] public Room Room;
    // Start is called before the first frame update
    void Start()
    {
        if (openWhenEnemiesClear)
        {
            Room.doorsCloseOnEnter = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Room.ShouldFreeze)
        {
            Enemies.ForEach(e => e.gameObject.GetComponent<EnemyController>().IsFreezed = true);
            Room.ShouldFreeze = false;
        }
        if (Enemies.Count > 0 && Room.IsActive && openWhenEnemiesClear)
        {
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
                Room.OpenDoors();
            }
        }
    }
}
