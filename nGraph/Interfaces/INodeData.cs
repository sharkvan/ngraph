/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace nGraph.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface INodeData<T> where T : IComparable<T>
    {
        /// <summary>
        /// 
        /// </summary>
        T Predecessor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Color Color { get; set; }
    }
}
