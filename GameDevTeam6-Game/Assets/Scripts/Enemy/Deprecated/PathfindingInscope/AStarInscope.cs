using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarInscope: MonoBehaviour
{
    //for debugging
    public GameObject astarsquare;

    private HashSet<Node> openList, closedList;
    private Dictionary<Vector3Int, Node> allNodes = new Dictionary<Vector3Int, Node>();

    [SerializeField]
    private GameObject objectTileController;

    private TileLayout tilemap;

    private Node currentNode;
    private Vector3Int start, goal;

    private int nodesExamined;

    private void Awake()
    {
        tilemap = objectTileController.GetComponent<TileLayout>();
    }

    public Stack<Vector3> FindPath(Vector3 start, Vector3 goal)
    {
        this.start = Vector3Int.FloorToInt(start);
        this.goal = Vector3Int.FloorToInt(goal);

        List<Vector3Int> path = FindReversePath(this.start, this.goal);
        if (path == null)
        {
            return null;
        } else
        {
            Stack<Vector3> vectorPath = new Stack<Vector3>();
            foreach (Vector3 node in path)
            {
                Vector3 vectorNode = new Vector3(node.x, node.y);
                vectorPath.Push(vectorNode);
            }

            print(nodesExamined);
            return vectorPath;
        }
    }

    /*
     * This finds the path but in reverse
     * returns a list of the reverse path
     * It is in reverse so that it can easily be pushed into a stack
     */
    public List<Vector3Int> FindReversePath(Vector3Int start, Vector3Int goal)
    {
        if (currentNode == null)
        {
            Instantiate(start, goal);
        }

        List<Vector3Int> path = null;

        while (openList.Count > 0 && path == null)
        {
            List<Node> neighbors = FindNeighbors(currentNode.Position);

            ExamineNeighbors(neighbors, currentNode);
            UpdateCurrentNode(ref currentNode);
            path = GeneratePath(currentNode);
        }

        return path;
    }

    private void Instantiate(Vector3Int start, Vector3Int goal)
    {
        nodesExamined = 0;
        this.start = start;
        this.goal = goal;
        currentNode = GetNode(this.start);
        openList = new HashSet<Node>();
        closedList = new HashSet<Node>();
        openList.Add(currentNode);

        //Debugging
        //currentNode.changeColor(Color.green);
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
                    ObjectTile neighborTile = tilemap.GetTile(neighborPos.x, neighborPos.y);

                    if (neighborPos != start && neighborTile != null && neighborTile.Walkable == true)
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
            nodesExamined++;

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
                    CalcScores(current, neighbor, gScore);
                }
            }
            else if (!closedList.Contains(neighbor))
            {
                CalcScores(current, neighbor, gScore);
                openList.Add(neighbor);

                //Debugging
                //neighbor.changeColor(Color.blue);
            }
        }
    }

    //getting the scores of a neighbor relative to start
    private void CalcScores(Node parent, Node neighbor, int cost)
    {
        neighbor.Parent = parent;

        neighbor.G = parent.G + cost;
        neighbor.H = (Math.Abs(neighbor.Position.x - goal.x) + Math.Abs(neighbor.Position.y - goal.y)) * 10;
        neighbor.F = neighbor.G + neighbor.H;
    }

    //getting the g score of an immediate neighbor
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

        //Debugging
        //current.changeColor(Color.red);

        if (openList.Count > 0)
        {
            current = openList.OrderBy(x => x.F).First();

            //Debugging
            //current.changeColor(Color.cyan);
        }

    }

    //connected diagonally is defined as having the nodes that are between
    //current and neighbor to be both walkable
    //other current and neighbor are at a corner and the enemy should not cut that corner
    private bool IsConnectedDiagonally(Node current, Node neighbor)
    {
        Vector3Int direction = current.Position - neighbor.Position;

        if (Math.Abs(direction.x) == 1 && Math.Abs(direction.y) == 1) //this means neighbor is a diagonal
        {
            Vector3Int first = new Vector3Int(current.Position.x + (direction.x * -1), current.Position.y, current.Position.z);
            Vector3Int second = new Vector3Int(current.Position.x, current.Position.y + (direction.y * -1), current.Position.z);

            ObjectTile firstTile = tilemap.GetTile(first.x, first.y);
            ObjectTile secondTile = tilemap.GetTile(second.x, second.y);

            //check if tiles are not null and are walkable
            bool isFirstWalkable = firstTile != null && firstTile.Walkable ? true : false;
            bool isSecondWalkable = secondTile != null && secondTile.Walkable ? true : false;

            if (isFirstWalkable && isSecondWalkable)
            {
                //Debug.LogFormat("{0}, {1} are connected diagonally", current.Position, neighbor.Position);
                return true;
            }
            else
            {
                //Debug.LogFormat("{0}, {1} are not connected diagonally", current.Position, neighbor.Position);
                return false;
            }
        } else
        {
            return true;
        }

    }

    private Node GetNode(Vector3Int position)
    {
        if (allNodes.ContainsKey(position))
        {
            return allNodes[position];
        }
        else
        {
            //Debugging
            //GameObject debugSquare = Instantiate(astarsquare, position, Quaternion.identity);
            //debugSquare.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0f);
            //Node node = new Node(position, debugSquare);

            Node node = new Node(position);
            allNodes.Add(position, node);
            return node;
        }
    }

    private List<Vector3Int> GeneratePath(Node current)
    {
        if (current.Position == goal)
        {
            List<Vector3Int> finalPath = new List<Vector3Int>();

            while (current.Position != start)
            {
                //Debugging
                //current.changeColor(Color.cyan);

                finalPath.Add(current.Position);
                current = current.Parent;
            }
            return finalPath;
        }

        return null;
    }
}
