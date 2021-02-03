using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidGraph : MonoBehaviour
{
    public PyramidNode[] Nodes = new PyramidNode[28];

    public Sprite m_startSprite;
    public Sprite m_changeSprite;

    enum CONNECT_DIRECTION
    {
        TOP_R,
        TOP_L,
        BOT_R,
        BOT_L
    }

    // Start is called before the first frame update
    void Start()
    {
        // Create the Node layout
        BuildGraph();
        // Assign sprites to each Node, set them all to unactivated
        foreach(PyramidNode node in Nodes)
        {
            node.m_startSprite = m_startSprite;
            node.m_changeSprite = m_changeSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildGraph()
    {
        LinkNodes(0, 1, CONNECT_DIRECTION.BOT_L);
        LinkNodes(0, 2, CONNECT_DIRECTION.BOT_R);

        ///

        LinkNodes(1, 3, CONNECT_DIRECTION.BOT_L);
        LinkNodes(1, 4, CONNECT_DIRECTION.BOT_R);

        LinkNodes(2, 4, CONNECT_DIRECTION.BOT_L);
        LinkNodes(2, 5, CONNECT_DIRECTION.BOT_R);

        ///

        LinkNodes(3, 6, CONNECT_DIRECTION.BOT_L);
        LinkNodes(3, 7, CONNECT_DIRECTION.BOT_R);

        LinkNodes(4, 7, CONNECT_DIRECTION.BOT_L);
        LinkNodes(4, 8, CONNECT_DIRECTION.BOT_R);

        LinkNodes(5, 8, CONNECT_DIRECTION.BOT_L);
        LinkNodes(5, 9, CONNECT_DIRECTION.BOT_R);

        ///

        LinkNodes(6, 10, CONNECT_DIRECTION.BOT_L);
        LinkNodes(6, 11, CONNECT_DIRECTION.BOT_R);

        LinkNodes(7, 11, CONNECT_DIRECTION.BOT_L);
        LinkNodes(7, 12, CONNECT_DIRECTION.BOT_R);

        LinkNodes(8, 12, CONNECT_DIRECTION.BOT_L);
        LinkNodes(8, 13, CONNECT_DIRECTION.BOT_R);

        LinkNodes(9, 13, CONNECT_DIRECTION.BOT_L);
        LinkNodes(9, 14, CONNECT_DIRECTION.BOT_R);

        ///

        LinkNodes(10, 15, CONNECT_DIRECTION.BOT_L);
        LinkNodes(10, 16, CONNECT_DIRECTION.BOT_R);

        LinkNodes(11, 16, CONNECT_DIRECTION.BOT_L);
        LinkNodes(11, 17, CONNECT_DIRECTION.BOT_R);

        LinkNodes(12, 17, CONNECT_DIRECTION.BOT_L);
        LinkNodes(12, 18, CONNECT_DIRECTION.BOT_R);

        LinkNodes(13, 18, CONNECT_DIRECTION.BOT_L);
        LinkNodes(13, 19, CONNECT_DIRECTION.BOT_R);

        LinkNodes(14, 19, CONNECT_DIRECTION.BOT_L);
        LinkNodes(14, 20, CONNECT_DIRECTION.BOT_R);

        ///

        LinkNodes(15, 21, CONNECT_DIRECTION.BOT_L);
        LinkNodes(15, 22, CONNECT_DIRECTION.BOT_R);

        LinkNodes(16, 22, CONNECT_DIRECTION.BOT_L);
        LinkNodes(16, 23, CONNECT_DIRECTION.BOT_R);

        LinkNodes(17, 23, CONNECT_DIRECTION.BOT_L);
        LinkNodes(17, 24, CONNECT_DIRECTION.BOT_R);

        LinkNodes(18, 24, CONNECT_DIRECTION.BOT_L);
        LinkNodes(18, 25, CONNECT_DIRECTION.BOT_R);

        LinkNodes(19, 25, CONNECT_DIRECTION.BOT_L);
        LinkNodes(19, 26, CONNECT_DIRECTION.BOT_R);

        LinkNodes(20, 26, CONNECT_DIRECTION.BOT_L);
        LinkNodes(20, 27, CONNECT_DIRECTION.BOT_R);

    }

    

    void LinkNodes(int index1, int index2, CONNECT_DIRECTION dir)
    {
        switch(dir)
        {
            case CONNECT_DIRECTION.TOP_R:
                Nodes[index1].m_TopRight = Nodes[index2];
                Nodes[index2].m_BotLeft = Nodes[index1];
                break;
            case CONNECT_DIRECTION.TOP_L:
                Nodes[index1].m_TopLeft = Nodes[index2];
                Nodes[index2].m_BotRight = Nodes[index1];
                break;
            case CONNECT_DIRECTION.BOT_R:
                Nodes[index1].m_BotRight = Nodes[index2];
                Nodes[index2].m_TopLeft = Nodes[index1];
                break;
            case CONNECT_DIRECTION.BOT_L:
                Nodes[index1].m_BotLeft = Nodes[index2];
                Nodes[index2].m_TopRight = Nodes[index1];
                break;
        }
    }
}
