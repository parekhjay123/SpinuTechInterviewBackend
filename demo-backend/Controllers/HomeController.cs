using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace demo_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        // Convert whole number to words
        
        private string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[]
                {
                "zero", "one", "two", "three", "four", "five", "six", "seven",
                "eight", "nine", "ten", "eleven", "twelve", "thirteen",
                "fourteen", "fifteen", "sixteen", "seventeen", "eighteen",
                "nineteen"
            };
                var tensMap = new[]
                {
                "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty",
                "seventy", "eighty", "ninety"
            };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        // Convert decimal number to currency words
        [HttpGet]
        [Route("exercise1/{amount}")]
        public string ConvertToCurrencyWords(decimal amount)
        {
            // Split the amount into dollars and cents
            int dollars = (int)amount;
            int cents = (int)((amount - dollars) * 100);

            string dollarWords = NumberToWords(dollars);
            string centWords = cents.ToString("D2"); // Format cents with 2 digits (e.g., 04)

            return $"{dollarWords} and {centWords}/100 dollars";
        }
        [HttpGet]
        [Route("exercise6")]
        public string IsPalindrome(int number)
        {
            // Store the original number to compare later
            int originalNumber = number;
            int reversedNumber = 0;

            // Reverse the number
            while (number > 0)
            {
                int remainder = number % 10;
                reversedNumber = (reversedNumber * 10) + remainder;
                number /= 10;
            }

            // Compare the reversed number with the original
            if(originalNumber == reversedNumber)
            {
                return originalNumber + " is palindrome number.";
            }
            else
            {
                return originalNumber + " is not palindrome number.";
            }
        }
        [HttpGet]
        [Route("exercise3/{n}")]
        public IActionResult PrintSpiral(int n)
        {
            int size = (int)Math.Ceiling(Math.Sqrt(n + 1));
            int[,] matrix = new int[size, size];

            int left = 0, right = size - 1;
            int top = 0, bottom = size - 1;
            int currentValue = n;

            // Fill the matrix in a spiral order
            while (left <= right && top <= bottom)
            {
                // Fill top row (left to right)
                for (int i = left; i <= right && currentValue >= 0; i++)
                    matrix[top, i] = currentValue--;
                top++;

                // Fill right column (top to bottom)
                for (int i = top; i <= bottom && currentValue >= 0; i++)
                    matrix[i, right] = currentValue--;
                right--;

                // Fill bottom row (right to left)
                for (int i = right; i >= left && currentValue >= 0; i--)
                    matrix[bottom, i] = currentValue--;
                bottom--;

                // Fill left column (bottom to top)
                for (int i = bottom; i >= top && currentValue >= 0; i--)
                    matrix[i, left] = currentValue--;
                left++;
            }

            var result = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                var row = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    row.Add(matrix[i, j]);
                }
                result.Add(row);
            }

            return Ok(result);
        }
    }
}
