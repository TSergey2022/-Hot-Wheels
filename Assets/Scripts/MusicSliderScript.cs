using UnityEngine;
using UnityEngine.UI;

public class MusicSliderScript : MonoBehaviour {
  [SerializeField] private Slider slider;

  private void Start() {
    slider.value = GodScript.Instance?.MusicVolume ?? 0f;
  }

  public void SetMusicVolume(float sliderValue) {
    if (GodScript.Instance)
      GodScript.Instance.MusicVolume = sliderValue;
  }
}
