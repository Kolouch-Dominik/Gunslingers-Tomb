using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; set; }
    [field: SerializeField]
    public Slider HealthSlider { get; set; }
    [field: SerializeField]
    public Text HealthText { get; set; }
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
