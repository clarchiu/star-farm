using System;
using UnityEngine;

public class Node
{
    public GameObject debugSquare;
    public int G { get; set; }
    public int H { get; set; }
    public int F { get; set; }

    public Node Parent { get; set; }

    public Vector3Int Position { get; set; }

    //Debugging
    public Node(Vector3Int position, GameObject square)
    {
        this.Position = position;
        this.debugSquare = square;
    }

    public Node(Vector3Int position)
    {
        this.Position = position;
    }

    public void changeColor(Color color)
    {
        debugSquare.GetComponent<Renderer>().material.color = color;
    }

    public override string ToString()
    {
        return String.Format("G: {0}, H: {1}, F: {2}, Position: {3}", G, H, F, Position);
    }
}
