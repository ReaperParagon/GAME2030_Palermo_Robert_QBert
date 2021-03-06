using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PyramidNode : MonoBehaviour
{
    public bool bIsActivated = false;

    public Vector3 m_Position = Vector3.zero;
    public SpriteRenderer m_renderer;
    public Sprite m_startSprite;
    public Sprite m_changeSprite;

    // Connected Nodes
    public PyramidNode m_TopRight = null;
    public PyramidNode m_TopLeft  = null;
    public PyramidNode m_BotRight = null;
    public PyramidNode m_BotLeft  = null;

    private void Start()
    {
        m_Position = transform.position;
        // transform.localScale = Vector3.one * 1.0f;
        transform.position = new Vector3(m_Position.x, m_Position.y - 0.55f, 0.0f);

        m_renderer = GetComponent<SpriteRenderer>();
        m_renderer.sprite = m_startSprite;
    }

    private void Update()
    {
        if(m_renderer.sprite == null)
        {
            m_renderer.sprite = m_startSprite;
        }
        
        if(transform.position.z != 0.0f)
        {
            transform.position = new Vector3(m_Position.x, m_Position.y - 0.55f, 0.0f);
        }
    }

    // Changes Sprite to a different one
    public void ChangeSprite()
    {
        m_renderer.sprite = m_changeSprite;
    }
}
