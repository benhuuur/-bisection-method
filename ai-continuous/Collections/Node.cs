using System.Collections.Generic;
using AulasAI.Collections;

public class Node<T> : INode<T>
{
    public T Value { get; set; }
    public List<Node<T>> Neighbors { get; set; }
    public int Connections => Neighbors.Count;

    public Node(T value, List<Node<T>> neighbors = null!)
    {
        this.Value = value;
        Neighbors = new List<Node<T>>();
        neighbors = neighbors ?? new List<Node<T>>();
        foreach (var neighbor in neighbors)
            this.AddNeighbor(neighbor);
    }

    public Node<T> AddNeighbor(Node<T> node)
    {
        node.Neighbors.Add(this);
        this.Neighbors.Add(node);
        return this;
    }

    public Node<T> RemoveNeighbor(Node<T> node)
    {
        node.Neighbors.Remove(this);
        this.Neighbors.Remove(node);
        return this;
    }
}
