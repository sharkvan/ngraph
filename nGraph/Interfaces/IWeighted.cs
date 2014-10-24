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
    public interface IWeighted
    {
        decimal Cost { get; set; }
    }
}
