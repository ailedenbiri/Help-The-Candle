using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Önceki seviye bilgilerini saklamak için deðiþkenler
    private float previousPlayerSpeed;
    private float previousPlayerLeftRightSpeed;

    // Playerin baþlangýç hýzý
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
        SceneManager.LoadScene("MainMenu"); // Oyun baþladýðýnda ana menüyü yükle
    }

    public void OnLevelWasLoaded(int level)
    {
        if (level == 1) // 1. sahneye geldiðinde
        {
            SetPlayerSpeedToDefault(); // Oyuncu hýzýný varsayýlan deðere ayarla
        }
    }

    public IEnumerator Restart()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f); // Oyunu 1 saniye durdur
        SceneManager.LoadScene("LEVEL 1"); // Level 1 sahnesine yükle
        SetPlayerSpeedToDefault(); // Oyuncu hýzýný varsayýlan deðere ayarla
        Time.timeScale = 1; // Oyunun zaman akýþýný normale döndür
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
        previousPlayerSpeed = playerInitialSpeed; // Varsayýlan hýz deðeri
        previousPlayerLeftRightSpeed = playerInitialSpeed; // Varsayýlan sola saða hýzý
    }
}
