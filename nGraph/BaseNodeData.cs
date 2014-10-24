/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using nGraph.Interfaces;
using System.Drawing;

namespace nGraph
{
    public class BaseNodeData<T> : INodeData<T> where T : IComparable<T>
    {
        private T _predecessor;
        private Color _color;

        public BaseNodeData()
        {
        }

        public BaseNodeData(T predecessor)
        {
            _predecessor = predecessor;
        }

        public BaseNodeData(Color color)
        {
            _color = color;
        }

        public BaseNodeData(T predecessor, Color color)
        {
            _predecessor = predecessor;
            _color = color;
        }

        #region INodeData<T> Members

        public T Predecessor
        {
            get { return _predecessor; }
            set { _predecessor = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        #endregion
    }
}
