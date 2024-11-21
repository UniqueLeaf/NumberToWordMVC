using Microsoft.AspNetCore.Mvc;

namespace NumberToWordsApp.Controllers
{
    public class NumberController : Controller
    {
        /*
        * Add attempt to parse the input string as it's decimal type
        * If false, return "Invalid input. Please enter a valid number."
        * Else return the number in decimal place and sent it to ConvertNumberToWords
        */
        public string ConvertNumberToWords(string number)
        {
            if (!decimal.TryParse(number, out decimal parsedNumber))
            {
                return "Invalid input. Please enter a valid number.";
            }
            return ConvertNumberToWords(parsedNumber);
        }

        /*
        * Initialize an empty string to hold the results
        * Split the number into whole and fractional parts
        * Convert the whole part to words, followed by "dollars"
        * If there is a fractional part, convert it to words followed by "cents"
        * Captialize the result and return it
        */
        public string ConvertNumberToWords(decimal number)
        {
            string words = "";

            int wholePart = (int)number;
            int fractionalPart = (int)((number - wholePart) * 100);

            words += $"{NumberToWords(wholePart)} DOLLARS";

            if (fractionalPart > 0)
            {
                words += $" AND {NumberToWords(fractionalPart)} CENTS";
            }

             return words.ToUpper();
        }

        /*
        * Check if the number is zero, return "zero dollars" if true
        * If the number is negative, convert it to positive and prepend "negative"
        * Define the mappings for ones, teens, and tens in arrays
        * Initialize an empty string to hold the result
        * Handle billions: Check if the number is in the billions, then convert and add "billion"
        * Handle millions: Check if the number is in the millions, then convert and add "million"
        * Handle thousands: Check if the number is in the thousands, then convert and add "thousand"
        * Handle hundreds: Check if the number is in the hundreds, then convert and add "hundred"
        * Handle tens and ones: Convert the remaining number to words using the ones, teens, or tens mapping
        * Return the result and ensure proper formatting
        */
        private string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "negative " + NumberToWords(Math.Abs(number));

            string[] onesMap = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] teensMap = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tensMap = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            string words = "";

            // Handle billions
            if ((number / 1000000000) > 0)
            {
                words += NumberToWords(number / 1000000000) + " billion ";
                number %= 1000000000;
            }

            // Handle millions
            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            // Handle thousands
            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            // Handle hundreds
            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            // Handle tens and ones
            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                if (number < 10)
                    words += onesMap[number];
                else if (number < 20)
                    words += teensMap[number - 10];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + onesMap[number % 10];
                }
            }

            return words.Trim();
        }

        /*
        * Attempt to parse the input string as it's decimal type
        * If successful, convert the parsed number into words
        * Display result in uppercase
        * If the input is invalid, show an error message
        */
        [HttpPost]
        public IActionResult ConvertToWords(string number)
        {
            if (decimal.TryParse(number, out decimal parsedNumber))
            {
                // Convert the number to words
                string result = ConvertNumberToWords(parsedNumber);
                ViewBag.Result = result.ToUpper();
            }
            else
            {
                ViewBag.Result = "Invalid input. Please enter a valid number.";
            }

            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
