using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton pattern

    void Awake()
    {
        // Make sure there's only one AudioManager across all scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        ApplySavedVolume(); // Apply saved volume at game start
    }

    void ApplySavedVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("musicVolume");
            AudioListener.volume = savedVolume; // Apply saved volume globally
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", 1f); // Default volume
            PlayerPrefs.Save();
        }
    }
}