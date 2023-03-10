using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Graph
{
    public List<Node> Nodes;

    public Graph()
    {
        Nodes = new List<Node>();
    }

    public void AddNode(Node node)
    {
        Nodes.Add(node);
    }

    public void AddEdge(Node node1, Node node2, float cost)
    {
        Edge edge = new Edge(node1, node2, cost);
        node1.edges.Add(edge);
        node2.edges.Add(edge);
    }

    public void CreateGraphFromTilemap(Tilemap tilemap)
    {
        for (int x = tilemap.cellBounds.xMin; x < tilemap.cellBounds.xMax - 1; x++)
        {
            for (int y = tilemap.cellBounds.yMin; y < tilemap.cellBounds.yMax; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(pos);
                if (tile != null)
                {
                    Node node = new Node(x, y);
                    AddNode(node);
                    if (x > tilemap.cellBounds.xMin)
                    {
                        TileBase leftTile = tilemap.GetTile(new Vector3Int(x - 1, y, 0));
                        if (leftTile != null)
                        {
                            Node leftNode = Nodes.Find(n => n.x == x - 1 && n.y == y);
                            if (leftNode != null)
                            {
                                AddEdge(node, leftNode, 1);
                            }
                        }
                    }
                    if (y > tilemap.cellBounds.yMin)
                    {
                        TileBase bottomTile = tilemap.GetTile(new Vector3Int(x, y - 1, 0));
                        if (bottomTile != null)
                        {
                            Node bottomNode = Nodes.Find(n => n.x == x && n.y == y - 1);
                            if (bottomNode != null)
                            {
                                AddEdge(node, bottomNode, 1);
                            }
                        }
                    }
                }
            }
        }
    }
}

