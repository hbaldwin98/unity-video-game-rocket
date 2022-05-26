using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugging : MonoBehaviour
{
  [SerializeField] Canvas pauseMenu;

  CollisionHandler _collisionHandler;
  GameMusicPlayer _musicPlayer;
  AudioSource _audioSource;

  void Start()
  {
    _collisionHandler = GetComponent<CollisionHandler>();
    _audioSource = GetComponent<AudioSource>();
    _musicPlayer = FindObjectOfType<GameMusicPlayer>();
    
    
  }

  void Update()
  {
    ProcessInput();
  }

  void ProcessInput()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNextScene();
    }    
    if (Input.GetKeyDown(KeyCode.C))
    {
      ToggleCollisions();
    }

    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
    {
      TogglePause();
    }
  }

  void LoadNextScene()
  {
    int totalSceneCount = SceneManager.sceneCountInBuildSettings;
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
    if (nextSceneIndex >= totalSceneCount)
    {
      Debug.Log("Loading scene 0");
      SceneManager.LoadScene(0);
      return;
    }
      
    Debug.Log("Loading scene " + nextSceneIndex);
    SceneManager.LoadScene(nextSceneIndex);
  }

  void ToggleCollisions()
  {
    bool res = _collisionHandler.ToggleDebugMode();

    Debug.Log(res ? "Disabling collisions" : "Enabling collisions");
  }

  void TogglePause()
  {
    if (Time.timeScale == 1)
    {
      _musicPlayer.PauseMusic();
      _audioSource.Pause();
      pauseMenu.gameObject.SetActive(true);
      Time.timeScale = 0;
      return;
    }
    
    Time.timeScale = 1;
    pauseMenu.gameObject.SetActive(false);
    _audioSource.Play();
    _musicPlayer.UnPauseMusic();
  }
}
