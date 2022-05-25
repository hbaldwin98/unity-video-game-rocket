using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Debugging : MonoBehaviour
{
  CollisionHandler _collisionHandler;

  void Start()
  {
    _collisionHandler = GetComponent<CollisionHandler>();
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
      DisableCollisions();
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

  void DisableCollisions()
  {
    bool res = _collisionHandler.ToggleDebugMode();

    if (res) Debug.Log("Disabling collisions");
    else Debug.Log("Enabling collisions");
  }
}
