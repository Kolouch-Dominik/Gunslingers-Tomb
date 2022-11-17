using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    public float MoveSpeed { get; private set; }
    public Rigidbody2D theRB;
    public Transform gunArm;

    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        theRB.velocity = moveInput * MoveSpeed;

        var mousePosition = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        var offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        var angle = Mathf.Atan2(offset.y, offset.x)* Mathf.Rad2Deg;

        gunArm.rotation = Quaternion.Euler(0,0,angle);
    }
}
