using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundScript : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler {
  [Header("Настройки звуков")]
  [Tooltip("Звук при наведении курсора на кнопку")]
  public AudioClip hoverSound;

  [Tooltip("Звук при нажатии на кнопку")]
  public AudioClip clickSound;

  [Header("Флаги воспроизведения звуков")]
  [Tooltip("Воспроизводить звук при наведении")]
  public bool playOnHover = true;

  [Tooltip("Воспроизводить звук при нажатии")]
  public bool playOnClick = true;

  // Метод, который вызывается при наведении курсора на кнопку
  public void OnPointerEnter(PointerEventData eventData) {
    if (playOnHover && hoverSound != null) {
      GodScript.Instance?.PlaySound(hoverSound);
    }
  }

  // Метод, который вызывается при нажатии на кнопку
  public void OnPointerClick(PointerEventData eventData) {
    if (playOnClick && clickSound != null) {
      GodScript.Instance?.PlaySound(clickSound);
    }
  }

}
