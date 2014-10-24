/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace nGraph.Interfaces
{
    public interface IGraphAlgorithm<T> where T : IComparable<T>
    {
        IGraph<T> Graph { get; set; }
        void SetStartNode(T u);
        void Compute();
        void Reset();

        event EventHandler<NodeEventArgs<T>> NodeEntered;
        event EventHandler<NodeEventArgs<T>> NodeDiscovered;
        event EventHandler<NodeEventArgs<T>> NodeExited;
        event EventHandler<EdgeEventArgs<T>> EdgeDiscovered;
        event EventHandler<EdgeEventArgs<T>> EdgeTraversed;
        event EventHandler<ComputeCompletedEventArgs<T>> ComputeCompleted;
    }
}
