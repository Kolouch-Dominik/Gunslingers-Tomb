using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; set; }

    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public Transform Target { get; set; }
    [field: SerializeField] public Camera MainCamera { get; set; }
    [field: SerializeField] public Camera BigMapCamera { get; set; }

    private bool bigMapActive;


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
            new Vector3(Target.position.x, Target.position.y, transform.position.z), MoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Tab))
            if (!bigMapActive) ActivateBigMap();
            else DeactivateBigMap();

    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }

    public void ActivateBigMap()
    {
        if (LevelManager.Instance.IsPaused) return;

        bigMapActive = true;

        BigMapCamera.enabled = true;
        MainCamera.enabled = false;

        PlayerController.Instance.CanMove = false;
        Time.timeScale = 0f;
        UIController.Instance.MapDisplay.SetActive(false);
        UIController.Instance.BigMapText.SetActive(true);
    }
    public void DeactivateBigMap()
    {
        if (LevelManager.Instance.IsPaused) return;

        bigMapActive = false;

        BigMapCamera.enabled = false;
        MainCamera.enabled = true;

        PlayerController.Instance.CanMove = true;
        Time.timeScale = 1f;
        UIController.Instance.MapDisplay.SetActive(true);
        UIController.Instance.BigMapText.SetActive(false);
    }
}
