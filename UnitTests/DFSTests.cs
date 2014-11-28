using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nGraph;
using System.Collections.Generic;
using nGraph.Interfaces;

namespace UnitTests
{
    [TestClass]
    public class DFSTests
    {
        Graph<char> graph = new Graph<char>();

        public DFSTests()
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
        public void Compute()
        {
            List<char> nodeFindOrder = new List<char>();
            List<IEdge<char>> edgeFindOrder = new List<IEdge<char>>();
            DFS<char> search = new DFS<char>(graph);

            search.NodeDiscovered += (sender, e) => nodeFindOrder.Add(e.Node);
            search.EdgeDiscovered += (sender, e) => edgeFindOrder.Add(e.Edge);

            search.Compute();

            Assert.AreEqual('r', nodeFindOrder[0]);
            Assert.AreEqual('s', nodeFindOrder[1]);
            Assert.AreEqual('w', nodeFindOrder[2]);
            Assert.AreEqual('t', nodeFindOrder[3]);
            Assert.AreEqual('x', nodeFindOrder[4]);
            Assert.AreEqual('y', nodeFindOrder[5]);
            Assert.AreEqual('u', nodeFindOrder[6]);
            Assert.AreEqual('v', nodeFindOrder[7]);

            Assert.AreEqual("(r,s)", edgeFindOrder[0].ToString());
            Assert.AreEqual("(s,r)", edgeFindOrder[1].ToString());
            Assert.AreEqual("(s,w)", edgeFindOrder[2].ToString());
            Assert.AreEqual("(w,s)", edgeFindOrder[3].ToString());
            Assert.AreEqual("(w,t)", edgeFindOrder[4].ToString());
            Assert.AreEqual("(t,w)", edgeFindOrder[5].ToString());
            Assert.AreEqual("(t,x)", edgeFindOrder[6].ToString());
            Assert.AreEqual("(x,w)", edgeFindOrder[7].ToString());
            Assert.AreEqual("(x,t)", edgeFindOrder[8].ToString());
            Assert.AreEqual("(x,y)", edgeFindOrder[9].ToString());
            Assert.AreEqual("(y,x)", edgeFindOrder[10].ToString());
            Assert.AreEqual("(y,u)", edgeFindOrder[11].ToString());
            Assert.AreEqual("(u,t)", edgeFindOrder[12].ToString());
            Assert.AreEqual("(u,y)", edgeFindOrder[13].ToString());
            Assert.AreEqual("(t,u)", edgeFindOrder[14].ToString());
            Assert.AreEqual("(w,x)", edgeFindOrder[15].ToString());
            Assert.AreEqual("(r,v)", edgeFindOrder[16].ToString());
            Assert.AreEqual("(v,r)", edgeFindOrder[17].ToString());
        }


        [TestMethod]
        public void StartTraversalAtSpecificNode()
        {
            List<char> nodeFindOrder = new List<char>();
            List<IEdge<char>> edgeFindOrder = new List<IEdge<char>>();
            DFS<char> search = new DFS<char>(graph);
            search.SetStartNode('v');

            search.NodeDiscovered += (sender, e) => nodeFindOrder.Add(e.Node);
            search.EdgeDiscovered += (sender, e) => edgeFindOrder.Add(e.Edge);

            search.Compute();

            Assert.AreEqual('v', nodeFindOrder[0]);
            Assert.AreEqual('r', nodeFindOrder[1]);
            Assert.AreEqual('s', nodeFindOrder[2]);
            Assert.AreEqual('w', nodeFindOrder[3]);
            Assert.AreEqual('t', nodeFindOrder[4]);
            Assert.AreEqual('x', nodeFindOrder[5]);
            Assert.AreEqual('y', nodeFindOrder[6]);
            Assert.AreEqual('u', nodeFindOrder[7]);

            Assert.AreEqual("(v,r)", edgeFindOrder[0].ToString());
            Assert.AreEqual("(r,s)", edgeFindOrder[1].ToString());
            Assert.AreEqual("(s,r)", edgeFindOrder[2].ToString());
            Assert.AreEqual("(s,w)", edgeFindOrder[3].ToString());
            Assert.AreEqual("(w,s)", edgeFindOrder[4].ToString());
            Assert.AreEqual("(w,t)", edgeFindOrder[5].ToString());
            Assert.AreEqual("(t,w)", edgeFindOrder[6].ToString());
            Assert.AreEqual("(t,x)", edgeFindOrder[7].ToString());
            Assert.AreEqual("(x,w)", edgeFindOrder[8].ToString());
            Assert.AreEqual("(x,t)", edgeFindOrder[9].ToString());
            Assert.AreEqual("(x,y)", edgeFindOrder[10].ToString());
            Assert.AreEqual("(y,x)", edgeFindOrder[11].ToString());
            Assert.AreEqual("(y,u)", edgeFindOrder[12].ToString());
            Assert.AreEqual("(u,t)", edgeFindOrder[13].ToString());
            Assert.AreEqual("(u,y)", edgeFindOrder[14].ToString());
            Assert.AreEqual("(t,u)", edgeFindOrder[15].ToString());
            Assert.AreEqual("(w,x)", edgeFindOrder[16].ToString());
            Assert.AreEqual("(r,v)", edgeFindOrder[17].ToString());
        }
    }
}
