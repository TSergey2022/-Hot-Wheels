using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IngameMenuCanvasScript : MonoBehaviour {
  // Панели
  [SerializeField] private GameObject MenuPanel;
  [SerializeField] private GameObject OptionsPanel;
  [SerializeField] private GameObject LeaderboardPanel;
  
  // Записи в таблице лидеров
  [SerializeField] private GameObject LeaderboardInner2;
  [SerializeField] private GameObject LeaderboardEntryPrefab;

  public void ResumeGame() {
    gameObject.SetActive(true);
  }
  
  // Метод для открытия главного меню
  public void OpenMenu() {
    CloseAllPanels(); // Закрыть все панели перед открытием новой
    MenuPanel.SetActive(true);
  }

  // Метод для открытия панели опций
  public void OpenOptions() {
    CloseAllPanels(); // Закрыть все панели перед открытием новой
    OptionsPanel.SetActive(true);
  }

  // Метод для открытия панели лидеров
  public void OpenLeaderboard() {
    CloseAllPanels();
    for (var i = LeaderboardInner2.transform.childCount - 1; i >= 0; i--) {
      var go = LeaderboardInner2.transform.GetChild(i).gameObject;
      if (go.CompareTag("LeaderboardEntry"))
        Destroy(go);
    }
    foreach (var entry in GodScript.Instance.Leaderboard) {
      var leaderboardEntryGo = Instantiate(LeaderboardEntryPrefab, LeaderboardInner2.transform);
      var script = leaderboardEntryGo.GetComponent<LeaderboardEntryScript>();
      script.SetText(entry);
    }
    LeaderboardPanel.SetActive(true);
  }

  public void ToMainMenu() {
    SceneManager.LoadScene("MainScene");
  }

  // Метод для закрытия всех панелей
  private void CloseAllPanels() {
    MenuPanel.SetActive(false);
    OptionsPanel.SetActive(false);
    LeaderboardPanel.SetActive(false);
  }
}
