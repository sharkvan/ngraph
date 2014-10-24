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
    public class EdgeEventArgs<T> : EventArgs where T : IComparable<T>
    {
        private IEdge<T> _edge;

        /// <summary>
        /// 
        /// </summary>
        public EdgeEventArgs()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        public EdgeEventArgs(IEdge<T> edge)
        {
            _edge = edge;
        }

        /// <summary>
        /// 
        /// </summary>
        public IEdge<T> Edge
        {
            get { return _edge; }
        }
    }
}
