using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [field: SerializeField] public GameObject BulletToFire { get; set; }
    [field: SerializeField] public Transform FirePoint { get; set; }
    [field: SerializeField] public float TimeBetweenShots { get; set; }
    [field: SerializeField] public string WeaponName { get; set; }
    [field: SerializeField] public Sprite GunUI { get; set; }
    private float shotCounter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.CanMove && !LevelManager.Instance.IsPaused)
        {
            if (shotCounter > 0)
            {
                shotCounter -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    Instantiate(BulletToFire, FirePoint.position, FirePoint.rotation); //vytvoøení instance objektu (objekt, pozice, rotace)

                    shotCounter = TimeBetweenShots;
                    AudioManager.Instance.PlaySFX(12);
                }
            }
        }
    }
}
