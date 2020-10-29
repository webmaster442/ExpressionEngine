﻿//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using ExpressionEngine.Maths;
using NUnit.Framework;
using System;

namespace ExpressionEngine.Tests.Maths
{
    [TestFixture]
    public class SpecialFunctionsTests
    {
        [TestCase(Double.NaN, Double.NaN)]
        [TestCase(1.000001e-35, 9.9999900000100041E34d)]
        [TestCase(1.000001e-10, 9.99998999943278432519738283781280989934496494539074049002e+9)]
        [TestCase(1.000001e-5, 99999.32279432557746387178953902739303931424932435387031653234)]
        [TestCase(1.000001e-2, 99.43248512896257405886134437203369035261893114349805309870831)]
        [TestCase(-4.8, -0.06242336135475955314181664931547009890495158793105543559676)]
        [TestCase(-1.5, 2.363271801207354703064223311121526910396732608163182837618410)]
        [TestCase(-0.5, -3.54490770181103205459633496668229036559509891224477425642761)]
        [TestCase(1.0e-5 + 1.0e-16, 99999.42279322556767360213300482199406241771308740302819426480)]
        [TestCase(0.1, 9.513507698668731836292487177265402192550578626088377343050000)]
        [TestCase(1.0 - 1.0e-14, 1.000000000000005772156649015427511664653698987042926067639529)]
        [TestCase(1.0, 1.0)]
        [TestCase(1.0 + 1.0e-14, 0.99999999999999422784335098477029953441189552403615306268023)]
        [TestCase(1.5, 0.886226925452758013649083741670572591398774728061193564106903)]
        [TestCase(Math.PI / 2, 0.890560890381539328010659635359121005933541962884758999762766)]
        [TestCase(2.0, 1.0)]
        [TestCase(2.5, 1.329340388179137020473625612505858887098162092091790346160355)]
        [TestCase(3.0, 2.0)]
        [TestCase(Math.PI, 2.288037795340032417959588909060233922889688153356222441199380)]
        [TestCase(3.5, 3.323350970447842551184064031264647217745405230229475865400889)]
        [TestCase(4.0, 6.0)]
        [TestCase(4.5, 11.63172839656744892914422410942626526210891830580316552890311)]
        [TestCase(5.0 - 1.0e-14, 23.99999999999963853175957637087420162718107213574617032780374)]
        [TestCase(5.0, 24.0)]
        [TestCase(5.0 + 1.0e-14, 24.00000000000036146824042363510111050137786752408660789873592)]
        [TestCase(5.5, 52.34277778455352018114900849241819367949013237611424488006401)]
        [TestCase(10.1, 454760.7514415859508673358368319076190405047458218916492282448)]
        [TestCase(150 + 1.0e-12, 3.8089226376501193E260)]
        public void Gamma(double z, double expected)
        {
            double result = SpecialFunctions.Gamma(z);
            Assert.AreEqual(expected, result, 1E-5);
        }
    }
}