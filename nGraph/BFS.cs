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
    /// Breadth-First-Search algorithm.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BFS<T> : BaseGraphAlgorithm<T> where T : IComparable<T>
    {
        private bool _startNodeSet = false;
        private T _startNode;
        private Type NodeDataType = typeof(BFSNodeData<T>);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="graph"></param>
        public BFS(IGraph<T> graph)
            : base(graph)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public override void SetStartNode(T u)
        {
            _startNodeSet = true;
            _startNode = u;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Compute()
        {
            if (!_startNodeSet)
            {
                throw new Exception("Must set Start Node first.");
            }

            ISet<T> nodes = Graph.Vertices;
            foreach (T u in nodes)
            {
                if (!u.Equals(_startNode))
                {
                    SetNodeData(u, new BFSNodeData<T>(u, Color.White, -1));
                }
            }

            SetNodeData(_startNode, new BFSNodeData<T>(_startNode, Color.Gray, 0));

            Queue<T> queue = new Queue<T>();
            queue.Enqueue(_startNode);
            while (queue.Count > 0)
            {
                T u = queue.Peek();

                OnNodeEntered(u);

                BFSNodeData<T> bvu = Graph.GetProperties(u)[NodeDataType] as BFSNodeData<T>;
                ISet<T> nodeList = Graph.Adjacent(u);
                foreach (T adjacent in nodeList)
                {
                    OnEdgeDiscovered(Graph[u, adjacent]);
                    BFSNodeData<T> bvv = Graph.GetProperties(adjacent)[NodeDataType] as BFSNodeData<T>;
                    if (bvv != null && bvv.Color == Color.White)
                    {
                        bvv.Color = Color.Gray;
                        bvv.Distance = bvu.Distance + 1;
                        bvv.Predecessor = u;
                        queue.Enqueue(adjacent);
                    }

                    OnNodeDiscovered(adjacent);
                    OnEdgeTraversed(Graph[u, adjacent]);
                }
                T v = queue.Dequeue();
                bvu.Color = Color.Black;
                OnNodeExited(u);
            }
            OnComputeCompleted();
        }

        /// <summary>
        /// 
        /// </summary>
        public IList<BFSNodeData<T>> Results
        {
            get
            {
                IList<BFSNodeData<T>> results = new List<BFSNodeData<T>>();
                foreach (T n in Graph.Vertices)
                {
                    if (Graph.GetProperties(n).ContainsKey(NodeDataType))
                    {
                        BFSNodeData<T> nodeData = Graph.GetProperties(n)[NodeDataType] as BFSNodeData<T>;
                        if (nodeData != null)
                        {
                            results.Add(nodeData);
                        }
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
    public class BFSNodeData<T> : BaseNodeData<T> where T : IComparable<T>
    {
        private T _node;
        private int _distance;

        /// <summary>
        /// 
        /// </summary>
        public BFSNodeData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="color"></param>
        /// <param name="distance"></param>
        public BFSNodeData(T node, Color color, int distance)
            : base(color)
        {
            _node = node;
            _distance = distance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="color"></param>
        /// <param name="distance"></param>
        /// <param name="predecessor"></param>
        public BFSNodeData(T node, Color color, int distance, T predecessor)
            : base(predecessor, color)
        {
            _node = node;
            _distance = distance;
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
        public int Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("Node: {0}, Color: {1}, Predecessor: {2}, Distance: {3}", 
                Node, Color, Predecessor, Distance);
        }
    }
}
