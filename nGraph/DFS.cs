/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using nGraph.Interfaces;
using nGraph.Collections;

namespace nGraph
{
    /// <summary>
    /// Depth-first search algorithm for graphs.
    /// </summary>
    public class DFS<T> : BaseGraphAlgorithm<T> where T : IComparable<T>
    {
        private int _time = 0;
        private T _startNode;
        private bool _useStartNode = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        public DFS(IGraph<T> graph)
            : base(graph)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override void SetStartNode(T s)
        {
            _startNode = s;
            _useStartNode = true;
        }

        /// <summary>
        /// Compute Depth-first search.
        /// </summary>
        public override void Compute()
        {
            ISet<T> nodes= Graph.Vertices;

            foreach (T u in nodes)
                SetNodeData(u, new DFSNodeData<T>(u, Color.White));

            _time = 0;
            if (_useStartNode)
            {
                DFSNodeData<T> uData = GetNodeData<DFSNodeData<T>>(_startNode);

                //this is the actual discovery
                if (uData != null && uData.Color == Color.White)
                {
                    OnNodeDiscovered(_startNode);
                    Visit(_startNode, uData);
                }
            }

            foreach (T u in nodes)
            {
                DFSNodeData<T> uData = GetNodeData<DFSNodeData<T>>(u);

                //this is the actual discovery
                if (uData != null && uData.Color == Color.White)
                {
                    OnNodeDiscovered(u);
                    Visit(u, uData);
                }
            }

            OnComputeCompleted();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entered"></param>
        /// <param name="uData"></param>
        private void Visit(T entered, DFSNodeData<T> uData)
        {
            uData.Color = Color.Gray;
            uData.Discovered = ++_time;

            OnNodeEntered(entered);

            ISet<T> adjNodes = Graph.Adjacent(entered);
            foreach (T discovered in adjNodes)
            {
                OnEdgeDiscovered(Graph[entered, discovered]);
                DFSNodeData<T> vData = GetNodeData<DFSNodeData<T>>(discovered);

                if (vData.Color == Color.White)
                {
                    vData.Predecessor = entered;
                    OnNodeDiscovered(discovered);
                    Visit(discovered, vData);
                }

                OnEdgeTraversed(Graph[entered, discovered]);
            }
            uData.Color = Color.Black;
            uData.Finished = ++_time;
            OnNodeExited(entered);
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<DFSNodeData<T>> Results
        {
            get
            {
                IList<DFSNodeData<T>> results = new List<DFSNodeData<T>>();
                foreach (T n in Graph.Vertices)
                {
                    DFSNodeData<T> nodeData = GetNodeData<DFSNodeData<T>>(n);
                    if (nodeData != null)
                    {
                        results.Add(nodeData);
                    }
                }
                return results;
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DFSNodeData<T> : BaseNodeData<T> where T : IComparable<T>
    {
        private T _node;
        private int _discovered;
        private int _finished;

        /// <summary>
        /// 
        /// </summary>
        public DFSNodeData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="color"></param>
        public DFSNodeData(T node, Color color)
            : base(color)
        {
            _node = node;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="color"></param>
        /// <param name="predecessor"></param>
        public DFSNodeData(T node, Color color, T predecessor)
            : base(predecessor, color)
        {
            _node = node;
        }

        /// <summary>
        /// 
        /// </summary>
        public T Node
        {
            get { return _node; }
            set { _node = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Discovered
        {
            get { return _discovered; }
            set { _discovered = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Finished
        {
            get { return _finished; }
            set { _finished = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("Node: {0}, Color: {1}, Predecessor: {2}, Discovered: {3}, Finished: {4}",
                Node, Color, Predecessor, Discovered, Finished);
        }
    }
}
