using UnityEngine;

public class MScene : MonoBehaviour
{
    public GameObject[] _playButton;
    public bool _isFirstGame;

    private void Start()
    {
        LoadButton();
    }

    public void LoadButton()
    {
        _isFirstGame = PlayerPrefs.GetInt("_isFirstGame", 1) == 1;

        if (_isFirstGame)
        {
            _playButton[0].SetActive(true);
            _playButton[1].SetActive(false);
        }
        else
        {
            _playButton[0].SetActive(false);
            _playButton[1].SetActive(true);
        }
    }

    public void SaveButton()
    {
        _isFirstGame = false; // Инвертируем значение _isFirstGame
        PlayerPrefs.SetInt("_isFirstGame", _isFirstGame ? 1 : 0);
    }
}
