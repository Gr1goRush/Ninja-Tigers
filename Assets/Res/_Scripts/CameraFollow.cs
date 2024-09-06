using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public float yMin = -5f;
    public float yMax = 5f;

    void LateUpdate()
    {
        if (player != null)
        {
            // ���������� ����� ������� ������
            Vector3 desiredCameraPosition = new Vector3(transform.position.x, player.position.y, transform.position.z);

            // ���������� SmoothDamp ��� �������� ���������� �� �������
            Vector3 smoothedCameraPosition = Vector3.Lerp(transform.position, desiredCameraPosition, smoothSpeed);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(smoothedCameraPosition.y, yMin, yMax), transform.position.z);

            // ���������� ����� ������� ������
            Vector3 desiredPlayerPosition = new Vector3(player.position.x, Mathf.Clamp(smoothedCameraPosition.y, yMin, yMax), player.position.z);

            // ���������� SmoothDamp ��� �������� �������� ������
            transform.position = Vector3.Lerp(player.position, desiredPlayerPosition, smoothSpeed);
        }
    }
}
