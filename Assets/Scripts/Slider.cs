using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
        }
        
        Load(); 

        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        float savedVolume = PlayerPrefs.GetFloat("musicVolume");
        AudioListener.volume = savedVolume;
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }
}