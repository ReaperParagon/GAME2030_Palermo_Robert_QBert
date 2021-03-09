using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitching : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MainMenu;

    [SerializeField]
    private GameObject m_Leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisplayLeaderboard()
    {
        m_Leaderboard.SetActive(true);
        m_MainMenu.SetActive(false);
    }

    public void DisplayMainMenu()
    {
        m_Leaderboard.SetActive(false);
        m_MainMenu.SetActive(true);
    }
}
