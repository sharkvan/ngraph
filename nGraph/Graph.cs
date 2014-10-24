/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;
using nGraph.Interfaces;
using System.Threading;
using nGraph.Collections;
using System.Linq;

namespace nGraph
{
    /// <summary>
    /// 
    /// </summary>
    public class GraphEventArgs : EventArgs
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void GraphEventHandler(object sender, GraphEventArgs args);

    /// <summary>
    /// Defines a graph container that allows vertices of any type T and edges based on IEdge 
    /// that contain a source and destination vertex of the same type T to be contained within
    /// it, as defined in the standard mathematical definition of an edge-node graph.  This
    /// Graph representation allows for the definition of both directed and undirected graphs
    /// at construction time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Graph<T> : IGraph<T> where T : IComparable<T>
    {
        private int _order;
        private int _size;
        private IDictionary<T, INode<T>> _nodes;
        private IEdgeFactory<T> _edgeFactory;
        private EdgeDirection _edgeDefault = EdgeDirection.Undirected;

        /// <summary>
        /// 
        /// </summary>
        public Graph()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edgeDefault"></param>
        public Graph(EdgeDirection edgeDefault)
        {
            _edgeDefault = edgeDefault;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edgeFactory"></param>
        public Graph(IEdgeFactory<T> edgeFactory)
        {
            _edgeFactory = edgeFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edgeDefault"></param>
        /// <param name="edgeFactory"></param>
        public Graph(EdgeDirection edgeDefault, IEdgeFactory<T> edgeFactory)
        {
            _edgeDefault = edgeDefault;
            _edgeFactory = edgeFactory;
        }

        /// <summary>
        /// The size of a graph is the number of edges contained in the graph.
        /// </summary>
        public int Size
        {
            get
            {
                int size = 0;
                foreach (INode<T> node in Nodes.Values)
                {
                    size += node.AdjacentNodeCount;
                }
                _size = size;
                return _size;
            }
        }

        /// <summary>
        /// The order of a graph is the number of vertices contained within a graph.
        /// </summary>
        public int Order
        {
            get
            {
                _order = Nodes.Count;
                return _order;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public EdgeDirection EdgeDefault
        {
            get { return _edgeDefault; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public IEdge<T> this[T u, T v]
        {
            get
            {
                if (Nodes.ContainsKey(u) && Nodes.ContainsKey(v))
                {
                    INode<T> uNode = GetNode(u);
                    INode<T> vNode = GetNode(v);
                    IEdge<T> e = uNode[vNode];
                    return e;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Return the set of edges given a vertex.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public ISet<IEdge<T>> this[INode<T> u]
        {
            get
            {
                HashSet<IEdge<T>> edgeSet = new HashSet<IEdge<T>>();
                if (Nodes.ContainsKey(u.Key))
                {
                    INode<T> node = Nodes[u.Key];
                    node
                        .EdgeSet
                        .AsParallel<IEdge<T>>()
                        .ForAll<IEdge<T>>(
                            i => edgeSet.Add(i));
                }
                return edgeSet;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public ISet<IEdge<T>> this[T u]
        {
            get
            {
                return this[new Node<T>(u)];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public INode<T> GetNode(T u)
        {
            if (u == null)
            {
                throw new ArgumentNullException("u");
            }
            return Nodes[u];
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            Nodes.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool Contains(INode<T> u)
        {
            return Nodes.ContainsKey(u.Key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(T key)
        {
            return Nodes.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool RemoveNode(T u)
        {
            if (u == null)
            {
                throw new ArgumentNullException("u");
            }

            return RemoveNode(new Node<T>(u));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public bool RemoveNode(INode<T> u)
        {
            if (u == null)
                throw new ArgumentNullException("u");

            if (!Nodes.ContainsKey(u.Key))
            {
                ISet<INode<T>> adj = u.AdjacentNodes;
                adj.AsParallel().ForAll(i => i.RemoveAdjacentNode(u));

                return Nodes.Remove(u.Key);
            }

            throw new ArgumentException("No such node: " + u);
        }

        /// <summary>
        /// Returns a copy as a Set of IEdge objects of the edges within this graph.
        /// </summary>
        public ISet<IEdge<T>> Edges
        {
            get
            {
                HashSet<IEdge<T>> edgeSet = new HashSet<IEdge<T>>();
                foreach (INode<T> n in Nodes.Values)
                {
                    n.EdgeSet
                        .AsParallel<IEdge<T>>()
                        .ForAll<IEdge<T>>(
                            i => edgeSet.Add(i)
                        );
                }
                return edgeSet;
            }
        }

        /// <summary>
        /// Return the set of vertices v adjacent to u.
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public ISet<T> Adjacent(T u)
        {
            if (u == null)
                throw new ArgumentNullException("u");

            if (this.Contains(u))
            {
                INode<T> uNode = Nodes[u];
                HashSet<T> adjacentSet = new HashSet<T>();

                uNode.AdjacentNodes.AsParallel().ForAll(i => adjacentSet.Add(i.Key));

                return adjacentSet;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(INode<T> node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (Contains(node))
                return;

            Nodes.Add(node.Key, node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public INode<T> AddNode(T key, object data)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (Contains(key))
                return null;

            INode<T> node = new Node<T>(key, data);
            AddNode(node);
            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public INode<T> AddNode(T key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (Contains(key))
                return null;

            INode<T> node = new Node<T>(key);
            AddNode(node);
            return node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public IEdge<T> AddEdge(T u, T v)
        {
            if (EdgeDefault == EdgeDirection.Undirected)
                return AddUndirectedEdge(u, v);
            else
                return AddDirectedEdge(u, v);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        public IEdge<T> AddEdge(T u, T v, decimal weight)
        {
            if (EdgeDefault == EdgeDirection.Undirected)
                return AddUndirectedEdge(u, v, weight);
            else
                return AddDirectedEdge(u, v, weight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        public IEdge<T> AddDirectedEdge(T u, T v)
        {
            return AddDirectedEdge(u, v, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="weight"></param>
        public IEdge<T> AddDirectedEdge(T u, T v, decimal weight)
        {
            if (u == null)
                throw new ArgumentNullException("u");

            if (v == null)
                throw new ArgumentNullException("v");

            INode<T> uNode = null;
            INode<T> vNode = null;

            if (Contains(u))
                uNode = Nodes[u];
            else
                uNode = new Node<T>(u);

            if (Contains(v))
                vNode = Nodes[v];
            else
                vNode = new Node<T>(v);

            return AddDirectedEdge(uNode, vNode, weight);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        public IEdge<T> AddUndirectedEdge(T u, T v)
        {
            return AddUndirectedEdge(u, v, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="weight"></param>
        public IEdge<T> AddUndirectedEdge(T u, T v, decimal weight)
        {
            if (u == null)
                throw new ArgumentNullException("u");

            if (v == null)
                throw new ArgumentNullException("v");

            INode<T> uNode = null;
            INode<T> vNode = null;

            if (Contains(u))
                uNode = Nodes[u];
            else
                uNode = new Node<T>(u);

            if (Contains(v))
                vNode = Nodes[v];
            else
                vNode = new Node<T>(v);

            return AddUndirectedEdge(uNode, vNode, weight);
        }

        #region IEnumerable<INode<T>> Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            ICollection<INode<T>> values = Nodes.Values;
            foreach (INode<T> node in values)
            {
                yield return node.Key;
            }
        }

        #endregion

        #region IEnumerable Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }

        #endregion

        #region IEnumerable<IEdge<T>> Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator<IEdge<T>> IEnumerable<IEdge<T>>.GetEnumerator()
        {
            ISet<IEdge<T>> edges = Edges;
            foreach (IEdge<T> edge in edges)
                yield return edge;
        }

        #endregion

        #region IGraph<T> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edgeDelegate"></param>
        public void ForEachEdge(EdgeDelegate<T> edgeDelegate)
        {
            if (edgeDelegate != null)
            {
                foreach (IEdge<T> edge in (IEnumerable<IEdge<T>>)this)
                    edgeDelegate(this, new EdgeEventArgs<T>(edge));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeDelegate"></param>
        public void ForEachNode(NodeDelegate<T> nodeDelegate)
        {
            if (nodeDelegate != null)
            {
                foreach (T u in this)
                    nodeDelegate(this, new NodeEventArgs<T>(this, u));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEdgeFactory<T> EdgeFactory
        {
            get { return _edgeFactory; }
            set { _edgeFactory = value; }
        }

        #endregion

        #region IGraph<T> Members

        /// <summary>
        /// 
        /// </summary>
        public ISet<T> Vertices
        {
            get { return new HashSet<T>(Nodes.Keys); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public object GetData(T u)
        {
            INode<T> node = GetNode(u);
            if (node == null)
                return null;

            return node.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public IDictionary<object, object> GetProperties(T u)
        {
            INode<T> node = GetNode(u);
            if (node == null)
                return null;

            return node.Properties;
        }

        #endregion


        #region Private Members

        /// <summary>
        /// This graph implements an adjacency list and not an adjacency matrix.  This is a good, 
        /// general purpose data structure for sparse graphs.
        /// </summary>
        private IDictionary<T, INode<T>> Nodes
        {
            get
            {
                if (_nodes == null)
                    _nodes = new Dictionary<T, INode<T>>();

                return _nodes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        private IEdge<T> AddUndirectedEdge(INode<T> u, INode<T> v)
        {
            if (u == null)
                throw new ArgumentNullException("u");

            if (v == null)
                throw new ArgumentNullException("v");
            
            return AddUndirectedEdge(u, v, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="weight"></param>
        private IEdge<T> AddUndirectedEdge(INode<T> u, INode<T> v, decimal weight)
        {
            if (u == null)
                throw new ArgumentNullException("u");

            if (v == null)
                throw new ArgumentNullException("v");

            INode<T> uNode = null;
            INode<T> vNode = null;

            if (Contains(u))
            {
                uNode = Nodes[u.Key];
            }
            else
            {
                AddNode(u);
                uNode = u;
            }

            if (Contains(v))
            {
                vNode = Nodes[v.Key];
            }
            else
            {
                AddNode(v);
                vNode = v;
            }

            IEdge<T> retEdge = null;

            if (EdgeFactory == null)
            {
                retEdge = uNode.AddAdjacentNode(vNode);
                vNode.AddAdjacentNode(uNode);
            }
            else
            {
                retEdge = uNode.AddAdjacentNode(vNode, EdgeFactory.CreateEdge());
                vNode.AddAdjacentNode(uNode, EdgeFactory.CreateEdge());
            }

            uNode[vNode].Cost = weight;
            vNode[uNode].Cost = weight;
            return retEdge;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        private IEdge<T> AddDirectedEdge(INode<T> u, INode<T> v)
        {
            if (u == null)
                throw new ArgumentNullException("u");

            if (v == null)
                throw new ArgumentNullException("v");

            return AddDirectedEdge(u, v, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="weight"></param>
        private IEdge<T> AddDirectedEdge(INode<T> u, INode<T> v, decimal weight)
        {
            if (u == null)
                throw new ArgumentNullException("u");

            if (v == null)
                throw new ArgumentNullException("v");

            INode<T> uNode = null;
            INode<T> vNode = null;

            if (Contains(u))
                uNode = Nodes[u.Key];
            else
            {
                this.AddNode(u);
                uNode = u;
            }

            if (Contains(v))
                vNode = Nodes[v.Key];
            else
            {
                this.AddNode(v);
                vNode = v;
            }

            IEdge<T> retEdge = null;
            if (EdgeFactory == null)
                retEdge = uNode.AddAdjacentNode(vNode);
            else
                retEdge = uNode.AddAdjacentNode(vNode, EdgeFactory.CreateEdge());

            uNode[vNode].Cost = weight;
            return retEdge;
        }

        #endregion

    }
}
