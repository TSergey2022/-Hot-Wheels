using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntryScript : MonoBehaviour {
  [SerializeField] private Text placeAndScoreText;
  [SerializeField] private Text scoreText;

  public void SetText(LeaderboardEntry entry) {
    placeAndScoreText.text = $"{entry.place}. {entry.nickname}";
    scoreText.text = $"{entry.score}";
  }
}
