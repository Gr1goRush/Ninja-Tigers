using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private void Awake()
    {
        // Позволяет сделать этот объект постоянным при загрузке новых сцен
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Если объект уже существует в другой сцене, уничтожаем текущий
            Destroy(gameObject);
        }
    }
}
