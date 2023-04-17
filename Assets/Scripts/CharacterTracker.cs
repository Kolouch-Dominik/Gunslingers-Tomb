using UnityEngine;

public class CharacterTracker : MonoBehaviour
{
    public static CharacterTracker Instance { get; set; }

    [field: SerializeField] public int CurrentHealth { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }
    [field: SerializeField] public int CurrentCoins { get; set; }



    private void Awake()
    {
        Instance = this;
    }
}
