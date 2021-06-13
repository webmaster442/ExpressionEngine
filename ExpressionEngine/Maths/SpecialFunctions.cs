//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------
// code taken from mathnet-numerics
// https://github.com/mathnet/mathnet-numerics

using System;

namespace ExpressionEngine.Maths
{
    public class SpecialFunctions
    {
        /// <summary>
        /// The order of the <see cref="GammaLn"/> approximation.
        /// </summary>
        const int GammaN = 10;

        /// <summary>
        /// Polynomial coefficients for the <see cref="GammaLn"/> approximation.
        /// </summary>
        static readonly double[] GammaDk =
        {
            2.48574089138753565546e-5,
            1.05142378581721974210,
            -3.45687097222016235469,
            4.51227709466894823700,
            -2.98285225323576655721,
            1.05639711577126713077,
            -1.95428773191645869583e-1,
            1.70970543404441224307e-2,
            -5.71926117404305781283e-4,
            4.63399473359905636708e-6,
            -2.71994908488607703910e-9
        };

        /// <summary>
        /// Auxiliary variable when evaluating the <see cref="GammaLn"/> function.
        /// </summary>
        const double GammaR = 10.900511;

        /// <summary>The number 2 * sqrt(e / pi)</summary>
        public const double TwoSqrtEOverPi = 1.8603827342052657173362492472666631120594218414085755;

        public static double Gamma(double z)
        {
            if (z < 0.5)
            {
                double s = GammaDk[0];
                for (int i = 1; i <= GammaN; i++)
                {
                    s += GammaDk[i] / (i - z);
                }

                return Math.PI / (Math.Sin(Math.PI * z)
                                * s
                                * TwoSqrtEOverPi
                                * Math.Pow((0.5 - z + GammaR) / Math.E, 0.5 - z));
            }
            else
            {
                double s = GammaDk[0];
                for (int i = 1; i <= GammaN; i++)
                {
                    s += GammaDk[i] / (z + i - 1.0);
                }

                return s * TwoSqrtEOverPi * Math.Pow((z - 0.5 + GammaR) / Math.E, z - 0.5);
            }
        }
    }
}
