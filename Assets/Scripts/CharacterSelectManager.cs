using UnityEngine;

public class CharacterSelectManager : MonoBehaviour
{
    public static CharacterSelectManager Instance { get; set; }

    [field: SerializeField] public PlayerController ActivePlayer { get; set; }
    [field: SerializeField] public CharacterSelector ActiveCharSelect { get; set; }

    public void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
