using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        // Check if a volume setting exists, otherwise set it to 1
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
        }

        Load(); // Load the saved volume and apply it immediately

        // Add a listener to update volume when slider changes
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
        AudioListener.volume = savedVolume; // Apply volume globally
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume; // Set slider value if UI is loaded
        }
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.Save();
    }
}