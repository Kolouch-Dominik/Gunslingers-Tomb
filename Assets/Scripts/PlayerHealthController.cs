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

    public float InvincLenght { get; set; } = 1f;
    public float InvincCount { get; set; }

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

            CurrentHealth--;

            InvincCount = InvincLenght;

            ChangePlayerColorAlpha(0.5f);

            if (CurrentHealth <= 0)
            {
                PlayerController.Instance.gameObject.SetActive(false);
                UIController.Instance.DeathScreen.SetActive(true);
            }


            UIController.Instance.HealthSlider.value = CurrentHealth;
            UIController.Instance.HealthText.text = CurrentHealth + " / " + MaxHealth;
        }
    }

    private void ChangePlayerColorAlpha(float alpha)
    {
        var color = PlayerController.Instance.Body.color;
        PlayerController.Instance.Body.color = new Color(color.r, color.g, color.b, alpha);
    }
}