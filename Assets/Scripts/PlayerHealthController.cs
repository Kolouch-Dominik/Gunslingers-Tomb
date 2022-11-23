using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance { get; set; }

    [SerializeField]
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (currentHealth + value > 5)
                currentHealth = 5;
            else currentHealth += value;

            UIController.Instance.HealthSlider.value = currentHealth;
            UIController.Instance.HealthText.text = currentHealth + " / " + MaxHealth;
        }
    }

    private int currentHealth;
    [field: SerializeField]
    public int MaxHealth { get; private set; }

    public float InvincLenght { get; set; } = 1f;
    public float InvincCount { get; set; }

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        currentHealth = MaxHealth;

        UIController.Instance.HealthSlider.maxValue = MaxHealth;
        UIController.Instance.HealthSlider.value = CurrentHealth;
        UIController.Instance.HealthText.text = CurrentHealth + " / " + MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (InvincCount > 0)
        {
            InvincCount -= Time.deltaTime;

            if (InvincCount <= 0)
            {
                ChangePlayerColorAlpha(1f);
            }
        }
    }

    public void DamagePlayer()
    {
        if (InvincCount <= 0)
        {

            currentHealth--;

            MakePlayerInvincible(InvincLenght);

            if (currentHealth <= 0)
            {
                PlayerController.Instance.gameObject.SetActive(false);
                UIController.Instance.DeathScreen.SetActive(true);
            }


            UIController.Instance.HealthSlider.value = currentHealth;
            UIController.Instance.HealthText.text = currentHealth + " / " + MaxHealth;
        }
    }

    private void ChangePlayerColorAlpha(float alpha)
    {
        var color = PlayerController.Instance.Body.color;
        PlayerController.Instance.Body.color = new Color(color.r, color.g, color.b, alpha);
    }

    public void MakePlayerInvincible(float lenght)
    {
        InvincCount = lenght;
        ChangePlayerColorAlpha(0.5f);
    }
    
}
