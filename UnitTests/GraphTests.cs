using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nGraph;
using System.Collections.Generic;
using nGraph.Interfaces;

namespace UnitTests
{
    [TestClass]
    public class GraphTests
    {
        Graph<char> graph = new Graph<char>();

        public GraphTests()
        {
            graph.AddNode('r');
            graph.AddNode('s');
            graph.AddNode('t');
            graph.AddNode('u');
            graph.AddNode('v');
            graph.AddNode('w');
            graph.AddNode('x');
            graph.AddNode('y');

            graph.AddEdge('r', 's');
            graph.AddEdge('r', 'v');
            graph.AddEdge('s', 'w');
            graph.AddEdge('w', 't');
            graph.AddEdge('w', 'x');
            graph.AddEdge('t', 'x');
            graph.AddEdge('t', 'u');
            graph.AddEdge('x', 'y');
            graph.AddEdge('u', 'y');
        }

        [TestMethod]
        public void Order()
        {
            Assert.AreEqual(8, graph.Order);
        }

        [TestMethod]
        public void Size()
        {
            Assert.AreEqual(18, graph.Size);
        }

        [TestMethod]
        public void Edges()
        {
            IEdge<char>[] edges = new IEdge<char>[18];

            graph.Edges.CopyTo(edges, 0);
            Assert.AreEqual(18, graph.Edges.Count);
            Assert.AreEqual("(r,s)", edges[0].ToString());
            Assert.AreEqual("(r,v)", edges[1].ToString());
            Assert.AreEqual("(s,r)", edges[2].ToString());
            Assert.AreEqual("(s,w)", edges[3].ToString());
            Assert.AreEqual("(t,w)", edges[4].ToString());
            Assert.AreEqual("(t,x)", edges[5].ToString());
            Assert.AreEqual("(t,u)", edges[6].ToString());
            Assert.AreEqual("(u,t)", edges[7].ToString());
            Assert.AreEqual("(u,y)", edges[8].ToString());
            Assert.AreEqual("(v,r)", edges[9].ToString());
            Assert.AreEqual("(w,s)", edges[10].ToString());
            Assert.AreEqual("(w,t)", edges[11].ToString());
            Assert.AreEqual("(w,x)", edges[12].ToString());
            Assert.AreEqual("(x,w)", edges[13].ToString());
            Assert.AreEqual("(x,t)", edges[14].ToString());
            Assert.AreEqual("(x,y)", edges[15].ToString());
            Assert.AreEqual("(y,x)", edges[16].ToString());
            Assert.AreEqual("(y,u)", edges[17].ToString());

        }

        [TestMethod]
        public void AdjacentNodes()
        {
            INode<char>[] nodes = new INode<char>[3];

            graph.GetNode('r').AdjacentNodes.CopyTo(nodes, 0);
            Assert.AreEqual("s", nodes[0].ToString());
            Assert.AreEqual("v", nodes[1].ToString());

            graph.GetNode('s').AdjacentNodes.CopyTo(nodes, 0);
            Assert.AreEqual("r", nodes[0].ToString());
            Assert.AreEqual("w", nodes[1].ToString());

            graph.GetNode('t').AdjacentNodes.CopyTo(nodes, 0);
            Assert.AreEqual("w", nodes[0].ToString());
            Assert.AreEqual("x", nodes[1].ToString());
            Assert.AreEqual("u", nodes[2].ToString());

            graph.GetNode('u').AdjacentNodes.CopyTo(nodes, 0);
            Assert.AreEqual("t", nodes[0].ToString());
            Assert.AreEqual("y", nodes[1].ToString());

            graph.GetNode('v').AdjacentNodes.CopyTo(nodes, 0);
            Assert.AreEqual("r", nodes[0].ToString());

            graph.GetNode('w').AdjacentNodes.CopyTo(nodes, 0);
            Assert.AreEqual("s", nodes[0].ToString());
            Assert.AreEqual("t", nodes[1].ToString());
            Assert.AreEqual("x", nodes[2].ToString());

            graph.GetNode('x').AdjacentNodes.CopyTo(nodes, 0);
            Assert.AreEqual("w", nodes[0].ToString());
            Assert.AreEqual("t", nodes[1].ToString());
            Assert.AreEqual("y", nodes[2].ToString());

            graph.GetNode('y').AdjacentNodes.CopyTo(nodes, 0);
            Assert.AreEqual("x", nodes[0].ToString());
            Assert.AreEqual("u", nodes[1].ToString());
        }

        [TestMethod]
        public void Index()
        {
            IEdge<char>[] edges = new IEdge<char>[2];
            graph['s'].CopyTo(edges, 0);

            Assert.AreEqual("(s,r)", edges[0].ToString());
            Assert.AreEqual("(s,w)", edges[1].ToString());
        }
    }
}
