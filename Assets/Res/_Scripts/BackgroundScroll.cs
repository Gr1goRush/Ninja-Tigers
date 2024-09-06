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
        // ������� ��� �����
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // �������� ����� ������� ����
        float newY = cameraTransform.position.y;

        // ������ ������������� ��� �� y ���������� ������
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, newY, Time.deltaTime), transform.position.z);
    }
}
