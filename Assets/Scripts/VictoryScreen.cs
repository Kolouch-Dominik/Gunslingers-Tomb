using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    [field: SerializeField] public float KeyWaitTime { get; set; } = 2f;
    [field: SerializeField] public GameObject AnyKeyText { get; set; }
    [field: SerializeField] public string MainMenu { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;

        Destroy(PlayerController.Instance.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyWaitTime > 0f)
        {
            KeyWaitTime -= Time.deltaTime;
            if (KeyWaitTime <= 0f)
            {
                AnyKeyText.SetActive(true);
            }
        }
        else
        {
            if (Input.anyKeyDown)
                SceneManager.LoadScene(MainMenu);
        }
    }
}
