using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusic : MonoBehaviour
{
    void Start()
    {
        GameMusicPlayer musicPlayer = FindObjectOfType<GameMusicPlayer>();
        if (musicPlayer)
        {
            musicPlayer.StopMusic();
        }
    }
}
