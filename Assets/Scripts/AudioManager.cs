using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }
    [field: SerializeField] public AudioSource LevelMusic { get; set; }
    [field: SerializeField] public AudioSource GameOverMusic { get; set; }
    [field: SerializeField] public AudioSource WinMusic { get; set; }
    [field: SerializeField] public List<AudioSource> SFX { get; set; }


    private void Awake()
    {
        Instance = this;
    }

    public void PlayGameOver()
    {
        LevelMusic.Stop();
        GameOverMusic.Play();
    }

    public void PlayWinMusic()
    {
        LevelMusic.Stop();
        WinMusic.Play();
    }

    //Bohužel Dictionary nejdou serializovat v Unity, takže int jako odkaz do listu
    public void PlaySFX(int sfxNum)
    {
        var source = SFX[sfxNum];
        source.Stop();
        source.Play();
    }
}
