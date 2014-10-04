//-----------------------------------------------------------------------
// <copyright file="PrintLetters.cs" company="Eusebio Rufian-Zilbermann">
//   Copyright (c) Eusebio Rufian-Zilbermann
// </copyright>
//-----------------------------------------------------------------------
namespace PrintLetters
{
   using System;
   using System.Text;
   using System.Text.RegularExpressions;

   /// <summary>
   /// Produce mnemonic versions of a 10-digit phone number
   /// </summary>
   public class PrintLetters
   {
      /// <summary>
      /// The table for possible conversions from a digit to a letter.
      /// </summary>
      private static readonly string[][] ConversionTable = new string[10][];

      /// <summary>
      /// Initializes static members of the <see cref="PrintLetters"/> class.
      /// </summary>
      static PrintLetters()
      {
         PrintLetters.ConversionTable[0] = new string[] { "0" };
         PrintLetters.ConversionTable[1] = new string[] { "1" };
         PrintLetters.ConversionTable[2] = new string[] { "A", "B", "C" };
         PrintLetters.ConversionTable[3] = new string[] { "D", "E", "F" };
         PrintLetters.ConversionTable[4] = new string[] { "G", "H", "I" };
         PrintLetters.ConversionTable[5] = new string[] { "J", "K", "L" };
         PrintLetters.ConversionTable[6] = new string[] { "M", "N", "O" };
         PrintLetters.ConversionTable[7] = new string[] { "P", "Q", "R", "S" };
         PrintLetters.ConversionTable[8] = new string[] { "T", "U", "V" };
         PrintLetters.ConversionTable[9] = new string[] { "W", "X", "Y", "Z" };
      }

      /// <summary>
      /// The main executable method.
      /// Parse the input and then convert the last 7 digits out of the 10
      /// (traditionally the area code does not get converted).
      /// </summary>
      /// <param name="args">The input arguments.</param>
      public static void Main(string[] args)
      {
         if (1 != args.Length || null == args[0] || string.IsNullOrEmpty(args[0]))
         {
            Console.WriteLine("Usage: PrintLetter <10 digit phone number as nnnnnnnnnn or n-nnn-nnn-nnnn>");
         }

         string phoneNumberToConvert = args[0];
         Regex noHyphen = new Regex(@"\d{10}");
         Regex withHyphen = new Regex(@"\d-\d{3}-\d{3}-\d{4}");
         if (!(10 == phoneNumberToConvert.Length && noHyphen.IsMatch(phoneNumberToConvert))
            && (13 == phoneNumberToConvert.Length && withHyphen.IsMatch(phoneNumberToConvert)))
         {
            Console.WriteLine("Specify the 10 digit phone number as nnnnnnnnnn or n-nnn-nnn-nnnn");
         }

         phoneNumberToConvert = phoneNumberToConvert.Replace("-", string.Empty);
         StringBuilder sb = new StringBuilder(phoneNumberToConvert.Substring(0, 4), 10);
         Convert(phoneNumberToConvert.ToCharArray(), sb);
      }

      /// <summary>
      /// Recursively convert digits one-by-one and print out the result when finished.
      /// </summary>
      /// <param name="inputNumbers">The original input number as a char array of 10 digits.</param>
      /// <param name="sb">The partial result.</param>
      public static void Convert(char[] inputNumbers, StringBuilder sb)
      {
         if (sb.Length == inputNumbers.Length)
         {
            string result = sb.ToString();
            Console.WriteLine(
               "{0}-{1}-{2}-{3}", 
               result.Substring(0, 1), 
               result.Substring(1, 3), 
               result.Substring(4, 3), 
               result.Substring(7, 4));
            return;
         }

         for (int i = 0; ConversionTable[inputNumbers[sb.Length] - '0'].Length > i; i++)
         {
            sb.Append(ConversionTable[inputNumbers[sb.Length] - '0'][i]);
            Convert(inputNumbers, sb);
            sb.Remove(sb.Length - 1, 1);
         }
      }
   }
}
