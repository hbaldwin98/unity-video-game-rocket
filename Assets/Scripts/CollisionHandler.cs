using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  [Header("Transitions (in seconds)")]
  [SerializeField] float levelDelay;
  [SerializeField] float crashDelay;
  
  [Header("Audio")]
  [SerializeField] AudioClip crashSound;
  [SerializeField] AudioClip successSound;  
  
  [Header("Particles")]
  [SerializeField] ParticleSystem crashParticles;
  [SerializeField] ParticleSystem successParticles;
  
  AudioSource _audioSource;
  bool _isTransitioning;
  bool _debugMode;
  void Start()
  {
    _audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision collision)
  {
    if (_isTransitioning || _debugMode) return;
    
    string gameObjectTag = collision.gameObject.tag;

    switch (gameObjectTag)
    {
      case "Friendly":
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
    crashParticles.Play();
    PlayCrashSound();
    Invoke(nameof(ReloadLevel), crashDelay);
  }
  
  void StartSuccessSequence()
  {
    _isTransitioning = true;
    GetComponent<PlayerController>().DisableControls();
    successParticles.Play();
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

  public bool ToggleDebugMode()
  {
    _debugMode = !_debugMode;

    return _debugMode;
  }
}
