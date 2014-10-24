/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;
using nGraph.Collections;

namespace nGraph.Interfaces
{
    /// <summary>
    /// Represents a graph node.
    /// </summary>
    /// <typeparam name="T">A type that implements generic IComparable.</typeparam>
    public interface INode<T> : IEnumerable<INode<T>>, IComparable<INode<T>> where T : IComparable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        T Key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        INode<T> this[int index] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        IEdge<T> this[INode<T> v] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        INode<T> this[T key] { get; }

        /// <summary>
        /// 
        /// </summary>
        ISet<INode<T>> AdjacentNodes { get; }

        /// <summary>
        /// 
        /// </summary>
        ISet<IEdge<T>> EdgeSet { get; }

        /// <summary>
        /// 
        /// </summary>
        IDictionary<Object,Object> Properties { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        bool ContainsAdjacentNode(INode<T> node);

        /// <summary>
        /// 
        /// </summary>
        int AdjacentNodeCount { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        IEdge<T> AddAdjacentNode(INode<T> n);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        IEdge<T> AddAdjacentNode(INode<T> n, IEdge<T> e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        bool RemoveAdjacentNode(INode<T> n);
    }
}
