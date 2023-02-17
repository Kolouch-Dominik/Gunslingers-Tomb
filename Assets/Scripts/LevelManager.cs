using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }
    private float waitToLoad = 4;
    [field: SerializeField] public bool IsPaused { get; set; }
    [field: SerializeField] public int CurrentCoins { get; private set; }
    [field: SerializeField] public string NextLevel { get; set; }
    [field: SerializeField] public Transform StartPoint { get; set; }


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        PlayerController.Instance.transform.position = StartPoint.position;
        PlayerController.Instance.CanMove = true;

        CurrentCoins = CharacterTracker.Instance.CurrentCoins;

        Time.timeScale = 1f;

        UIController.Instance.CoinText.text = CurrentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseUnpause();
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.Instance.PlayWinMusic();

        PlayerController.Instance.CanMove = false;

        UIController.Instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);

        CharacterTracker.Instance.CurrentCoins = CurrentCoins;
        CharacterTracker.Instance.MaxHealth = PlayerHealthController.Instance.MaxHealth;
        CharacterTracker.Instance.CurrentHealth = PlayerHealthController.Instance.CurrentHealth;

        SceneManager.LoadScene(NextLevel);
    }

    public void PauseUnpause()
    {

        if (!IsPaused)
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

    public void AddCoins(int amount)
    {
        CurrentCoins += amount;

        UIController.Instance.CoinText.text = CurrentCoins.ToString();
    }
    public void SpendCoins(int amount)
    {
        CurrentCoins -= amount;

        if (CurrentCoins < 0)
            CurrentCoins = 0;

        UIController.Instance.CoinText.text = CurrentCoins.ToString();
    }
}
