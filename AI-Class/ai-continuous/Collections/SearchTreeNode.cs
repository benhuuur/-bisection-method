using System.Collections.Generic;
using System.Linq;

namespace AulasAI.Collections;

public class SearchTreeNode<T> : SearchNode<T, TreeNode<T>>
{
    public SearchTreeNode(TreeNode<T> node) : base(node) { }

    public override List<SearchNode<T, TreeNode<T>>> Neighbours()
    {
        var neighbours = new List<SearchNode<T, TreeNode<T>>>(Node.Children.Count);
        neighbours.AddRange(Node.Children.Select(child => new SearchTreeNode<T>(child)));

        return neighbours;
    }
}