using System.Collections.Generic;
using System.Linq;
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

  public List<LeaderboardEntry> Leaderboard { get; private set; } = new ();

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
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    var leaderboardJson = PlayerPrefs.GetString(LeaderboardKey, "[]");
    try {
      Leaderboard = JsonConvert.DeserializeObject<List<LeaderboardEntry>>(leaderboardJson);
    } catch {}
    if (Leaderboard.Count == 0) {
      AddLeaderboardEntry("Amogus", 100);
      AddLeaderboardEntry("Amogus Kid", 80);
      AddLeaderboardEntry("Stun Seed", 60);
      AddLeaderboardEntry("deeS nutS", 50);
      AddLeaderboardEntry("ABOBA", 33);
    }
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

  private void RecalculateLeaderboardPlaces() {
    // Сортируем записи по убыванию score
    var sortedLeaderboard = Leaderboard.OrderByDescending(entry => entry.score).ToList();

    // Проставляем номера места
    for (int i = 0; i < sortedLeaderboard.Count; i++)
    {
      sortedLeaderboard[i].place = i + 1;
    }

    // Оставляем только первые 10 записей, если их больше 10
    if (sortedLeaderboard.Count > 10)
    {
      sortedLeaderboard = sortedLeaderboard.Take(10).ToList();
    }

    Leaderboard = sortedLeaderboard;
  }
  
  private void AddLeaderboardEntry(string nickname, int score) {
    if (Leaderboard.Count < 10) {
      Leaderboard.Add(new LeaderboardEntry() {
        place = 10,
        nickname = nickname,
        score = score
      });
    } else {
      for (var i = 0; i < Leaderboard.Count; i++) {
        var entry = Leaderboard[i];
        if (score > entry.score) {
          Leaderboard.Insert(i, new LeaderboardEntry() {
            place = i + 1,
            nickname = nickname,
            score = score
          });
        }
      }
    }
    RecalculateLeaderboardPlaces();
  }
}
