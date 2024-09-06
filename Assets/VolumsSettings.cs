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

        // ��������� ����� ����������� ������� ��� Toggle
        musicToggle.onValueChanged.AddListener(ToggleMusic);

        // �������������� ��������� AudioSource � ������������ � ��������� ���������� Toggle
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

        // ��������� � ������������� ��������� Toggle
        musicToggle.isOn = PlayerPrefs.GetInt("musicToggleState") == 1;
    }

    // �����, ������� ����� ���������� ��� ��������� ��������� Toggle
    public void ToggleMusic(bool isMusicOn)
    {
        // �������� ��� ��������� AudioSource � ����������� �� ��������� Toggle
        audioSource.enabled = isMusicOn;

        // ���� ����� ����� ��������������/������������� ������, �� ����� �������� ��������� ������:
        if (isMusicOn) audioSource.Play(); else audioSource.Stop();

        // ��������� ��������� Toggle � PlayerPrefs
        PlayerPrefs.SetInt("musicToggleState", musicToggle.isOn ? 1 : 0);
    }
}
