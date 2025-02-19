using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    void Awake()
    {
        // Make sure there's only one AudioManager across all scenes
        if (Instance == null)
        {
            Instance = this;

            // Persist across scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Apply saved volume at game start
        ApplySavedVolume(); 
    }

    void ApplySavedVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("musicVolume");
            AudioListener.volume = savedVolume;
        }
        else
        {
            PlayerPrefs.SetFloat("musicVolume", 1f);
            PlayerPrefs.Save();
        }
    }
}