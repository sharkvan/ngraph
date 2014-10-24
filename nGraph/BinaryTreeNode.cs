/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using nGraph.Interfaces;

namespace nGraph
{
    public class BinaryTreeNode<T> : Node<T>, IBinaryTreeNode<T> where T : IComparable<T>
    {
        #region IBinaryTreeNode Members
        private IBinaryTreeNode<T> _left;
        private IBinaryTreeNode<T> _right;

        public BinaryTreeNode(T key)
            : base(key)
        {
        }

        public BinaryTreeNode(T key, object data)
            : base(key, data)
        {
        }

        public IBinaryTreeNode<T> LeftNode
        {
            get 
            {
                return _left;
            }
            set 
            {
                if (_left != null && AdjacentNodes.Contains(_left))
                {
                    RemoveAdjacentNode(_left);
                }
                _left = value;
                if (_left != null && !AdjacentNodes.Contains(_left))
                {
                    AdjacentNodes.Add(_left);
                }
            }
        }

        public IBinaryTreeNode<T> RightNode
        {
            get 
            {
                return _right;
            }
            set 
            {
                if (_right != null && AdjacentNodes.Contains(_left))
                {
                    RemoveAdjacentNode(_left);
                }
                _right = value;
                if (_right != null && !AdjacentNodes.Contains(_right))
                {
                    AdjacentNodes.Add(_right);
                }
            }
        }

        #endregion
    }
}
