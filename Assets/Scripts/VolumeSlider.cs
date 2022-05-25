using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
  [SerializeField] AudioMixer mixer;

  public void SetLevel(float sliderValue)
  {
    mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
  }
}
