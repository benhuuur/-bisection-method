using System.Collections.Generic;
using System.Linq;

namespace AulasAI.Collections;

public class Graph<T, TNode> where TNode : INode<T>
{
    public List<TNode> Nodes { get; set; }

    public Graph(List<TNode> nodes = null!)
    {
        Nodes = nodes ?? new List<TNode>();
    }

    public Graph<T, TNode> AddNode(TNode node)
    {
        if (!Nodes.Any(anyNode => EqualityComparer<TNode>.Default.Equals(anyNode, node)))
            Nodes.Add(node);

        return this;
    }
}