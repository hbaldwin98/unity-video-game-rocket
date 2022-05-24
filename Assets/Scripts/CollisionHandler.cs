using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  void OnCollisionEnter(Collision collision)
  {
    string gameObjectTag = collision.gameObject.tag;
    switch (gameObjectTag)
    {
      case "Friendly":
        Debug.Log("You hit a friendly object.");
        break;
      case "Finish":
        LoadNextLevel();
        break;
      default:
        ReloadLevel();
        break;
    }
  }
  
  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  void LoadNextLevel()
  {
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
    if (nextSceneIndex > SceneManager.sceneCountInBuildSettings)
    {
      SceneManager.LoadScene(0);
    }
    else
    {
      SceneManager.LoadScene(nextSceneIndex);
    }
  }
}
