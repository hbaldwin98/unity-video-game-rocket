using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelDelay;
  [SerializeField] float crashDelay;
  [SerializeField] AudioClip crashSound;
  [SerializeField] AudioClip successSound;
  
  AudioSource _audioSource;
  bool _isTransitioning;
  void Start()
  {
    _audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision collision)
  {
    if (_isTransitioning) return;
    
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
    _isTransitioning = true;
    GetComponent<PlayerController>().DisableControls();
    PlayCrashSound();
    Invoke(nameof(ReloadLevel), crashDelay);
  }

  void StartSuccessSequence()
  {
    _isTransitioning = true;
    GetComponent<PlayerController>().DisableControls();
    PlaySuccessSound();
    Invoke(nameof(LoadNextLevel), levelDelay);
  }
  
  void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
  }

  void LoadNextLevel()
  {
    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
    SceneManager.LoadScene(nextSceneIndex >= SceneManager.sceneCountInBuildSettings ? 0 : nextSceneIndex);
  }
  void PlayCrashSound()
  {
    _audioSource.volume = 0.1f;
    _audioSource.Stop();
    _audioSource.PlayOneShot(crashSound);

  }
  void PlaySuccessSound()
  {
    _audioSource.volume = 0.1f;
    _audioSource.Stop();
    _audioSource.PlayOneShot(successSound);
  }
}
