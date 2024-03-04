using System.Collections.Generic;
using AulasAI.Collections;

public static partial class Search //Primeira pesquisa em profundidade
{
    public static bool BreadthFirstSearch<T>(TreeNode<T> node, T goal)
    {
        var Comparer = EqualityComparer<T>.Default;

        var queue = new Queue<TreeNode<T>>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            var currNode = queue.Dequeue();
            if (Comparer.Equals(currNode.Value, goal))
                return true;

            foreach (var currChild in currNode.Children)
                queue.Enqueue(currChild);
        }
        return false;
    }
}
