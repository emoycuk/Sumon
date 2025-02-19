using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject darkPanel;

    void Start()
    {
        darkPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            darkPanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
            darkPanel.SetActive(false);
        }
    }
}
