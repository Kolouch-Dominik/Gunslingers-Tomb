using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [field: SerializeField]
    public GameObject BuyMessage { get; set; }
    private bool inBuyZone;
    [field: SerializeField] public bool IsHealthRestore { get; set; }
    [field: SerializeField] public bool IsHealthUpgrade { get; set; }
    [field: SerializeField] public bool IsWeapon { get; set; }
    [field: SerializeField] public int ItemCost { get; set; }
    [field: SerializeField] public int HealthUpgradeAmount { get; set; }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inBuyZone && Input.GetKeyDown(KeyCode.E))
        {
            if (LevelManager.Instance.CurrentCoins >= ItemCost)
            {
                LevelManager.Instance.SpendCoins(ItemCost);

                if (IsHealthRestore)
                    PlayerHealthController.Instance.HealPlayer(PlayerHealthController.Instance.MaxHealth);

                else if (IsHealthUpgrade)
                    PlayerHealthController.Instance.IncreaseMaxHealth(HealthUpgradeAmount);


                gameObject.SetActive(false);
                inBuyZone = false;

                AudioManager.Instance.PlaySFX(18);
            }
            else AudioManager.Instance.PlaySFX(19);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            BuyMessage.SetActive(true);
            inBuyZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            BuyMessage.SetActive(false);
            inBuyZone = false;
        }
    }
}
