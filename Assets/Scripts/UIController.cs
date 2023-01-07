using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; set; }
    [field: SerializeField] public Slider HealthSlider { get; set; }
    [field: SerializeField] public Text HealthText { get; set; }
    [field: SerializeField] public Text CoinText { get; set; }
    [field: SerializeField] public GameObject DeathScreen { get; set; }
    [field: SerializeField] public Image FadeScreen { get; set; }
    [field: SerializeField] public GameObject PauseMenu { get; set; }
    [field: SerializeField] public GameObject MapDisplay { get; set; }
    [field: SerializeField] public GameObject BigMapText { get; set; }


    private float fadeSpeed = 1f;
    private bool fadeToBlack, fadeOutBlack;
    [field: SerializeField] public string NewGameScene { get; set; }
    [field: SerializeField] public string MainMenuScene { get; set; }




    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
            var c = FadeScreen.color;
            FadeScreen.color = new Color(c.r, c.g, c.b, Mathf.MoveTowards(c.a, 0f, fadeSpeed * Time.deltaTime));
            if (FadeScreen.color.a.Equals(0))
                fadeOutBlack = false;
        }
        if (fadeToBlack)
        {
            var c = FadeScreen.color;
            FadeScreen.color = new Color(c.r, c.g, c.b, Mathf.MoveTowards(c.a, 1f, fadeSpeed * Time.deltaTime));
            if (FadeScreen.color.a.Equals(1))
                fadeToBlack = false;
        }
    }
    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(NewGameScene);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(MainMenuScene);
    }

    public void Resume()
    {
        LevelManager.Instance.PauseUnpause();
    }
}
