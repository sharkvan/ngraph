/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using nGraph.Interfaces;

namespace nGraph
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NodeEventArgs<T> : EventArgs where T : IComparable<T>
    {
        private IGraph<T> _graph;
        private T _t;
        private int _level;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        public NodeEventArgs(IGraph<T> graph)
        {
            _graph = graph;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="t"></param>
        public NodeEventArgs(IGraph<T> graph, T t)
        {
            _graph = graph;
            _t = t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="t"></param>
        /// <param name="level"></param>
        public NodeEventArgs(IGraph<T> graph, T t, int level)
        {
            _graph = graph;
            _t = t;
            _level = level;
        }

        /// <summary>
        /// 
        /// </summary>
        public T Node
        {
            get { return _t; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IGraph<T> Graph
        {
            get { return _graph; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }
    }
}
