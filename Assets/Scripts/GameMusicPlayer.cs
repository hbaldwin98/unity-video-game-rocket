using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{

  static GameMusicPlayer _instance;
  AudioSource _audioSource;
  
  public static GameMusicPlayer Instance => _instance;

  void Awake()
  {
    if (_instance != null && _instance != this)
    {
      Destroy(this.gameObject);
      return;
    }

    _instance = this;
    _audioSource = GetComponent<AudioSource>();
    
    DontDestroyOnLoad(this);
  }
  
  public void PlayMusic()
  {
    if (_audioSource.isPlaying) return;
    _audioSource.Play();
  }  
  
  public void PauseMusic()
  {
    _audioSource.Pause();
  }  
  
  public void UnPauseMusic()
  {
    _audioSource.UnPause();
  }
 
  public void StopMusic()
  {
    _audioSource.Stop();
  }
}
// any other methods you need
