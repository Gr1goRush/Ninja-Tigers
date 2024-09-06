using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public PlayerController playerController;

    private int currentScore = 0;
    private int savedScore = 0;
    private float scoreIncrementRate = 10.0f; // Измените это значение по вашему усмотрению
    private float elapsedTime = 0.0f;

    private void Start()
    {
        // Загружаем сохраненное количество очков
        if (PlayerPrefs.HasKey("SavedScore"))
        {
            savedScore = PlayerPrefs.GetInt("SavedScore");
        }

        UpdateScoreText();
    }

    private void Update()
    {
        // Проверяем, жив ли игрок (ваша логика может отличаться)
        bool isPlayerAlive = !playerController.isDeath; // Реализуйте свою логику проверки жизни игрока

        if (isPlayerAlive)
        {
            // Увеличиваем прошедшее время
            elapsedTime += Time.deltaTime;

            // Используем Mathf.Lerp для плавного начисления очков
            float targetScore = scoreIncrementRate * elapsedTime;
            currentScore = Mathf.FloorToInt(Mathf.Lerp(0, targetScore, Mathf.Clamp01(elapsedTime)));

            UpdateScoreText();
        }
        else
        {
            // Игрок мертв, сохраняем результат
            SaveScore();
        }
    }

    private void UpdateScoreText()
    {
        // Обновляем текст с текущим количеством очков
        scoreText.text = currentScore.ToString();
    }

    private void SaveScore()
    {
        // Проверяем, превышает ли текущее количество очков сохраненное
        if (currentScore > savedScore)
        {
            // Сохраняем текущее количество очков
            PlayerPrefs.SetInt("SavedScore", currentScore);
            PlayerPrefs.Save();

            // Обновляем сохраненное количество очков
            savedScore = currentScore;
        }
    }
}
