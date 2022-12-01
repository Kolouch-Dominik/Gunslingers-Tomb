using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; set; }
    private float waitToLoad = 4;
    [field: SerializeField]
    public string NextLevel { get; set; }
    public Room CurrentRoom { get; set; }

    // Start is called before the first frame update
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
        
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.Instance.PlayWinMusic();

        PlayerController.Instance.CanMove = false;

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(NextLevel);
    }
}
