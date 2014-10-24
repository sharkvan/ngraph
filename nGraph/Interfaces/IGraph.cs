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
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void EdgeDelegate<T>(object sender, EdgeEventArgs<T> args) where T : IComparable<T>;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void NodeDelegate<T>(object sender, NodeEventArgs<T> args) where T : IComparable<T>;

    /// <summary>
    /// 
    /// </summary>
    public enum EdgeDirection
    {
        Directed,
        Undirected
    }

    /// <summary>
    /// Interface for an Edge-Node Graph container.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGraph<T> : IEnumerable<T>, IEnumerable<IEdge<T>> where T : IComparable<T>
    {
        #region properties
        /// <summary>
        /// 
        /// </summary>
        int Size { get; }

        /// <summary>
        /// 
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 
        /// </summary>
        ISet<T> Vertices{ get; }

        /// <summary>
        /// 
        /// </summary>
        EdgeDirection EdgeDefault { get; }

        /// <summary>
        /// 
        /// </summary>
        ISet<IEdge<T>> Edges { get; }
        #endregion

        #region Indexers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        IEdge<T> this[T u, T v] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        ISet<IEdge<T>> this [INode<T> u] { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        ISet<IEdge<T>> this[T u] { get; }
        #endregion 

        #region methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        INode<T> GetNode(T u);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        object GetData(T u);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        IDictionary<object, object> GetProperties(T u);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        ISet<T> Adjacent(T u);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(T key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        bool RemoveNode(T u);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        bool Contains(INode<T> u);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        bool RemoveNode(INode<T> u);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        void AddNode(INode<T> node);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        INode<T> AddNode(T key, object data);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        INode<T> AddNode(T key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        IEdge<T> AddEdge(T u, T v);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        IEdge<T> AddEdge(T u, T v, decimal weight);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        IEdge<T> AddDirectedEdge(T u, T v);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        IEdge<T> AddDirectedEdge(T u, T v, decimal weight);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        IEdge<T> AddUndirectedEdge(T u, T v);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        IEdge<T> AddUndirectedEdge(T u, T v, decimal weight);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edgeDelegate"></param>
        void ForEachEdge(EdgeDelegate<T> edgeDelegate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeDelegate"></param>
        void ForEachNode(NodeDelegate<T> nodeDelegate);

        /// <summary>
        /// 
        /// </summary>
        IEdgeFactory<T> EdgeFactory { get; set; }
        #endregion
    }
}
