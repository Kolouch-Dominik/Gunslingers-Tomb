using UnityEngine;

public class GunPickUp : MonoBehaviour
{
    [field: SerializeField] public Gun TheGun { get; set; }
    [field: SerializeField] public float WaitToPickUp { get; set; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (WaitToPickUp > 0)
            WaitToPickUp -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player") && WaitToPickUp <= 0)
        {
            bool hasGun = false;

            PlayerController.Instance.AvailableGuns.ForEach(gun =>
            {
                if (gun.WeaponName.Equals(TheGun.WeaponName))
                    hasGun = true;
            });

            if (!hasGun)
            {
                var gunClone = Instantiate(TheGun);
                gunClone.transform.parent = PlayerController.Instance.GunArm;
                gunClone.transform.position = PlayerController.Instance.GunArm.position;
                gunClone.transform.localRotation = Quaternion.Euler(Vector3.zero);
                gunClone.transform.localScale = Vector3.one;

                PlayerController.Instance.AvailableGuns.Add(gunClone);
                PlayerController.Instance.CurrentGunNum = PlayerController.Instance.AvailableGuns.Count - 1;
                PlayerController.Instance.SwitchGun();
            }
            Destroy(gameObject);

            AudioManager.Instance.PlaySFX(7);
        }
    }
}
