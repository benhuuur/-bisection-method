using System.Collections.Generic;
using System.Linq;

namespace AulasAI.Collections;

public class SearchGraphNode<T> : SearchNode<T, GraphNode<T>>
{
    public SearchGraphNode(GraphNode<T> node) : base(node) { }

    public override List<SearchNode<T, GraphNode<T>>> Neighbours()
    {
        var neighbours = new List<SearchNode<T, GraphNode<T>>>(Node.Neighbours.Count);
        neighbours.AddRange(Node.Neighbours.Select(neighbour => new SearchGraphNode<T>(neighbour)));

        return neighbours;
    }
}