using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    string _text;
    TextMeshProUGUI _textComponent;

    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _textComponent = GetComponentInChildren<TextMeshProUGUI>();
        _text = GetComponentInChildren<TextMeshProUGUI>().text;
        _audioSource = GetComponentInChildren<AudioSource>();
        _textComponent.text = "";
        StartCoroutine(BuildText());
        // StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(1);
    
        while (!loadLevel.isDone)
        {
            yield return null;
        }
    }

    IEnumerator BuildText()
    {
        for (int i = 0; i < _text.Length; i++)
        {
            if (_text[i] == '.' || _text[i] == ' ')
            { 
                yield return new WaitForSeconds(0.1f);
            }
            _textComponent.text = _text.Substring( 0, i );
            if (_text[i] != ' ')
            { 
                _audioSource.Play();
            }
            
            yield return new WaitForSeconds(0.1f);
        }
        
        StartCoroutine(LoadNextLevel());
    }
}
