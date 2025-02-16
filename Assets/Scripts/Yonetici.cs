using UnityEngine;
using UnityEngine.UI;

public class Yonetici : MonoBehaviour
{
    public bool oyunudurdumu = false;
    public GameObject karartmaPanel; // Karartma paneli referansı

    void Start()
    {
        karartmaPanel.SetActive(false); // Oyun başladığında karartma kapalı olmalı
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // P tuşuna basınca çağrılır
        {
            oyunudurdur();
        }
    }

    public void oyunudurdur()
    {
        if (!oyunudurdumu)
        {
            Time.timeScale = 0f; // Oyunu duraklat
            oyunudurdumu = true;
            karartmaPanel.SetActive(true); // Ekranı karart
        }
        else
        {
            Time.timeScale = 1f; // Oyunu devam ettir
            oyunudurdumu = false;
            karartmaPanel.SetActive(false); // Karartmayı kaldır
        }
    }
}
