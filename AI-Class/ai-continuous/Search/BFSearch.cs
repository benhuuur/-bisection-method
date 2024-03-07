using System.Collections.Generic;
using AulasAI.Collections;

namespace AulasAI.Search;

public static partial class Search
{
    public static bool BFSearch<T, TNode>(SearchNode<T, TNode> node, T goal) where TNode : INode<T>
    {
        var queue = new Queue<SearchNode<T, TNode>>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();

            if (currNode.Visited)
                continue;

            currNode.Visited = true;

            if (EqualityComparer<T>.Default.Equals(currNode.Node.Value, goal))
            {
                currNode.IsSolution = true;
                return true;
            }

            foreach (var child in currNode.Neighbours())
                queue.Enqueue(child);
        }

        return false;
    }
}