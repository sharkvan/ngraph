/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;
using nGraph.Interfaces;
using nGraph.Collections;
using System.Collections.ObjectModel;

namespace nGraph
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="KEY"></typeparam>
    public class Node<KEY> : INode<KEY>, IComparable<INode<KEY>> where KEY : IComparable<KEY>
    {
        private KEY _key;
        private object _data;
        private List<INode<KEY>> _adjacencyList;
        private IDictionary<EdgeKey<KEY>, IEdge<KEY>> _edges;
        private IDictionary<object, object> _properties;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public Node(KEY key)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            _key = key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public Node(KEY key, object data)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (data == null)
                throw new ArgumentNullException("data");

            _key = key;
            _data = data;
        }

        /// <summary>
        /// Clones a Node by pointing to the same memebers
        /// </summary>
        /// <param name="node"></param>
        /// 
        public Node(INode<KEY> node)
        {
            if (node == null)
                return;

            _key = node.Key;
            _data = node.Data;
            _properties = node.Properties;

            Node<KEY> nodeT = node as Node<KEY>;
            if (nodeT != null)
            {
                _edges = nodeT._edges;
                _adjacencyList = nodeT._adjacencyList;
            }
            else
            {
                foreach (INode<KEY> n in node.AdjacentNodes)
                    AdjacencyList.Add(n);

                foreach (IEdge<KEY> edge in node.EdgeSet)
                    Edges.Add(new KeyValuePair<EdgeKey<KEY>, IEdge<KEY>>(new EdgeKey<KEY>(edge.SourceNode.Key, edge.TargetNode.Key), edge));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        public KEY Key
        {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ISet<INode<KEY>> AdjacentNodes
        {
            get
            {
                return new HashSet<INode<KEY>>(AdjacencyList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public INode<KEY> this[int index]
        {
            get { return AdjacencyList[index]; }
            set { AdjacencyList[index] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public INode<KEY> this[KEY key]
        {
            get
            {
                foreach (INode<KEY> n in AdjacencyList)
                {
                    if (n.Key.CompareTo(key) == 0)
                        return n;
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public IEdge<KEY> this[INode<KEY> v]
        {
            get
            {
                EdgeKey<KEY> key = new EdgeKey<KEY>(Key, v.Key);
                if (Edges.ContainsKey(key))
                    return Edges[key];
                else
                    return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ISet<IEdge<KEY>> EdgeSet
        {
            get { return new HashSet<IEdge<KEY>>(Edges.Values); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        public IEdge<KEY> AddAdjacentNode(INode<KEY> n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            IEdge<KEY> e = new Edge<KEY>(this, n);
            Edges.Add(new EdgeKey<KEY>(this.Key, n.Key), e);
            AdjacencyList.Add(n);
            return e;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public IEdge<KEY> AddAdjacentNode(INode<KEY> n, IEdge<KEY> e)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            if (e == null)
                throw new ArgumentNullException("e");

            e.SourceNode = this;
            e.TargetNode = n;
            Edges.Add(new EdgeKey<KEY>(this.Key, n.Key), e);
            AdjacencyList.Add(n);
            return e;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        public bool RemoveAdjacentNode(INode<KEY> n)
        {
            if (n == null)
                throw new ArgumentNullException("n");

            Edges.Remove(new EdgeKey<KEY>(this.Key, n.Key));
            return AdjacencyList.Remove(n);
        }

        /// <summary>
        /// 
        /// </summary>
        public int AdjacentNodeCount
        {
            get { return AdjacencyList.Count; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected Collection<INode<KEY>> AdjacencyList
        {
            get
            {
                if (_adjacencyList == null)
                    _adjacencyList = new List<INode<KEY>>();

                return new Collection<INode<KEY>>(_adjacencyList);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<object, object> Properties
        {
            get
            {
                if (_properties == null)
                    _properties = new Dictionary<object, object>();

                return _properties;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (Key == null)
                return 0;

            return Key.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (Key == null && obj == null)
                return false;

            return GetHashCode() == obj.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Key == null)
                return base.ToString();

            return Key.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(INode<KEY> other)
        {
            if (other == null)
                return 1;

            if (Key == null && other.Key == null)
                    return 0;

            if (Key != null && other.Key == null)
                    return 1;

            return GetHashCode().CompareTo(other.GetHashCode());
        }

        private IDictionary<EdgeKey<KEY>, IEdge<KEY>> Edges
        {
            get
            {
                if (_edges == null)
                    _edges = new Dictionary<EdgeKey<KEY>, IEdge<KEY>>();

                return _edges;
            }
        }

        #region INode<T> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool ContainsAdjacentNode(INode<KEY> node)
        {
            return AdjacencyList.Contains(node);
        }

        #endregion

        #region IEnumerable<INode<T>> Members

        public IEnumerator<INode<KEY>> GetEnumerator()
        {
            return AdjacencyList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class EdgeKey<T>
    {
        T _sourceKey;
        T _destKey;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <param name="destKey"></param>
        public EdgeKey(T sourceKey, T destKey)
        {
            if (sourceKey == null)
                throw new ArgumentNullException("sourceKey");

            if (destKey == null)
                throw new ArgumentNullException("destKey");
            
            _sourceKey = sourceKey;
            _destKey = destKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return _sourceKey.GetHashCode() * 7 * _destKey.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj != null)
                return GetHashCode() == obj.GetHashCode();

            return false;
        }
    }

}
