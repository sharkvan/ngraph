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
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEdgeFactory<T> where T : IComparable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEdge<T> CreateEdge();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        IEdge<T> CreateEdge(INode<T> source, INode<T> target);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        IEdge<T> CreateEdge(INode<T> source, INode<T> target, object data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="data"></param>
        /// <param name="cost"></param>
        /// <returns></returns>
        IEdge<T> CreateEdge(INode<T> source, INode<T> target, object data, double cost);
    }
}
