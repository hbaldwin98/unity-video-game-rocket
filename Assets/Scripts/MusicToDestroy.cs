using System.Collections.Generic;
using UnityEngine;

public class MusicToDestroy : MonoBehaviour
{
  static MusicToDestroy _instance;
  List<GameMusicPlayer> _destroy = new(); 
  
  public static MusicToDestroy Instance => _instance;
  void Awake()
  {
    if (_instance != null && _instance != this)
    {
      Destroy(gameObject);
      return;
    }

    _instance = this;

    DontDestroyOnLoad(this);
  }
  
  public void AddToDestroy(GameMusicPlayer music)
  {
    _destroy.Add(music);
  }

  public void DestroyAll()
  {
    if (_destroy.Count == 0) return;

    foreach (var gameMusicPlayer in _destroy)
    {
      Destroy(gameMusicPlayer.gameObject);
    }

    _destroy = new();
  }
}
