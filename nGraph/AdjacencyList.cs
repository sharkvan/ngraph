/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;
using nGraph.Interfaces;
using System.Collections.ObjectModel;


namespace nGraph
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdjacencyList<T> : Collection<INode<T>>, IAdjacencyList<T> where T : IComparable<T>
    {
    }

}
