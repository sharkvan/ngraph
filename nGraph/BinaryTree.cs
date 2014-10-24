/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using nGraph.Interfaces;

namespace nGraph
{
    public class BinaryTree<T> : IBinaryTree<T> where T : IComparable<T>
    {
        private IBinaryTreeNode<T> _root;
        
        /// <summary>
        /// Notifies subscribers that a binary tree node has been visited.
        /// </summary>
        public event EventHandler<BinaryTreeNodeEventArgs<T>> NodeVisited;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BinaryTree()
        {
        }

        /// <summary>
        /// Raises the <c ref="NodeVisited">NodeVisited</c> event.
        /// </summary>
        /// <param name="node">Node being visited.</param>
        /// <param name="level">Level of the node in the tree.</param>
        protected virtual void OnNodeVisited(INode<T> node, int level)
        {
            if (NodeVisited != null)
            {
                NodeVisited(this, new BinaryTreeNodeEventArgs<T>(node, level));
            }
        }

        /// <summary>
        /// Returns the root node of the tree.
        /// </summary>
        public IBinaryTreeNode<T> Root
        {
            get { return _root; }
        }

        /// <summary>
        /// Add a node to the tree.
        /// </summary>
        /// <param name="z"></param>
        public void Add(T z)
        {
            if (z == null)
            {
                throw new ArgumentNullException("z");
            }

            IBinaryTreeNode<T> zNode = new BinaryTreeNode<T>(z);

            Add(zNode);
        }

        /// <summary>
        /// Add a node to the tree.
        /// </summary>
        /// <param name="zNode">Node to add.</param>
        public void Add(IBinaryTreeNode<T> zNode)
        {
            if (zNode == null)
            {
                throw new ArgumentNullException("zNode");
            }

            IBinaryTreeNode<T> y = null;
            IBinaryTreeNode<T> x = _root;

            while (x != null)
            {
                y = x;
                if (zNode.Key.CompareTo(x.Key) == -1)
                {
                    x = x.LeftNode;
                }
                else
                {
                    x = x.RightNode;
                }
            }

            if (y == null)
            {
                _root = zNode;
            }
            else if (zNode.Key.CompareTo(y.Key) == -1)
            {
                y.LeftNode = zNode;
            }
            else
            {
                y.RightNode = zNode;
            }
        }

        /// <summary>
        /// Find minimum node.
        /// </summary>
        /// <returns></returns>
        public IBinaryTreeNode<T> Minimum()
        {
            return _Minimum(_root);
        }

        /// <summary>
        /// Find maximum node.
        /// </summary>
        /// <returns></returns>
        public IBinaryTreeNode<T> Maximum()
        {
            return _Maximum(_root);
        }

        /// <summary>
        /// Search for node with containing key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IBinaryTreeNode<T> Search(T key)
        {
            return _Search(_root, key);
        }

        /// <summary>
        /// Perform Inorder traversal, raising <c ref="NodeVisited">NodeVisited</c> event.
        /// </summary>
        public void InorderTraversal()
        {
            int level = 0;
            _InorderTraversal(_root, level);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PreorderTraversal()
        {
            int level = 0;
            _PreorderTraversal(_root, level);
        }

        /// <summary>
        /// 
        /// </summary>
        public void PostorderTraversal()
        {
            int level = 0;
            _PostorderTraversal(_root, level);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public void InorderTraversal(T root)
        {
            int level = 0;
            IBinaryTreeNode<T> r = Search(root);
            if (r != null)
            {
                _InorderTraversal(r, level);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public void PreorderTraversal(T root)
        {
            int level = 0;
            IBinaryTreeNode<T> r = Search(root);
            if (r != null)
            {
                _PreorderTraversal(r, level);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public void PostorderTraversal(T root)
        {
            int level = 0;
            IBinaryTreeNode<T> r = Search(root);
            if (r != null)
            {
                _PostorderTraversal(r, level);
            }
        }

        /// <summary>
        /// Perform an InorderTraversal, notifying registered visitors.  Also record current tree level.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="level"></param>
        private void _InorderTraversal(IBinaryTreeNode<T> node, int level)
        {
            if (node != null)
            {
                _InorderTraversal(node.LeftNode, level);
                OnNodeVisited(node, level);
                _InorderTraversal(node.RightNode, ++level);
            }
            level--;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="level"></param>
        private void _PreorderTraversal(IBinaryTreeNode<T> node, int level)
        {
            if (node != null)
            {
                OnNodeVisited(node, level);
                _InorderTraversal(node.LeftNode, level);
                _InorderTraversal(node.RightNode, ++level);
            }
            level--;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"></param>
        private void _PostorderTraversal(IBinaryTreeNode<T> node, int level)
        {
            if (node != null)
            {
                _InorderTraversal(node.LeftNode, level);
                _InorderTraversal(node.RightNode, ++level);
                OnNodeVisited(node, level);
            }
            level--;
        }

        /// <summary>
        /// Peform an iterative binary tree search on key, returning null or the IBinaryTreeNode that matches.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private IBinaryTreeNode<T> _Search(IBinaryTreeNode<T> node, T key)
        {
            if (node != null && node.Key.CompareTo(key) != 0)
            {
                if (node.Key.CompareTo(key) == -1)
                {
                    node = node.LeftNode;
                }
                else
                {
                    node = node.RightNode;
                }
            }
            return node;
        }

        /// <summary>
        /// Walks down left side of tree to find the minimum node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private IBinaryTreeNode<T> _Minimum(IBinaryTreeNode<T> node)
        {
            if (node != null)
            {
                while (node.LeftNode != null)
                {
                    node = node.LeftNode;
                }
            }
            return node;
        }

        /// <summary>
        /// Walks down the right side of tree to find the maximum node.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private IBinaryTreeNode<T> _Maximum(IBinaryTreeNode<T> node)
        {
            if (node != null)
            {
                while (node.RightNode != null)
                {
                    node = node.RightNode;
                }
            }
            return node;
        }
    }
}
