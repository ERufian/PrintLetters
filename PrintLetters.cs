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
      private static readonly char[,] ConversionTable = new char[10, 4];

      /// <summary>
      /// Initializes static members of the <see cref="PrintLetters"/> class.
      /// </summary>
      static PrintLetters()
      {
         PrintLetters.ConversionTable = new char[10, 4] 
         { 
            { '0', ':', ':', ':' },
            { '1', ':', ':', ':' },
            { 'A', 'B', 'C', ':' },
            { 'D', 'E', 'F', ':' },
            { 'G', 'H', 'I', ':' },
            { 'J', 'K', 'L', ':' },
            { 'M', 'N', 'O', ':' },
            { 'P', 'Q', 'R', 'S' },
            { 'T', 'U', 'V', ':' },
            { 'W', 'X', 'Y', 'Z' }
         };
      }

      /// <summary>
      /// The main executable method.
      /// Parse the input and then convert the last 7 digits out of the 10
      /// (follow the tradition where the area code does not get converted if it is 1-800 or 1-888).
      /// </summary>
      /// <param name="args">The input arguments.</param>
      public static void Main(string[] args)
      {
         if (1 != args.Length || null == args[0] || string.IsNullOrEmpty(args[0]))
         {
            Console.WriteLine("Usage: PrintLetter <10 digit phone number as nnnnnnnnnn or n-nnn-nnn-nnnn>");
            return;
         }

         string phoneNumberToConvert = args[0];
         Regex noHyphen = new Regex(@"\d{10}");
         Regex withHyphen = new Regex(@"\d-\d{3}-\d{3}-\d{4}");
         if (!(10 == phoneNumberToConvert.Length && noHyphen.IsMatch(phoneNumberToConvert))
            && (13 == phoneNumberToConvert.Length && withHyphen.IsMatch(phoneNumberToConvert)))
         {
            Console.WriteLine("Specify the 10 digit phone number as nnnnnnnnnn or n-nnn-nnn-nnnn");
            return;
         }

         phoneNumberToConvert = phoneNumberToConvert.Replace("-", string.Empty);
         string printPrefix = string.Empty;

         if (0 == string.Compare(phoneNumberToConvert, 0, "1800", 0, 4, StringComparison.OrdinalIgnoreCase))
         {
            phoneNumberToConvert = phoneNumberToConvert.Substring(4);
            printPrefix = "1-800-";
         }
         else if (0 == string.Compare(phoneNumberToConvert, 0, "1888", 0, 4, StringComparison.OrdinalIgnoreCase))
         {
            phoneNumberToConvert = phoneNumberToConvert.Substring(4);
            printPrefix = "1-888-";
         }
            
         Convert(phoneNumberToConvert.ToCharArray(), printPrefix);
      }

      /// <summary>
      /// Use a counter to produce all possible combinations and convert digits.
      /// </summary>
      /// <param name="inputNumbers">The original input number as a char array of 10 digits</param>
      /// <param name="printPrefix">An optional prefix to print first (this can be used for cases like 1-800-nnnnnnn where the 1-800 is not converted).</param>
      /// <remarks>
      /// The conversion table has up to 4 items, this routine uses bit manipulation to count in base 4
      /// </remarks>
      public static void Convert(char[] inputNumbers, string printPrefix)
      {
         for (int i = 0; inputNumbers.Length > i; ++i)
         {
            inputNumbers[i] -= '0';
         }
         
         // Upper limit for counter: 2 bits per digit
         long upperLimit = 1 << (inputNumbers.Length << 1); // Use long to support the full range of 10 digits if needed
         bool printResult = true;
         
         // Optimize inputs of the form 1-nnn... where we can count in increments of 4
         int counterIncrement = (inputNumbers[0] == 1) ? 4 : 1;
         for (long counter = 0; upperLimit > counter; counter += counterIncrement)
         {
            char[] candidateBuilder = new char[inputNumbers.Length];

            for (int j = 0; inputNumbers.Length > j; ++j)
            {
               // break up counter into pairs of 2 bits
               char index = (char)(counter >> (j << 1) & 3);

               char appendCandidate = PrintLetters.ConversionTable[inputNumbers[j], index];
               if (appendCandidate != ':')
               {
                  candidateBuilder[j] = appendCandidate;
               }
               else
               {
                  printResult = false;
                  break;
               }
            }

            if (printResult)
            {
               StringBuilder result = new StringBuilder(printPrefix);
               result.Append(candidateBuilder);
               Console.WriteLine(result);
            }
            else
            {
               printResult = true;
            }
         }
      }
   }
}
