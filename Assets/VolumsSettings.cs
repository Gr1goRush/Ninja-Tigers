using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumsSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Toggle musicToggle;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolum();
        }

        // Назначаем метод обработчика события для Toggle
        musicToggle.onValueChanged.AddListener(ToggleMusic);

        // Инициализируем состояние AudioSource в соответствии с начальным состоянием Toggle
        ToggleMusic(musicToggle.isOn);
    }

    public void SetMusicVolum()
    {
        float volume = musicSlider.value;
        audioMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);        
    }

    void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolum();

        // Загружаем и устанавливаем состояние Toggle
        musicToggle.isOn = PlayerPrefs.GetInt("musicToggleState") == 1;
    }

    // Метод, который будет вызываться при изменении состояния Toggle
    public void ToggleMusic(bool isMusicOn)
    {
        // Включаем или выключаем AudioSource в зависимости от состояния Toggle
        audioSource.enabled = isMusicOn;

        // Если нужно также воспроизводить/останавливать музыку, то можно добавить следующую строку:
        if (isMusicOn) audioSource.Play(); else audioSource.Stop();

        // Сохраняем состояние Toggle в PlayerPrefs
        PlayerPrefs.SetInt("musicToggleState", musicToggle.isOn ? 1 : 0);
    }
}
