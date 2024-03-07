using System.Collections.Generic;

namespace AulasAI.Collections;

public abstract class SearchNode<T, TNode> where TNode : INode<T>
{
    public TNode Node { get; set; }
    public abstract List<SearchNode<T, TNode>> Neighbours();
    public bool Visited { get; set; } = false;
    public bool IsSolution { get; set; } = false;

    public SearchNode(TNode node) => Node = node;

    public void Reset()
    {
        Visited = false;
        IsSolution = false;
    }
}