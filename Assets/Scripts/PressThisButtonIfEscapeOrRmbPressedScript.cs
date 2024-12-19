using UnityEngine;
using UnityEngine.UI;

public class PressThisButtonIfEscapeOrRmbPressedScript : MonoBehaviour {
  private void Update() {
    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) {
      GetComponent<Button>().onClick.Invoke();
    }
  }
}
