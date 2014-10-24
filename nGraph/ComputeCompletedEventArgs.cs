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
    public class ComputeCompletedEventArgs<T> : EventArgs where T : IComparable<T>
    {
        private IGraph<T> _computedGraph;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="computedGraph"></param>
        public ComputeCompletedEventArgs(IGraph<T> computedGraph)
        {
            _computedGraph = computedGraph;
        }

        /// <summary>
        /// 
        /// </summary>
        public IGraph<T> ComputedGraph
        {
            get { return _computedGraph; }
        }
    }
}
