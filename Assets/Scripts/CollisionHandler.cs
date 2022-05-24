using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelDelay = 1f;
  [SerializeField] float crashDelay = 1f;
  void OnCollisionEnter(Collision collision)
  {
    string gameObjectTag = collision.gameObject.tag;
    switch (gameObjectTag)
    {
      case "Friendly":
        Debug.Log("You hit a friendly object.");
        break;
      case "Finish":
        StartSuccessSequence();
        break;
      default:
        StartCrashSequence();
        break;
    }
  }

  void StartCrashSequence()
  {
    // todo add SFX upon crash
    // todo add particle effect upon crash
    GetComponent<PlayerController>().DisableControls();
    Invoke("ReloadLevel", crashDelay);
  }

  void StartSuccessSequence()
  {
    GetComponent<PlayerController>().DisableControls();
    Invoke("LoadNextLevel", levelDelay);
  }
  
  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  void LoadNextLevel()
  {
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
    if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
    {
      SceneManager.LoadScene(0);
    }
    else
    {
      SceneManager.LoadScene(nextSceneIndex);
    }
  }
}
