using System;

namespace ExpressionEngine.Numbers
{
    public class Fraction
    {
        /// <summary>
        /// Constructors
        /// </summary>
        public Fraction()
        {
            Initialize(0, 1);
        }

        public Fraction(long iWholeNumber)
        {
            Initialize(iWholeNumber, 1);
        }

        public Fraction(double dDecimalValue)
        {
            Fraction temp = ToFraction(dDecimalValue);
            Initialize(temp.Numerator, temp.Denominator);
        }

        public Fraction(string strValue)
        {
            Fraction temp = ToFraction(strValue);
            Initialize(temp.Numerator, temp.Denominator);
        }

        public Fraction(long iNumerator, long iDenominator)
        {
            Initialize(iNumerator, iDenominator);
        }

        /// <summary>
        /// Internal function for constructors
        /// </summary>
        private void Initialize(long iNumerator, long iDenominator)
        {
            Numerator = iNumerator;
            Denominator = iDenominator;
            ReduceFraction(this);
        }

        /// <summary>
        /// Properites
        /// </summary>
        public long Denominator {  get; private set; }

        public long Numerator { get; private set; }

        /// <summary>
        /// The function returns the current Fraction object as double
        /// </summary>
        public double ToDouble()
        {
            return ((double)Numerator / Denominator);
        }

        /// <summary>
        /// The function returns the current Fraction object as a string
        /// </summary>
        public override string ToString()
        {
            string str;
            if (Denominator == 1)
                str = Numerator.ToString();
            else
                str = Numerator + "/" + Denominator;
            return str;
        }
        /// <summary>
        /// The function takes an string as an argument and returns its corresponding reduced fraction
        /// the string can be an in the form of and integer, double or fraction.
        /// e.g it can be like "123" or "123.321" or "123/456"
        /// </summary>
        public static Fraction ToFraction(string strValue)
        {
            int i;
            for (i = 0; i < strValue.Length; i++)
            {
                if (strValue[i] == '/')
                {
                    break;
                }
            }

            if (i == strValue.Length)       // if string is not in the form of a fraction
            {
                // then it is double or integer
                return (Convert.ToDouble(strValue));
            }

            // else string is in the form of Numerator/Denominator
            long iNumerator = Convert.ToInt64(strValue.Substring(0, i));
            long iDenominator = Convert.ToInt64(strValue.Substring(i + 1));
            return new Fraction(iNumerator, iDenominator);
        }


        /// <summary>
        /// The function takes a floating point number as an argument 
        /// and returns its corresponding reduced fraction
        /// </summary>
        public static Fraction ToFraction(double dValue)
        {
            try
            {
                checked
                {
                    if (dValue % 1 == 0)    // if whole number
                    {
                        return new Fraction((long)dValue);
                    }
                    else
                    {
                        double dTemp = dValue;
                        long iMultiple = 1;
                        string strTemp = dValue.ToString();
                        while (strTemp.IndexOf("E") > 0)    // if in the form like 12E-9
                        {
                            dTemp *= 10;
                            iMultiple *= 10;
                            strTemp = dTemp.ToString();
                        }
                        int i = 0;
                        while (strTemp[i] != '.')
                            i++;
                        int iDigitsAfterDecimal = strTemp.Length - i - 1;
                        while (iDigitsAfterDecimal > 0)
                        {
                            dTemp *= 10;
                            iMultiple *= 10;
                            iDigitsAfterDecimal--;
                        }
                        return new Fraction((int)Math.Round(dTemp), iMultiple);
                    }
                }
            }
            catch (OverflowException)
            {
                throw new ExpressionEngineException("Conversion not possible due to overflow");
            }
            catch (Exception)
            {
                throw new ExpressionEngineException("Conversion not possible");
            }
        }

        /// <summary>
        /// The function replicates current Fraction object
        /// </summary>
        public Fraction Duplicate()
        {
            Fraction frac = new Fraction();
            frac.Numerator = Numerator;
            frac.Denominator = Denominator;
            return frac;
        }

        /// <summary>
        /// The function returns the inverse of a Fraction object
        /// </summary>
        public static Fraction Inverse(Fraction frac1)
        {
            if (frac1.Numerator == 0)
                throw new ExpressionEngineException("Operation not possible (Denominator cannot be assigned a ZERO Value)");

            long iNumerator = frac1.Denominator;
            long iDenominator = frac1.Numerator;
            return (new Fraction(iNumerator, iDenominator));
        }


        /// <summary>
        /// Operators for the Fraction object
        /// includes -(unary), and binary opertors such as +,-,*,/
        /// also includes relational and logical operators such as ==,!=,<,>,<=,>=
        /// </summary>
        public static Fraction operator -(Fraction frac1) => Negate(frac1);

        public static Fraction operator +(Fraction frac1, Fraction frac2) => Add(frac1, frac2);

        public static Fraction operator +(double dbl, Fraction frac1) => Add(frac1, Fraction.ToFraction(dbl));

        public static Fraction operator +(Fraction frac1, double dbl) => Add(frac1, Fraction.ToFraction(dbl));

        public static Fraction operator -(Fraction frac1, Fraction frac2) => Add(frac1, -frac2);

        public static Fraction operator -(double dbl, Fraction frac1) => Add(-frac1, Fraction.ToFraction(dbl));

        public static Fraction operator -(Fraction frac1, double dbl) => Add(frac1, -Fraction.ToFraction(dbl));

        public static Fraction operator *(Fraction frac1, Fraction frac2) => Multiply(frac1, frac2);

        public static Fraction operator *(double dbl, Fraction frac1) => Multiply(frac1, Fraction.ToFraction(dbl));

        public static Fraction operator *(Fraction frac1, double dbl) => Multiply(frac1, Fraction.ToFraction(dbl));

        public static Fraction operator /(Fraction frac1, Fraction frac2) => Multiply(frac1, Inverse(frac2));

        public static Fraction operator /(double dbl, Fraction frac1) => Multiply(Inverse(frac1), Fraction.ToFraction(dbl));

        public static Fraction operator /(Fraction frac1, double dbl) => Multiply(frac1, Fraction.Inverse(Fraction.ToFraction(dbl)));

        public static bool operator ==(Fraction frac1, Fraction frac2) => frac1.Equals(frac2);

        public static bool operator !=(Fraction frac1, Fraction frac2) => (!frac1.Equals(frac2));

        public static bool operator ==(Fraction frac1, double dbl) => frac1.Equals(new Fraction(dbl));

        public static bool operator !=(Fraction frac1, double dbl) => (!frac1.Equals(new Fraction(dbl)));

        public static bool operator <(Fraction frac1, Fraction frac2)
        { 
            return frac1.Numerator * frac2.Denominator < frac2.Numerator * frac1.Denominator;
        }

        public static bool operator >(Fraction frac1, Fraction frac2)
        {
            return frac1.Numerator * frac2.Denominator > frac2.Numerator * frac1.Denominator;
        }

        public static bool operator <=(Fraction frac1, Fraction frac2)
        { 
            return frac1.Numerator * frac2.Denominator <= frac2.Numerator * frac1.Denominator;
        }

        public static bool operator >=(Fraction frac1, Fraction frac2)
        { 
            return frac1.Numerator * frac2.Denominator >= frac2.Numerator * frac1.Denominator;
        }

        public static implicit operator Fraction(double dNo)
        {
            return new Fraction(dNo);
        }

        public static implicit operator Fraction(string strNo)
        { 
            return new Fraction(strNo); 
        }

        /// <summary>
        /// overloaed user defined conversions: from fractions to double and string
        /// </summary>
        public static explicit operator double(Fraction frac)
        { 
            return frac.ToDouble();
        }

        /// <summary>
        /// checks whether two fractions are equal
        /// </summary>
        public override bool Equals(object obj)
        {
            Fraction frac = (Fraction)obj;
            return (Numerator == frac.Numerator && Denominator == frac.Denominator);
        }

        /// <summary>
        /// returns a hash code for this fraction
        /// </summary>
        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        /// <summary>
        /// internal function for negation
        /// </summary>
        private static Fraction Negate(Fraction frac1)
        {
            long iNumerator = -frac1.Numerator;
            long iDenominator = frac1.Denominator;
            return (new Fraction(iNumerator, iDenominator));

        }

        /// <summary>
        /// internal functions for binary operations
        /// </summary>
        private static Fraction Add(Fraction frac1, Fraction frac2)
        {
            try
            {
                checked
                {
                    long iNumerator = frac1.Numerator * frac2.Denominator + frac2.Numerator * frac1.Denominator;
                    long iDenominator = frac1.Denominator * frac2.Denominator;
                    return (new Fraction(iNumerator, iDenominator));
                }
            }
            catch (OverflowException)
            {
                throw new ExpressionEngineException("Overflow occurred while performing arithemetic operation");
            }
            catch (Exception)
            {
                throw new ExpressionEngineException("An error occurred while performing arithemetic operation");
            }
        }

        private static Fraction Multiply(Fraction frac1, Fraction frac2)
        {
            try
            {
                checked
                {
                    long iNumerator = frac1.Numerator * frac2.Numerator;
                    long iDenominator = frac1.Denominator * frac2.Denominator;
                    return (new Fraction(iNumerator, iDenominator));
                }
            }
            catch (OverflowException)
            {
                throw new ExpressionEngineException("Overflow occurred while performing arithemetic operation");
            }
            catch (Exception)
            {
                throw new ExpressionEngineException("An error occurred while performing arithemetic operation");
            }
        }

        /// <summary>
        /// The function returns GCD of two numbers (used for reducing a Fraction)
        /// </summary>
        private static long GCD(long iNo1, long iNo2)
        {
            // take absolute values
            if (iNo1 < 0) iNo1 = -iNo1;
            if (iNo2 < 0) iNo2 = -iNo2;

            do
            {
                if (iNo1 < iNo2)
                {
                    long tmp = iNo1;  // swap the two operands
                    iNo1 = iNo2;
                    iNo2 = tmp;
                }
                iNo1 = iNo1 % iNo2;
            } while (iNo1 != 0);
            return iNo2;
        }

        /// <summary>
        /// The function reduces(simplifies) a Fraction object by dividing both its numerator 
        /// and denominator by their GCD
        /// </summary>
        public static void ReduceFraction(Fraction frac)
        {
            try
            {
                if (frac.Numerator == 0)
                {
                    frac.Denominator = 1;
                    return;
                }

                long iGCD = GCD(frac.Numerator, frac.Denominator);
                frac.Numerator /= iGCD;
                frac.Denominator /= iGCD;

                if (frac.Denominator < 0)   // if -ve sign in denominator
                {
                    //pass -ve sign to numerator
                    frac.Numerator *= -1;
                    frac.Denominator *= -1;
                }
            } // end try
            catch (Exception exp)
            {
                throw new ExpressionEngineException("Cannot reduce Fraction: " + exp.Message);
            }
        }

    }
}