using System.Collections.Generic;

public class Graph<T> : Node<T>
{
    public Graph(T value, List<Node<T>> neighbors = null!)
        : base(value, neighbors) { }
}
