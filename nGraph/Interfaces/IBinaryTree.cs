/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace nGraph.Interfaces
{
    public interface IBinaryTree<T> where T : IComparable<T>
    {
        void Add(T item);
        void Add(IBinaryTreeNode<T> node);
        IBinaryTreeNode<T> Minimum();
        IBinaryTreeNode<T> Maximum();
        IBinaryTreeNode<T> Search(T key);
        void InorderTraversal();
        void PreorderTraversal();
        void PostorderTraversal();
        void InorderTraversal(T root);
        void PreorderTraversal(T root);
        void PostorderTraversal(T root);
        IBinaryTreeNode<T> Root { get; }
        event EventHandler<BinaryTreeNodeEventArgs<T>> NodeVisited;
    }
}
