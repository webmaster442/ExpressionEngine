//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using System;
using System.Numerics;

namespace ExpressionEngine
{
    public interface INumber: IComparable<INumber>, IEquatable<INumber>, ICloneable
    {
        BigInteger Numerator { get; }
        BigInteger Denominator { get; }
        string ToString(IFormatProvider formatProvider);
        double ToDouble();
        new INumber Clone();
    }
}
