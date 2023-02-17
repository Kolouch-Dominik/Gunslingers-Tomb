using UnityEngine;

public class LevelExit : MonoBehaviour
{
    [field: SerializeField] public string LevelToLoad;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            StartCoroutine(LevelManager.Instance.LevelEnd());
        }
    }
}
