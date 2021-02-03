using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PyramidNode : MonoBehaviour
{
    public bool bActivated = false;
    public Transform m_transform;

    // Connected Nodes
    private PyramidNode* m_TopRight = null;
    private PyramidNode* m_TopLeft  = null;
    private PyramidNode* m_BotRight = null;
    private PyramidNode* m_BotLeft  = null;
}
