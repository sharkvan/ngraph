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
using System.Linq;

namespace TestNGraph
{
    class GraphNodeFuncs
    {
        public static void DisplayEnteredNode<T>(object sender, NodeEventArgs<T> e) where T : IComparable<T>
        {
            Console.WriteLine("Entered: {0}", e.Node);
        }

        public static void DisplayDiscoveredNode<T>(object sender, NodeEventArgs<T> e) where T : IComparable<T>
        {
            Console.WriteLine("Discovered: {0}", e.Node);
        }

        public static void DisplayExitedNode<T>(object sender, NodeEventArgs<T> e) where T : IComparable<T>
        {
            Console.WriteLine("Exited: {0}", e.Node);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.TestUndirected();
            p.TestDirected();
            Console.WriteLine();
            p.TestTree();
            Console.ReadKey(true);
        }

        private void TestTree()
        {
            Random rng = new Random();
            BinaryTree<int> t = new BinaryTree<int>();
            t.NodeVisited += new EventHandler<BinaryTreeNodeEventArgs<int>>(DisplayTreeNode<int>);  //+= new NodeVisitedDelegate<int>(DisplayTreeNode<int>);
            for(int i = 0; i <10; i++)
            {
                t.Add(rng.Next());
            }
            Console.WriteLine("Root: {0}", t.Root);
            Console.WriteLine("\nInorder Traversal:");
            t.InorderTraversal();
            Console.WriteLine("\nPreorder Traversal:");
            t.PreorderTraversal();
            Console.WriteLine("\nPostorder Traversal:");
            t.PostorderTraversal();
            Console.WriteLine();
        }

        public void DisplayTreeNode<T>(object sender, BinaryTreeNodeEventArgs<T> args) where T : IComparable<T>
        {
            for (int i = 0; i < args.Level; i++)
            {
                Console.Write("\t");
            }
            Console.WriteLine("{0}, {1}", args.Node.Key, args.Level);
        }

        public void TestUndirected()
        {
            Graph<char> G = new Graph<char>();

            INode<char> r = G.AddNode('r');
            INode<char> s = G.AddNode('s');
            INode<char> t = G.AddNode('t');
            INode<char> u = G.AddNode('u');
            INode<char> v = G.AddNode('v');
            INode<char> w = G.AddNode('w');
            INode<char> x = G.AddNode('x');
            INode<char> y = G.AddNode('y');

            G.AddUndirectedEdge('r', 's');
            G.AddUndirectedEdge('r', 'v');
            G.AddUndirectedEdge('s', 'w');
            G.AddUndirectedEdge('w', 't');
            G.AddUndirectedEdge('w', 'x');
            G.AddUndirectedEdge('t', 'x');
            G.AddUndirectedEdge('t', 'u');
            G.AddUndirectedEdge('x', 'y');
            G.AddUndirectedEdge('u', 'y');

            Console.WriteLine("G has order {0} and size {1}", G.Order, G.Size);

            ISet<char> nodeSet = G.Vertices;

            foreach (char n in nodeSet)
            {
                foreach(INode<char> an in G.GetNode(n).AdjacentNodes)
                {
                    Console.WriteLine("({0},{1})", n, an);
                }
            }
            Console.WriteLine();

            ISet<IEdge<char>> edges = G['s'];

            foreach (IEdge<char> e in edges)
            {
                Console.WriteLine(e);
            }

            ISet<char> vertices = G.Vertices;
            foreach (char n in vertices)
            {
                Console.Write("Nodes adjacent to {0}: ", n);
                ISet<INode<char>> adjNodes = G.GetNode(n).AdjacentNodes;
                foreach (INode<char> an in adjNodes)
                {
                    Console.Write("{0} ", an);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            TestBfs(G, 's');
        }

        public void TestDirected()
        {
            Graph<char> G = new Graph<char>();

            INode<char> u = G.AddNode('u');
            INode<char> v = G.AddNode('v');
            INode<char> w = G.AddNode('w');
            INode<char> x = G.AddNode('x');
            INode<char> y = G.AddNode('y');
            INode<char> z = G.AddNode('z');

            G.AddDirectedEdge('u', 'v');
            G.AddDirectedEdge('u', 'x');
            G.AddDirectedEdge('x', 'v');
            G.AddDirectedEdge('v', 'y');
            G.AddDirectedEdge('y', 'x');
            G.AddDirectedEdge('w', 'y');
            G.AddDirectedEdge('w', 'z');
            G.AddDirectedEdge('z', 'z');

            TestDfs(G);
        }

        private void TestBfs(Graph<char> G, char c)
        {
            BFS<char> bfs = new BFS<char>(G);

            bfs.NodeEntered += new EventHandler<NodeEventArgs<char>>(GraphNodeFuncs.DisplayEnteredNode<char>);
            bfs.NodeDiscovered += new EventHandler<NodeEventArgs<char>>(GraphNodeFuncs.DisplayDiscoveredNode<char>);
            bfs.NodeExited += new EventHandler<NodeEventArgs<char>>(GraphNodeFuncs.DisplayExitedNode<char>);  
            bfs.SetStartNode(G.GetNode(c).Key);
            bfs.Compute();
            IList<BFSNodeData<char>> results = bfs.Results;

            foreach (BFSNodeData<char> datum in results)
            {
                Console.WriteLine(datum);
            }
        }

        private void TestDfs(Graph<char> G)
        {
            DFS<char> dfs = new DFS<char>(G);
            dfs.NodeEntered += new EventHandler<NodeEventArgs<char>>(GraphNodeFuncs.DisplayEnteredNode<char>);  
            dfs.NodeDiscovered += new EventHandler<NodeEventArgs<char>>(GraphNodeFuncs.DisplayDiscoveredNode<char>);
            dfs.NodeExited += new EventHandler<NodeEventArgs<char>>(GraphNodeFuncs.DisplayExitedNode<char>); 
            dfs.Compute();
            IList<DFSNodeData<char>> results = dfs.Results;

            foreach (DFSNodeData<char> datum in results)
            {
                Console.WriteLine(datum);
            }
        }
    }
}
