using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    public float MoveSpeed { get; private set; }
    [field: SerializeField]
    public Animator Anim { get; private set; }
    public Rigidbody2D theRB;
    public Transform gunArm;
    private Camera cam;

    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; //v updatu volat Camera.Main --> moc nároèná opera, staèí jednou pøi startu
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();      //odstranìní chybi pøi držení "w" + smìr do strany (diagonální pohyb) už není rychlejší

        theRB.velocity = moveInput * MoveSpeed;

        var mousePosition = Input.mousePosition;    //pozice myši
        var screenPoint = cam.WorldToScreenPoint(transform.localPosition);  //pozice hráèe
         
        //otáèení podle smìru pohledu postavy
        if (mousePosition.x < screenPoint.x)
        {
            ScaleTransform(transform, new Vector3(-1f,1f,1f));
            ScaleTransform(gunArm, new Vector3(-1f,-1f,1f));
        }
        else if (mousePosition.x > screenPoint.x)
        {
            ScaleTransform(transform, Vector3.one);
            ScaleTransform(gunArm, Vector3.one);
        }

        var offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);

        var angle = Mathf.Atan2(offset.y, offset.x)* Mathf.Rad2Deg;

        gunArm.rotation = Quaternion.Euler(0,0,angle);

        Anim.SetBool("isMoving", moveInput != Vector2.zero);
    }

    void ScaleTransform(Transform transformToScale, Vector3 scale)
    {
        transformToScale.localScale = scale;
    }
}
