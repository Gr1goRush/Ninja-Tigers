using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // ������ ������� ��� ������
    public Transform spawnPoint;       // �����, ��� ����� ���������� ������
    public float spawnChance = 0.3f;   // ����������� ������ (30% � ������ ������)

    private void Start()
    {
        TrySpawnObject();
    }

    private void TrySpawnObject()
    {
        // ���������� ��������� ����� �� 0 �� 1
        float randomValue = Random.value;

        // ���� ��������� ����� ������ ��� ����� ����������� ������, �� ������� ������
        if (randomValue <= spawnChance)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        // ���������, ���� �� ������ ��� ������
        if (objectToSpawn != null && spawnPoint != null)
        {
            // ������� ������ � �������� �����
            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}
