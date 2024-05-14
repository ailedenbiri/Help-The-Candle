using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject quitButton;

    [Header("Panel")]
    [SerializeField] private GameObject settingsPanel;

    private void SetGamePause(bool isPaused)
    {
        Time.timeScale = isPaused ? 0f : 1f;
        playButton.SetActive(isPaused);
        pauseButton.SetActive(!isPaused);
    }

    // Oyunu duraklat
    public void PauseGame()
    {
        SetGamePause(true);
    }

    // Oyunu devam ettir
    public void ResumeGame()
    {
        SetGamePause(false);
        quitButton.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("LEVEL 1");
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        SetGamePause(true);
        quitButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
