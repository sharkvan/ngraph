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
    public interface IEdge<T> : IWeighted, IComparable<IEdge<T>> where T : IComparable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        INode<T> SourceNode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        INode<T> TargetNode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDictionary<Object, Object> Properties { get; }
    }
}
