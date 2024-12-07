using UnityEngine;
using UnityEngine.UI;

public class SoundSliderScript : MonoBehaviour {
  [SerializeField] private Slider slider;

  private void Start() {
    slider.value = GodScript.Instance?.SoundVolume ?? 0f;
  }

  public void SetSoundVolume(float sliderValue) {
    if (GodScript.Instance)
      GodScript.Instance.SoundVolume = sliderValue;
  }
}
