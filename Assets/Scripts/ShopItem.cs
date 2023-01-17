using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [field: SerializeField] public List<Gun> GunList { get; set; }

    private Gun gun;
    [field: SerializeField] public SpriteRenderer GunSprite { get; set; }
    [field: SerializeField] public Text InfoText { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        if (IsWeapon)
        {
            gun = GunList[Random.Range(0, GunList.Count)];

            GunSprite.sprite = gun.GunShopSprite;
            InfoText.text = gun.name + $"\n- {gun.ItemCost} -";
            ItemCost = gun.ItemCost;
        }
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
                else if (IsWeapon)
                {
                    var gunClone = Instantiate(gun);
                    gunClone.transform.parent = PlayerController.Instance.GunArm;
                    gunClone.transform.position = PlayerController.Instance.GunArm.position;
                    gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    gunClone.transform.localScale = Vector3.one;

                    PlayerController.Instance.AvailableGuns.Add(gunClone);
                    PlayerController.Instance.CurrentGunNum = PlayerController.Instance.AvailableGuns.Count - 1;
                    PlayerController.Instance.SwitchGun();
                }


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
