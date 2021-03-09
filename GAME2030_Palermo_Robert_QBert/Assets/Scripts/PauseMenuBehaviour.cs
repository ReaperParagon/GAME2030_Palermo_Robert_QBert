using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_pauseMenu;

    public bool m_paused = false;

    private void Start()
    {
        if(m_pauseMenu == null)
        {
            GameObject.Find("PauseMenu");
        }
    }

    public void ShowPauseMenu()
    {
        Time.timeScale = 0.0f;
        m_paused = true;
        m_pauseMenu.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        m_paused = false;
        m_pauseMenu.SetActive(false);
    }
}
