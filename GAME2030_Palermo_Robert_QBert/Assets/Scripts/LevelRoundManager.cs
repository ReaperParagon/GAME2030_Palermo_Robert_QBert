using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelRoundManager : MonoBehaviour
{
    [SerializeField]
    private Text m_TLevel;

    [SerializeField]
    private Text m_TRound;

    private int m_iLevel;

    private int m_iRound;

    private void Start()
    {
        ResetLevel();
        ResetRound();
    }

    public void ResetRound()
    {
        SetRound(1);
        UpdateRoundText();
    }

    public void ResetLevel()
    {
        SetLevel(1);
        UpdateLevelText();
    }

    public void AddRound(int round)
    {
        m_iRound += round;
        UpdateRoundText();
    }

    public void AddLevel(int level)
    {
        m_iLevel += level;
        UpdateLevelText();
    }

    private void SetRound(int round)
    {
        m_iRound = round;
        UpdateRoundText();
    }

    private void SetLevel(int level)
    {
        m_iLevel = level;
        UpdateLevelText();
    }

    private void UpdateRoundText()
    {
        m_TRound.text = m_iRound.ToString();
    }

    private void UpdateLevelText()
    {
        m_TLevel.text = m_iLevel.ToString();
    }
}
