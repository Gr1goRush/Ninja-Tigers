using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public PlayerController playerController;

    private int currentScore = 0;
    private int savedScore = 0;
    private float scoreIncrementRate = 10.0f; // �������� ��� �������� �� ������ ����������
    private float elapsedTime = 0.0f;

    private void Start()
    {
        // ��������� ����������� ���������� �����
        if (PlayerPrefs.HasKey("SavedScore"))
        {
            savedScore = PlayerPrefs.GetInt("SavedScore");
        }

        UpdateScoreText();
    }

    private void Update()
    {
        // ���������, ��� �� ����� (���� ������ ����� ����������)
        bool isPlayerAlive = !playerController.isDeath; // ���������� ���� ������ �������� ����� ������

        if (isPlayerAlive)
        {
            // ����������� ��������� �����
            elapsedTime += Time.deltaTime;

            // ���������� Mathf.Lerp ��� �������� ���������� �����
            float targetScore = scoreIncrementRate * elapsedTime;
            currentScore = Mathf.FloorToInt(Mathf.Lerp(0, targetScore, Mathf.Clamp01(elapsedTime)));

            UpdateScoreText();
        }
        else
        {
            // ����� �����, ��������� ���������
            SaveScore();
        }
    }

    private void UpdateScoreText()
    {
        // ��������� ����� � ������� ����������� �����
        scoreText.text = currentScore.ToString();
    }

    private void SaveScore()
    {
        // ���������, ��������� �� ������� ���������� ����� �����������
        if (currentScore > savedScore)
        {
            // ��������� ������� ���������� �����
            PlayerPrefs.SetInt("SavedScore", currentScore);
            PlayerPrefs.Save();

            // ��������� ����������� ���������� �����
            savedScore = currentScore;
        }
    }
}
