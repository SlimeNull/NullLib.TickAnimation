//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
// 
// File: DoubleUtil.cs
//
// Description: This file contains the implementation of DoubleUtil, which 
//              provides "fuzzy" comparison functionality for doubles and 
//              double-based classes and structs in our code.
// 
// History:  
//  04/28/2003 : Microsoft - Created
//  05/20/2003 : Microsoft - Moved it to Shared, so Base, Core and Framework can all share.
//
//---------------------------------------------------------------------------

using System;

namespace MS.Internal
{
    internal static class DoubleUtil
    {
        internal const double DBL_EPSILON = 2.2204460492503131e-016; /* smallest such that 1.0+DBL_EPSILON != 1.0 */

        public static bool IsOne(double value)
        {
            return Math.Abs(value - 1.0) < 10.0 * DBL_EPSILON;
        }
        public static bool IsZero(double value)
        {
            return Math.Abs(value) < 10.0 * DBL_EPSILON;
        }
    }
}