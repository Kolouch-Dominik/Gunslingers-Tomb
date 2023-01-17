using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [field: SerializeField] public string LevelToLoad { get; set; }
    [field: SerializeField] public GameObject DeletePanel { get; set; }
    [field: SerializeField] public List<CharacterSelector> CharToDelete { get; set; }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void DeleteSave()
    {
        DeletePanel.SetActive(true);
    }
    public void ConfirmDelete()
    {
        DeletePanel.SetActive(false);

        CharToDelete.ForEach(x => PlayerPrefs.SetInt(x.PlayerToSpawn.name, 0));
    }
    public void CancelDelete()
    {
        DeletePanel.SetActive(false);
    }
}
