using UnityEngine;

public class RotateAroundCenterScript : MonoBehaviour {
  [Tooltip("Скорость вращения камеры вокруг оси Y")]
  public float rotationSpeed = 10f;

  private void Update() {
    // Поворачиваем CameraCenter вокруг оси Y
    transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
  }
}
