using System.Collections.Generic;
using AulasAI.Collections;

public static partial class Search //Primeira pesquisa em profundidade
{
    public static bool DepthFirstSearch<T>(TreeNode<T> node, T goal)
    {
        var Comparer = EqualityComparer<T>.Default;
        if (Comparer.Equals(node.Value, goal))
            return true;

        foreach (var currNode in node.Children)
        {
            if (DepthFirstSearch(currNode, goal))
                return true;
        }
        return false;
    }
}
