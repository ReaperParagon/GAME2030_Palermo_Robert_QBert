using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidGraph : MonoBehaviour
{
    public PyramidNode[] Nodes = new PyramidNode[50];

    public Sprite m_startSprite;
    public Sprite m_changeSprite;

    // private int[][] m_adjMatrix;
    // private int[] m_vertVisits;

    enum CONNECT_DIRECTION
    {
        TOP_R,
        TOP_L,
        BOT_R,
        BOT_L
    }

    enum NODE_TYPE
    {
        DEFAULT,
        VOID,
        ELEVATOR
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        m_adjMatrix = new int[28][];    // Instantiates 2D array as a whole

        m_vertVisits = new int[28];

        for (int i = 0; i < 28; i++)
        {
            m_vertVisits[i] = 0;
            m_adjMatrix[i] = new int[28];  // A single array instantiation

            for (int j = 0; j < 28; j++)
            {
                m_adjMatrix[i][j] = 0;
            }
        }
        */

        // Create the Node layout
        BuildGraph();
        // Assign sprites to each Node, set them all to unactivated
        int index = 0;

        for (int i = 0; i < 28; i++)
        {
            Nodes[i].m_startSprite = m_startSprite;
            Nodes[i].m_changeSprite = m_changeSprite;
            Nodes[i].m_iIndex = index;
            Nodes[i].m_nodeType = 0;
            index++;
        }

        for (int i = 28; i < 50; i++)
        {
            Nodes[i].m_startSprite = null;
            Nodes[i].m_changeSprite = null;
            Nodes[i].m_iIndex = index;
            Nodes[i].m_nodeType = 1;
            index++;
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

        // Connecting To Nodes to fall off the pyramid

        LinkNodes(0, 28, CONNECT_DIRECTION.TOP_L);
        LinkNodes(0, 29, CONNECT_DIRECTION.TOP_R);

        LinkNodes(1, 30, CONNECT_DIRECTION.TOP_L);
        LinkNodes(2, 31, CONNECT_DIRECTION.TOP_R);

        LinkNodes(3, 32, CONNECT_DIRECTION.TOP_L);
        LinkNodes(5, 33, CONNECT_DIRECTION.TOP_R);

        LinkNodes(6, 34, CONNECT_DIRECTION.TOP_L);
        LinkNodes(9, 35, CONNECT_DIRECTION.TOP_R);

        LinkNodes(10, 36, CONNECT_DIRECTION.TOP_L);
        LinkNodes(14, 37, CONNECT_DIRECTION.TOP_R);

        LinkNodes(15, 38, CONNECT_DIRECTION.TOP_L);
        LinkNodes(20, 39, CONNECT_DIRECTION.TOP_R);

        LinkNodes(21, 48, CONNECT_DIRECTION.TOP_L);
        LinkNodes(27, 49, CONNECT_DIRECTION.TOP_R);

        ///

        LinkNodes(21, 40, CONNECT_DIRECTION.BOT_L);
        LinkNodes(21, 41, CONNECT_DIRECTION.BOT_R);

        LinkNodes(22, 41, CONNECT_DIRECTION.BOT_L);
        LinkNodes(22, 42, CONNECT_DIRECTION.BOT_R);

        LinkNodes(23, 42, CONNECT_DIRECTION.BOT_L);
        LinkNodes(23, 43, CONNECT_DIRECTION.BOT_R);

        LinkNodes(24, 43, CONNECT_DIRECTION.BOT_L);
        LinkNodes(24, 44, CONNECT_DIRECTION.BOT_R);

        LinkNodes(25, 44, CONNECT_DIRECTION.BOT_L);
        LinkNodes(25, 45, CONNECT_DIRECTION.BOT_R);

        LinkNodes(26, 45, CONNECT_DIRECTION.BOT_L);
        LinkNodes(26, 46, CONNECT_DIRECTION.BOT_R);

        LinkNodes(27, 46, CONNECT_DIRECTION.BOT_L);
        LinkNodes(27, 47, CONNECT_DIRECTION.BOT_R);
    }

    

    void LinkNodes(int index1, int index2, CONNECT_DIRECTION dir)
    {
        // Attaching Connections
        // m_adjMatrix[index1][index2] = 1;
        // m_adjMatrix[index2][index1] = 1;

        switch (dir)
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

    /*
    int getNextUnvisitedVertex(int index)
    {
        // Traverse through all vertices in the graph (potentially)
        for (int i = 0; i < Nodes.Length; i++)
        {
            // Check if a connection between "index" and "i" exists
            // Check if the vertex at "i" has not already been checked
            if (m_adjMatrix[index][i] == 1 && m_vertVisits[i] == 0)
            {
                return i;
            }
        }

        // All adjacent vertices have been visited
        return -1;
    }

    // Search all adjacent vertices first before moving on
    // Queue data structure used
    public int BreadthFirstSearch(int startIndex, int endIndex)
    {
        // We are visiting our first vertex
        m_vertVisits[startIndex] = 1;

        Queue<int> searchQueue = new Queue<int>();
        int vert1 = 0, vert2 = 0;

        // Push our visited vertex to the queue

        searchQueue.Enqueue(startIndex);

        // Keep traversing until our searchQueue is empty OR we find our destination
        while (searchQueue.Count != 0)
        {
            // Save the front of the queue to vert1
            vert1 = searchQueue.Peek();
            searchQueue.Dequeue();

            // Have we reached our destination?
            if (vert1 == endIndex)
            {
                // Destination found!
                for (int i = 0; i < m_vertVisits.Length; i++)
                {
                    m_vertVisits[i] = 0;
                }

                while (searchQueue.Count != 0)
                {
                    vert1 = searchQueue.Dequeue();
                }

                return vert1;
            }

            // Get all adjacent vertices
            while ((vert2 = getNextUnvisitedVertex(vert1)) != -1)
            {
                // Mark vert2 as visited
                m_vertVisits[vert2] = 1;

                searchQueue.Enqueue(vert2);
            }
        }

        for (int i = 0; i < m_vertVisits.Length; i++)
        {
            m_vertVisits[i] = 0;
        }
        return -1;
    }
    */
}
