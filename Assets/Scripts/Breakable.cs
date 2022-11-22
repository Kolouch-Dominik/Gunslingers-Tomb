using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [field: SerializeField]
    public List<GameObject> Breakables { get; set; }
    [field: SerializeField]
    public int MaxPieces { get; set; }

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
            if(PlayerController.Instance.DashCounter > 0)
            {
                Destroy(gameObject);
                for (int i = 0; i < Random.Range(1, MaxPieces); i++)
                {
                    Instantiate(Breakables[Random.Range(0, Breakables.Count)], transform.position, transform.rotation);
                }
            }
        }
    }
}
