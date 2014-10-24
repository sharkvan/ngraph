/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;

using nGraph.Collections;
using nGraph;
using nGraph.Interfaces;

namespace nGraph.UnitTests
{
    class TopoSort<T>  where T : IComparable<T>
    {
        private LinkedList<INode<T>> topoList = new LinkedList<INode<T>>();

        public TopoSort()
        {
        }

        public LinkedList<INode<T>> TopoList
        {
            get { return topoList; }
        }

        public void Reset()
        {
            topoList.Clear();
        }

        public void AddNode(object sender, NodeEventArgs<T> e)
        {
            topoList.AddFirst(e.Graph.GetNode(e.Node));
        }
    }

    public class nGraphTests
    {

        public void TestTree()
        {
            Random rnd = new Random();
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.NodeVisited += new EventHandler<BinaryTreeNodeEventArgs<int>>(DisplayTreeNode<int>);
            for (int i = 0; i < 10; i++)
            {
                tree.Add(rnd.Next());
            }
            Console.WriteLine("Root: {0}", tree.Root);
            Console.WriteLine("\nInorder Traversal:");
            tree.InorderTraversal();
            Console.WriteLine("\nPreorder Traversal:");
            tree.PreorderTraversal();
            Console.WriteLine("\nPostorder Traversal:");
            tree.PostorderTraversal();
            Console.WriteLine();
        }

        public void TestUndirected()
        {
            Graph<char> graph = new Graph<char>();

            graph.AddNode('r');
            graph.AddNode('s');
            graph.AddNode('t');
            graph.AddNode('u');
            graph.AddNode('v');
            graph.AddNode('w');
            graph.AddNode('x');
            graph.AddNode('y');

            graph.AddUndirectedEdge('r', 's');
            graph.AddUndirectedEdge('r', 'v');
            graph.AddUndirectedEdge('s', 'w');
            graph.AddUndirectedEdge('w', 't');
            graph.AddUndirectedEdge('w', 'x');
            graph.AddUndirectedEdge('t', 'x');
            graph.AddUndirectedEdge('t', 'u');
            graph.AddUndirectedEdge('x', 'y');
            graph.AddUndirectedEdge('u', 'y');

            Console.WriteLine("G has order {0} and size {1}", graph.Order, graph.Size);

            PrintGraph(graph);
            PrintEdges<char>(graph);

            ISet<char> nodeSet = graph.Vertices;

            PrintNodes(nodeSet);

            foreach (char node in nodeSet)
            {
                foreach (char adjNode in graph.Adjacent(node))
                {
                    Console.WriteLine("({0},{1})", node, adjNode);
                }
            }
            Console.WriteLine();

            ISet<IEdge<char>> edges = graph['s'];

            PrintEdges(edges);

            ISet<char> vertices = graph.Vertices;
            foreach (char n in vertices)
            {
                Console.Write("Nodes adjacent to {0}: ", n);
                ISet<char> adjNodes = graph.Adjacent(n);
                foreach (char an in adjNodes)
                {
                    Console.Write("{0} ", an);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Testing Depth-first search.");
            TestDfs(graph);
            Console.WriteLine("\n\nTesting Breadth-first search.");
            TestBfs(graph, 's');
        }

        public void TestDirected()
        {
            Graph<char> graph = new Graph<char>();

            graph.AddNode('u');
            graph.AddNode('v');
            graph.AddNode('w');
            graph.AddNode('x');
            graph.AddNode('y');
            graph.AddNode('z');
            graph.AddNode('p');

            graph.AddDirectedEdge('u', 'v');
            graph.AddDirectedEdge('u', 'w');
            graph.AddDirectedEdge('v', 'x');
            graph.AddDirectedEdge('v', 'y');
            graph.AddDirectedEdge('w', 'z');
            graph.AddDirectedEdge('z', 'p');
            graph.AddDirectedEdge('x', 'p');
            graph.AddDirectedEdge('y', 'p');

            Console.WriteLine("Testing Depth-first search.");
            TestDfs(graph);
            Console.WriteLine("\n\nTesting Breadth-first search.");
            TestBfs(graph, 'u');
            //AddFilterToEdge(graph);
            //DisplayEdgeProperties(graph);
        }

        private static void DisplayEdgeProperties<T>(Graph<T> graph) where T : IComparable<T>
        {
            DFS<T> dfs = new DFS<T>(graph);
            dfs.EdgeTraversed += new EventHandler<EdgeEventArgs<T>>(GraphEventFunctions.DisplayEdgeProperties);
            dfs.Compute();
        }

        private void DisplayTreeNode<T>(object sender, BinaryTreeNodeEventArgs<T> args) where T : IComparable<T>
        {
            for (int i = 0; i < args.Level; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine("{0}, {1}", args.Node.Key, args.Level);
        }

        private static void AddFilterToEdge<T>(Graph<T> G) where T : IComparable<T>
        {
            DFS<T> dfs = new DFS<T>(G);
            dfs.EdgeTraversed += new EventHandler<EdgeEventArgs<T>>(GraphEventFunctions.AddFilterToEdge<T>);
            dfs.Compute();
        }

        private static void TestBfs<T>(Graph<T> G, T c) where T : IComparable<T>
        {
            BFS<T> bfs = new BFS<T>(G);
            bfs.NodeEntered += new EventHandler<NodeEventArgs<T>>(GraphEventFunctions.DisplayEnteredNode<T>);
            bfs.NodeDiscovered += new EventHandler<NodeEventArgs<T>>(GraphEventFunctions.DisplayDiscoveredNode<T>);
            bfs.NodeExited += new EventHandler<NodeEventArgs<T>>(GraphEventFunctions.DisplayExitedNode<T>);
            bfs.EdgeDiscovered += new EventHandler<EdgeEventArgs<T>>(GraphEventFunctions.DisplayDiscoveredEdge<T>);
            bfs.EdgeTraversed +=new EventHandler<EdgeEventArgs<T>>(GraphEventFunctions.DisplayTraversedEdge<T>);
            bfs.SetStartNode(c);
            bfs.Compute();
            bfs.Graph.ForEachNode(PrintBFSNodeData<T>);

        }

        private static void PrintBFSNodeData<T>(Object sender, NodeEventArgs<T> args) where T : IComparable<T>
        {
            BFSNodeData<T> datum = (BFSNodeData<T>)args.Graph.GetProperties(args.Node)[typeof(BFSNodeData<T>)];
            Console.WriteLine(datum);
        }

        private static void TestDfs<T>(Graph<T> G) where T : IComparable<T>
        {
            TopoSort<T> topoSort = new TopoSort<T>();
            DFS<T> dfs = new DFS<T>(G);
            //dfs.NodeEntered += new EventHandler<NodeEventArgs<T>>(GraphEventFunctions.DisplayEnteredNode<T>);
            //dfs.NodeDiscovered += new EventHandler<NodeEventArgs<T>>(GraphEventFunctions.DisplayDiscoveredNode<T>);
            dfs.NodeExited += new EventHandler<NodeEventArgs<T>>(GraphEventFunctions.DisplayExitedNode<T>);
            dfs.NodeExited += new EventHandler<NodeEventArgs<T>>(topoSort.AddNode);
            dfs.EdgeDiscovered += new EventHandler<EdgeEventArgs<T>>(GraphEventFunctions.DisplayDiscoveredEdge<T>);
            dfs.EdgeTraversed += new EventHandler<EdgeEventArgs<T>>(GraphEventFunctions.DisplayTraversedEdge<T>);
            dfs.Compute();
            dfs.Graph.ForEachNode(PrintDFSNodeData<T>);

            Console.WriteLine("TopoSort...");
            foreach (INode<T> node in topoSort.TopoList)
            {
                Console.WriteLine("Node {0}", node);
            }
        }

        private static void PrintDFSNodeData<T>(Object sender, NodeEventArgs<T> args) where T : IComparable<T>
        {
            DFSNodeData<T> datum = (DFSNodeData<T>)args.Graph.GetProperties(args.Node)[typeof(DFSNodeData<T>)];
            Console.WriteLine(datum);
        }

        private static void PrintNodes<T>(IEnumerable<T> e) where T : IComparable<T>
        {
            Console.WriteLine("\nPrinting Nodes...");
            foreach (T node in e)
            {
                Console.WriteLine(node);
            }
            Console.WriteLine("Done Printing Nodes.\n");
        }

        private static void PrintGraph<T>(Graph<T> g) where T : IComparable<T>
        {
            Console.WriteLine("\nPrinting Nodes...");
            foreach (T node in g)
            {
                Console.WriteLine(node);
            }
            Console.WriteLine("Done Printing Nodes.\n");
        }

        private static void PrintEdges<T>(IEnumerable<IEdge<T>> edgeCollection) where T : IComparable<T>
        {
            Console.WriteLine("\nPrinting Edges...");
            foreach (IEdge<T> edge in edgeCollection)
            {
                Console.WriteLine(edge);
            }
            Console.WriteLine("Done Printing Edges.\n");
        }

    }
}
