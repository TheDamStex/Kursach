using System.Linq;
using System.Text.RegularExpressions;
using System;

namespace Kursach.Validation
{
    public static class PaymentValidation
    {
        // === Перевірка номера картки ===
        // Форматування номера картки (додає пробіли між кожними 4 цифрами)
        public static string FormatCardNumber(string cardNumber)
        {
            var currentText = cardNumber.Replace(" ", ""); // Видаляємо пробіли з введеного тексту
            string formattedText = Regex.Replace(currentText, @"(\d{4})(?=\d)", "$1 "); // Форматуємо номер картки
            return formattedText;
        }

        // Перевірка, чи є номер картки дійсним
        public static bool IsValidCardNumber(string cardNumber)
        {
            var cardNumberWithoutSpaces = cardNumber?.Replace(" ", ""); // Видаляємо пробіли
            return !string.IsNullOrWhiteSpace(cardNumberWithoutSpaces) &&
                   cardNumberWithoutSpaces.Length == 16 && // Перевірка довжини (16 цифр)
                   cardNumberWithoutSpaces.All(char.IsDigit); // Перевірка, чи складається номер тільки з цифр
        }

        // Перевірка, чи дозволено вводити новий символ для номера картки
        public static bool IsCardNumberInputValid(string currentText, string inputText)
        {
            currentText = currentText.Replace(" ", ""); // Видаляємо пробіли з поточного тексту
            if (currentText.Length >= 16) // Якщо довжина номеру картки вже 16, не дозволяється вводити більше
            {
                return false;
            }
            return inputText.All(char.IsDigit); // Дозволено вводити тільки цифри
        }

        // === Перевірка дати закінчення терміну картки ===
        // Перевірка, чи є дата закінчення терміну дійсною
        public static bool IsValidExpirationDate(string expirationDate)
        {
            if (string.IsNullOrWhiteSpace(expirationDate) || expirationDate.Length != 5)
            {
                return false; // Перевірка довжини введеного рядка
            }

            var dateParts = expirationDate.Split('/'); // Розділяємо місяць і рік за допомогою '/'
            if (dateParts.Length != 2 || dateParts[0].Length != 2 || dateParts[1].Length != 2)
            {
                return false; // Перевірка формату місяця та року
            }

            if (!int.TryParse(dateParts[0], out int month) || !int.TryParse(dateParts[1], out int year))
            {
                return false; // Перевірка, чи є місяць і рік цілими числами
            }

            if (month < 1 || month > 12)
            {
                return false; // Перевірка, чи коректний місяць
            }

            var currentYear = DateTime.Now.Year % 100; // Поточний рік (останні дві цифри)
            return year >= currentYear; // Перевірка, чи не закінчився термін картки
        }

        // Перевірка, чи дозволено вводити новий символ для дати закінчення терміну
        public static bool IsExpirationDateInputValid(string currentText, string inputText)
        {
            if (currentText.Length >= 5) // Якщо довжина введеної дати вже 5 символів, не дозволяється вводити більше
            {
                return false;
            }
            return inputText.All(char.IsDigit) || inputText == "/"; // Дозволено вводити тільки цифри або '/'
        }

        // === Перевірка CVV картки ===
        // Перевірка, чи є CVV дійсним
        public static bool IsValidCVV(string cvv)
        {
            return !string.IsNullOrWhiteSpace(cvv) && cvv.Length == 3 && cvv.All(char.IsDigit); // Перевірка довжини та цифр
        }

        // Перевірка, чи дозволено вводити новий символ для CVV
        public static bool IsCVVInputValid(string currentText, string inputText)
        {
            if (currentText.Length >= 3) // Якщо довжина CVV вже 3, не дозволяється вводити більше
            {
                return false;
            }
            return inputText.All(char.IsDigit); // Дозволено вводити тільки цифри
        }
    }
}
