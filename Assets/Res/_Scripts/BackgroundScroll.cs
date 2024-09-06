using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1f;

    private Transform cameraTransform;
    private float originalY;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        originalY = transform.position.y;
    }

    void Update()
    {
        // Двигаем фон влево
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // Получаем новую позицию фона
        float newY = cameraTransform.position.y;

        // Плавно позиционируем фон по y координате камеры
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, newY, Time.deltaTime), transform.position.z);
    }
}
