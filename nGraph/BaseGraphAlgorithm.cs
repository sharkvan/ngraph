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

namespace nGraph
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    abstract public class BaseGraphAlgorithm<T> : IGraphAlgorithm<T> where T : IComparable<T>
    {
        private IGraph<T> _graph;
        
        /// <summary>
        /// Notifies subscribers that a node was entered. <see cref="INode"/>
        /// </summary>
        public event EventHandler<NodeEventArgs<T>> NodeEntered;

        /// <summary>
        /// Notifies subscribers that a node was discovered. <see cref="INode"/>
        /// </summary>
        public event EventHandler<NodeEventArgs<T>> NodeDiscovered;

        /// <summary>
        /// Notifies subscribers that a node was exited. <see cref="INode"/>
        /// </summary>
        public event EventHandler<NodeEventArgs<T>> NodeExited;

        /// <summary>
        /// Notifies subscribers that an edge was traversed.
        /// </summary>
        public event EventHandler<EdgeEventArgs<T>> EdgeDiscovered;

        /// <summary>
        /// Notifies subscribers that an edge was traversed.
        /// </summary>
        public event EventHandler<EdgeEventArgs<T>> EdgeTraversed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<ComputeCompletedEventArgs<T>> ComputeCompleted;

        /// <summary>
        /// Sets the graph field.
        /// </summary>
        /// <param name="graph">Graph to store.</param>
        protected BaseGraphAlgorithm(IGraph<T> graph)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("graph");
            }
            _graph = graph;
        }

        /// <summary>
        /// Raises the <c ref="NodeEntered">NodeEntered</c> event.
        /// </summary>
        /// <param name="node">Graph node.</param>
        protected void OnNodeEntered(T u)
        {
            EventHandler<NodeEventArgs<T>> temp = NodeEntered;
            if (temp != null)
            {
                temp(this, new NodeEventArgs<T>(Graph, u));
            }
        }

        /// <summary>
        /// Raises the <c ref="NodeEntered">NodeEntered</c> event.
        /// </summary>
        /// <param name="node">Graph node.</param>
        /// <param name="level">Level of the node.</param>
        protected void OnNodeEntered(T u, int level)
        {
            EventHandler<NodeEventArgs<T>> temp = NodeEntered;
            if (temp != null)
            {
                temp(this, new NodeEventArgs<T>(Graph, u, level));
            }
        }


        /// <summary>
        /// Raises the <c ref="NodeDiscovered">NodeDiscovered</c> event.
        /// </summary>
        /// <param name="node">Graph node.</param>
        protected void OnNodeDiscovered(T u)
        {
            EventHandler<NodeEventArgs<T>> temp = NodeDiscovered;
            if (temp != null)
            {
                temp(this, new NodeEventArgs<T>(Graph, u));
            }
        }

        /// <summary>
        /// Raises the <c ref="NodeDiscovered">NodeDiscovered</c> event.
        /// </summary>
        /// <param name="node">Graph node.</param>
        /// <param name="level">Level of the node.</param>
        protected void OnNodeDiscovered(T u, int level)
        {
            EventHandler<NodeEventArgs<T>> temp = NodeDiscovered;
            if (temp != null)
            {
                temp(this, new NodeEventArgs<T>(Graph, u, level));
            }
        }

        /// <summary>
        /// Raises the <c ref="NodeExited">NodeExited</c> event.
        /// </summary>
        /// <param name="node">Graph node.</param>
        protected void OnNodeExited(T u)
        {
            EventHandler<NodeEventArgs<T>> temp = NodeExited;
            if (temp != null)
            {
                temp(this, new NodeEventArgs<T>(Graph, u));
            }
        }

        /// <summary>
        /// Raises the <c ref="NodeExited">NodeExited</c> event.
        /// </summary>
        /// <param name="node">Graph node.</param>
        /// <param name="level">Level of the node.</param>
        protected void OnNodeExited(T u, int level)
        {
            EventHandler<NodeEventArgs<T>> temp = NodeExited;
            if (temp != null)
            {
                temp(this, new NodeEventArgs<T>(Graph, u, level));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        protected void OnEdgeDiscovered(IEdge<T> edge)
        {
            EventHandler <EdgeEventArgs<T>> temp = EdgeDiscovered;
            if (temp != null)
            {
                temp(this, new EdgeEventArgs<T>(edge));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edge"></param>
        protected void OnEdgeTraversed(IEdge<T> edge)
        {
            EventHandler<EdgeEventArgs<T>> temp = EdgeTraversed;
            if (temp != null)
            {
                temp(this, new EdgeEventArgs<T>(edge));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void OnComputeCompleted()
        {
            EventHandler<ComputeCompletedEventArgs<T>> temp = ComputeCompleted;
            if (temp != null)
            {
                temp(this, new ComputeCompletedEventArgs<T>(_graph));
            }
        }

        #region IGraphAlgorithm<T> Members

        /// <summary>
        /// 
        /// </summary>
        public IGraph<T> Graph
        {
            get { return _graph; }
            set { _graph = value; }
        }

        /// <summary>
        /// Set the starting node where the graph algorithm with begin its traversal.  Note
        /// that in some algorithms this is ignored.
        /// </summary>
        /// <param name="start">Beginning point of traversal.</param>
        abstract public void SetStartNode(T u);

        /// <summary>
        /// Call this to begin the graph algorithm computation.
        /// </summary>
        abstract public void Compute();

        /// <summary>
        /// Reset data structures so that the algorithm can be recomputed.
        /// </summary>
        public virtual void Reset()
        {
            //  The default Reset method is to clear all the properties because it is
            //  within the Properties container that the algorithms store their 
            //  various structures (e.g. graph coloring, etc.).
            //
            ISet<T> nodes = Graph.Vertices;
            foreach (T u in nodes)
            {
                Graph.GetProperties(u).Clear();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        protected void SetNodeData<TNodeDataType>(INode<T> node, TNodeDataType data) where TNodeDataType : INodeData<T>
        {
            if (node.Properties.ContainsKey(data.GetType()))
            {
                node.Properties[data.GetType()] = data;
            }
            else
            {
                node.Properties.Add(data.GetType(), data);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNodeDataType"></typeparam>
        /// <param name="node"></param>
        /// <param name="data"></param>
        protected void SetNodeData<TNodeDataType>(T u, TNodeDataType data) where TNodeDataType : INodeData<T>
        {
            INode<T> iNode = Graph.GetNode(u);
            SetNodeData(iNode, data);
        }

        #endregion
    }
}
