using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Text m_TScore;

    private int m_iScore;

    private void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        SetScore(0);
        UpdateText();
    }

    public void AddScore(int score)
    {
        m_iScore += score;
        UpdateText();
    }

    private void SetScore(int score)
    {
        m_iScore = score;
        UpdateText();
    }

    private void UpdateText()
    {
        m_TScore.text = m_iScore.ToString();
    }
}
