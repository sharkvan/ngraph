/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using nGraph.Interfaces;

namespace nGraph
{
    public class BinaryTreeNodeEventArgs<T> : EventArgs where T : IComparable<T>
    {
        private INode<T> _node;
        private int _level;

        public BinaryTreeNodeEventArgs()
        {
        }

        public BinaryTreeNodeEventArgs(INode<T> node)
        {
            _node = node;
        }

        public BinaryTreeNodeEventArgs(INode<T> node, int level)
        {
            _node = node;
            _level = level;
        }


        public INode<T> Node
        {
            get { return _node; }
        }

        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
    }
}
