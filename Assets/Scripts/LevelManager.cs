using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }
    private float waitToLoad = 4;
    [field: SerializeField]
    public bool IsPaused { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseUnpause();
    }

    public IEnumerator LevelEnd(string LevelToLoad)
    {
        AudioManager.Instance.PlayWinMusic();

        PlayerController.Instance.CanMove = false;

        UIController.Instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(LevelToLoad);
    }

    public void PauseUnpause()
    {
        
        if(!IsPaused)
        {
            UIController.Instance.PauseMenu.SetActive(true); 

            Time.timeScale = 0f;
        }
        else
        {
            UIController.Instance.PauseMenu.SetActive(false);

            Time.timeScale = 1f;
        }
        IsPaused = !IsPaused;
    }
}
