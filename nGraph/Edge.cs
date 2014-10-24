/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;
using nGraph.Interfaces;

namespace nGraph
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Edge<T> : IEdge<T>, IComparable<IEdge<T>> where T : IComparable<T>
    {
        private INode<T> _sourceNode;
        private INode<T> _targetNode;
        private decimal _cost;
        private object _data;
        private IDictionary<Object, Object> _properties;

        /// <summary>
        /// 
        /// </summary>
        public Edge()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public Edge(INode<T> source, INode<T> target)
        {
            _sourceNode = source;
            _targetNode = target;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="cost"></param>
        public Edge(INode<T> source, INode<T> target, decimal cost)
        {
            _sourceNode = source;
            _targetNode = target;
            _cost = cost;
        }

        /// <summary>
        /// Returns a shallow copy.
        /// </summary>
        /// <param name="edge">Edge to copy.</param>
        public Edge(IEdge<T> edge, bool includeCost)
        {
            if (edge == null)
                throw new ArgumentNullException("edge");

            _sourceNode = edge.SourceNode;
            _targetNode = edge.TargetNode;
            _properties = edge.Properties;
            _data = edge.Data;

            if(includeCost)
            {
                Edge<T> tEdge = edge as Edge<T>;
                if (tEdge != null)
                    _cost = tEdge._cost;
                else
                    _cost = edge.Cost;
            }
        }

        #region IEdge<T> Members

        /// <summary>
        /// 
        /// </summary>
        public INode<T> SourceNode
        {
            get { return _sourceNode; }
            set { _sourceNode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public INode<T> TargetNode
        {
            get { return _targetNode; }
            set { _targetNode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Decimal Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public IDictionary<Object, Object> Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new Dictionary<Object, Object>();
                }
                return _properties;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "(" + (SourceNode != null ? SourceNode.ToString() : "") + "," + (TargetNode != null ? TargetNode.ToString() : "") + ")";
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (TargetNode != null)
            {
                return TargetNode.GetHashCode() + (int)Cost;
            }
            else
            {
                return (int)Cost;
            }
        }

        #region IComparable<IEdge<T>> Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IEdge<T> other)
        {
            if (other != null)
            {
                if (TargetNode != null && other.TargetNode == null)
                {
                    return 1;
                }
                else if (TargetNode == null && other.TargetNode == null)
                {
                    return 0;
                }
                else
                {
                    return GetHashCode().CompareTo(other.GetHashCode());
                }
            }
            else
            {
                return 1;
            }
        }

        #endregion
    }
}
