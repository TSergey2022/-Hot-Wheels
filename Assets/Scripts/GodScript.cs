using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Newtonsoft.Json;

public class GodScript : MonoBehaviour {
  private const string LeaderboardKey = "Leaderboard";
  private const string MusicVolumeKey = "MusicVolume";
  private const string SoundVolumeKey = "SoundVolume";
  public static GodScript Instance { get; private set; }

  [Tooltip("Ссылка на аудиомикшер")] [SerializeField]
  private AudioMixer audioMixer;
  
  [Tooltip("Имя параметра громкости для группы Music")]
  public string musicVolumeParameter = "MusicVolume";

  [Tooltip("Имя параметра громкости для группы Sound")]
  public string soundVolumeParameter = "SoundVolume";
  
  [SerializeField] private AudioSource soundAudioSource;

  public List<LeaderboardEntry> Leaderboard { get; private set; }

  public float MusicVolume {
    get {
      audioMixer.GetFloat(musicVolumeParameter, out var value);
      return DbToSliderValue(value);
    }
    set {
      audioMixer.SetFloat(musicVolumeParameter, SliderValueToDb(value));
    }
  }
  
  public float SoundVolume {
    get {
      audioMixer.GetFloat(soundVolumeParameter, out var value);
      return DbToSliderValue(value);
    }
    set {
      audioMixer.SetFloat(soundVolumeParameter, SliderValueToDb(value));
    }
  }

  private void Awake() {
    Instance = this;
    var leaderboardJson = PlayerPrefs.GetString(LeaderboardKey, "[]");
    try {
      Leaderboard = JsonConvert.DeserializeObject<List<LeaderboardEntry>>(leaderboardJson);
    } catch {}
    MusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
    SoundVolume = PlayerPrefs.GetFloat(SoundVolumeKey, 0.5f);
  }

  private void OnApplicationQuit() {
    PlayerPrefs.SetString(LeaderboardKey, JsonConvert.SerializeObject(Leaderboard));
    PlayerPrefs.SetFloat(MusicVolumeKey, MusicVolume);
    PlayerPrefs.SetFloat(SoundVolumeKey, SoundVolume);
    PlayerPrefs.Save();
  }

  public void PlaySound(AudioClip clip) {
    soundAudioSource.PlayOneShot(clip);
  }

  // Преобразование децибел в значение слайдера (0-1)
  private static float DbToSliderValue(float db) {
    return Mathf.Pow(10, db / 20);
  }

  // Преобразование значения слайдера (0-1) в децибелы (-80 до 0)
  private static float SliderValueToDb(float sliderValue) {
    return sliderValue > 0 ? Mathf.Log10(sliderValue) * 20 : -80f;
  }
}
