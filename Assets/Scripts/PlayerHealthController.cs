using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance { get; set; }
    [field: SerializeField]
    public int CurrentHealth { get; set; }
    [field: SerializeField]
    public int MaxHealth { get; set; }
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        CurrentHealth = MaxHealth;

        UIController.Instance.HealthSlider.maxValue = MaxHealth;
        UIController.Instance.HealthSlider.value = CurrentHealth;
        UIController.Instance.HealthText.text = CurrentHealth + " / " + MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer()
    {
        CurrentHealth--;
        if (CurrentHealth <= 0)
        {
            PlayerController.Instance.gameObject.SetActive(false);
        }


        UIController.Instance.HealthSlider.value = CurrentHealth;
        UIController.Instance.HealthText.text = CurrentHealth + " / " + MaxHealth;
    }
}
