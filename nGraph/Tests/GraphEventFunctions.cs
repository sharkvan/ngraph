/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;

using nGraph.Interfaces;

namespace nGraph.UnitTests
{
    public static class GraphEventFunctions
    {
        public static void DisplayEnteredNode<T>(object sender, NodeEventArgs<T> e) where T : IComparable<T>
        {
            Console.WriteLine("Entered:\t{0}", e.Node);
        }

        public static void DisplayDiscoveredNode<T>(object sender, NodeEventArgs<T> e) where T : IComparable<T>
        {
            Console.WriteLine("Discovered:\t{0}", e.Node);
        }

        public static void DisplayExitedNode<T>(object sender, NodeEventArgs<T> e) where T : IComparable<T>
        {
            Console.WriteLine("Exited:\t{0}", e.Node);
        }

        public static void DisplayDiscoveredEdge<T>(object sender, EdgeEventArgs<T> e) where T : IComparable<T>
        {
            Console.WriteLine("Edge Discovered: Cost = {0}, Data = {1}, SourceNode = {2}, TargetNode = {3}",
                e.Edge.Cost, e.Edge.Data, e.Edge.SourceNode, e.Edge.TargetNode);
        }

        public static void DisplayTraversedEdge<T>(object sender, EdgeEventArgs<T> e) where T : IComparable<T>
        {
            Console.WriteLine("Edge Traversed: Cost = {0}, Data = {1}, SourceNode = {2}, TargetNode = {3}",
                e.Edge.Cost, e.Edge.Data, e.Edge.SourceNode, e.Edge.TargetNode);
        }

        public static void DisplayEdgeProperties<T>(object sender, EdgeEventArgs<T> e) where T : IComparable<T>
        {
            IEdge<T> edge = e.Edge;
            if (edge != null)
            {
                IDictionary<Object, Object> properties = edge.Properties;
                foreach (KeyValuePair<object, object> pair in properties)
                {
                    Console.WriteLine("{0} = {1}", 
                        (pair.Key != null ? pair.Key.ToString() : "<null>"), 
                        (pair.Value != null ? pair.Value.ToString() : "<null>"));
                }
            }
        }

        public static void AddFilterToEdge<T>(object sender, EdgeEventArgs<T> e) where T : IComparable<T>
        {
            IEdge<T> edge = e.Edge;
            if (edge != null)
            {
                if (!edge.Properties.ContainsKey("Filter"))
                {
                    Console.WriteLine("Adding filter to edge {0}", edge);
                    edge.Properties.Add("Filter", "This is a Filter!");
                }
            }
        }
    }
}
