using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; set; }
    [field: SerializeField]
    public float MoveSpeed { get; set; }
    [field: SerializeField]
    public Transform Target { get; private set; }


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
            transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(Target.position.x, Target.position.y, transform.position.z), MoveSpeed*Time.deltaTime);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
