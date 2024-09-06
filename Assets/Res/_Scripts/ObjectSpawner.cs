using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;  // Префаб объекта для спавна
    public Transform spawnPoint;       // Точка, где будет спауниться объект
    public float spawnChance = 0.3f;   // Вероятность спавна (30% в данном случае)

    private void Start()
    {
        TrySpawnObject();
    }

    private void TrySpawnObject()
    {
        // Генерируем случайное число от 0 до 1
        float randomValue = Random.value;

        // Если случайное число меньше или равно вероятности спавна, то спауним объект
        if (randomValue <= spawnChance)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        // Проверяем, есть ли объект для спавна
        if (objectToSpawn != null && spawnPoint != null)
        {
            // Спауним объект в заданной точке
            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}
