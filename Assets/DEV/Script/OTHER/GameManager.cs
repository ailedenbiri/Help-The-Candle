using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // �nceki seviye bilgilerini saklamak i�in de�i�kenler
    private float previousPlayerSpeed;
    private float previousPlayerLeftRightSpeed;

    // Playerin ba�lang�� h�z�
    [SerializeField] private float playerInitialSpeed = 800f;

    public float PlayerInitialSpeed
    {
        get { return playerInitialSpeed; }
        set { playerInitialSpeed = value; }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.LoadScene("MainMenu"); // Oyun ba�lad���nda ana men�y� y�kle
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level == 1) // 1. sahneye geldi�inde
        {
            SetPlayerSpeedToDefault(); // Oyuncu h�z�n� varsay�lan de�ere ayarla
        }
    }

    public IEnumerator Restart()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f); // Oyunu 1 saniye durdur
        SceneManager.LoadScene("LEVEL 1"); // Level 1 sahnesine y�kle
        SetPlayerSpeedToDefault(); // Oyuncu h�z�n� varsay�lan de�ere ayarla
        Time.timeScale = 1; // Oyunun zaman ak���n� normale d�nd�r
    }

    public void SavePreviousPlayerPrefs(float speed, float leftRightSpeed)
    {
        previousPlayerSpeed = speed;
        previousPlayerLeftRightSpeed = leftRightSpeed;
    }

    public void LoadPreviousPlayerPrefs(out float speed, out float leftRightSpeed)
    {
        speed = previousPlayerSpeed;
        leftRightSpeed = previousPlayerLeftRightSpeed;
    }

    public void ClearPreviousPlayerPrefs()
    {
        previousPlayerSpeed = 0f;
        previousPlayerLeftRightSpeed = 0f;
    }

    public void SetPlayerSpeedToDefault()
    {
        previousPlayerSpeed = playerInitialSpeed; // Varsay�lan h�z de�eri
        previousPlayerLeftRightSpeed = playerInitialSpeed; // Varsay�lan sola sa�a h�z�
    }
}
