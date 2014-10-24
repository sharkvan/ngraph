/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace nGraph.Collections
{
    /// <summary>
    /// 
    /// </summary>
    public class SetCountComparer<T> : IComparer<ISet<T>>
    {
        #region IComparer<Set<T>> Members

        public int Compare(ISet<T> x, ISet<T> y)
        {
            if (x == null) throw new ArgumentNullException("x");
            if (y == null) throw new ArgumentNullException("y");
            if (x.Count > y.Count)
            {
                return 1;
            }
            if (x.Count == y.Count)
            {
                return 0;
            }
            return -1;

        }

        #endregion
    }
}
