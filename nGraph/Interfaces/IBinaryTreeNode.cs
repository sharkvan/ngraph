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
    public interface IBinaryTreeNode<T> : INode<T> where T : IComparable<T>
    {
        IBinaryTreeNode<T> LeftNode { get; set; }
        IBinaryTreeNode<T> RightNode { get; set; }
    }
}
