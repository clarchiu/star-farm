using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStar
{
    private HashSet<Node> openList, closedList;
    private Stack<Vector3Int> path;

    //how to populate this?
    private Dictionary<Vector3Int, Node> allNodes = new Dictionary<Vector3Int, Node>();
    //how to connect this with the map?
    private Tilemap tilemap;

    private Node currentNode;
    private Vector3Int startPos, goalPos;

    private void Initialize()
    {
        currentNode = GetNode(startPos);
        openList = new HashSet<Node>();
        closedList = new HashSet<Node>();
        openList.Add(currentNode);
    }

    private List<Node> FindNeighbors(Vector3Int parentPosition)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector3Int neighborPos = new Vector3Int(parentPosition.x - x, parentPosition.y - y, parentPosition.z);
                if (y != 0 || x != 0)
                {
                    //need to add a check to not add unwalkable tiles to neighbors list
                    if (neighborPos != startPos && tilemap.GetTile(neighborPos))
                    {
                        Node neighbor = GetNode(neighborPos);
                        neighbors.Add(neighbor);
                    }                   
                }
            }
        }

        return neighbors;
    }

    private void ExamineNeighbors(List<Node> neighbors, Node current)
    {
        for (int i = 0; i < neighbors.Count; i++)
        {
            Node neighbor = neighbors[i];

            if (!IsConnectedDiagonally(current, neighbor))
            {
                continue;
            }

            int gScore = CalcNeighborGScore(neighbor.Position, current.Position);

            if (openList.Contains(neighbor))
            {
                if (current.G + gScore < neighbor.G)
                {
                    CalcValues(current, neighbor, gScore);
                }
            }
            else if (!closedList.Contains(neighbor))
            {
                CalcValues(current, neighbor, gScore);
                openList.Add(neighbor);
            }
        }
    }

    private void CalcValues(Node parent, Node neighbor, int cost)
    {
        neighbor.Parent = parent;

        neighbor.G = parent.G + cost;
        neighbor.H = (Math.Abs(neighbor.Position.x - goalPos.x) + Math.Abs(neighbor.Position.y - goalPos.y)) + 10;
        neighbor.F = neighbor.G + neighbor.H;
    }

    private int CalcNeighborGScore(Vector3Int neighbor, Vector3Int current)
    {
        int gScore;

        int x = neighbor.x - current.x;
        int y = neighbor.y - current.y;

        if (Math.Abs(x - y) % 2 == 1)
        {
            gScore = 10;
        }
        else
        {
            gScore = 14;
        }

        return gScore;
    }

    private void UpdateCurrentNode(ref Node current)
    {
        openList.Remove(current);
        closedList.Add(current);

        if (openList.Count > 0)
        {
            current = openList.OrderBy(x => x.F).First();
        }
    }

    private bool IsConnectedDiagonally(Node current, Node neighbor)
    {
        Vector3Int direction = current.Position - neighbor.Position;
        Vector3Int first = new Vector3Int(current.Position.x + (direction.x * -1), current.Position.y, current.Position.z);
        Vector3Int second = new Vector3Int(current.Position.x, current.Position.y + (direction.x * -1), current.Position.z);

        //if first or second is unwalkable, that means the path is diagonally connected
        return false;
    }

    private Node GetNode(Vector3Int position)
    {
        if (allNodes.ContainsKey(position))
        {
            return allNodes[position];
        }
        else
        {
            Node node = new Node(position);
            allNodes.Add(position, node);
            return node;
        }

    }

    private Stack<Vector3Int> GeneratePath(Node current)
    {
        if (current.Position == goalPos)
        {
            Stack<Vector3Int> finalPath = new Stack<Vector3Int>();

            while (current.Position != startPos)
            {
                finalPath.Push(current.Position);
                current = current.Parent;
            }

            return finalPath;
        }

        return null;
    }

    public void Algorithm()
    {
        if (currentNode == null)
        {
            Initialize();
        }

        while (openList.Count > 0 && path == null)
        {
            List<Node> neighbors = FindNeighbors(currentNode.Position);
            ExamineNeighbors(neighbors, currentNode);
            UpdateCurrentNode(ref currentNode);

            path = GeneratePath(currentNode);
        }
     }
}
