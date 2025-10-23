namespace Core.Validations;

public class CreditCardNumberValidation
{
    public static bool IsCreditCardValid(string creditCardNumber)
    {
        string trimmedNumber = creditCardNumber.Replace("-", "");
        if (trimmedNumber.Any(c => !char.IsDigit(c)))
        {
            return false;
        }

        var checkDigit = trimmedNumber[trimmedNumber.Length - 1] - '0';
        var payloadSum = trimmedNumber
            .Substring(0, trimmedNumber.Length - 1)
            .Reverse()
            .Select(c => c - '0')
            .Select((n,         i) => i % 2 == 0 ? DigitSum(n * 2) : n)
            .Aggregate(0, (sum, n) => sum += n);
        int calcCheckDigit = CalculateCheckDigit(payloadSum);
        return checkDigit == calcCheckDigit;
    }

    public static int CalculateCheckDigit(int sum)
    {
        int chkDigit = sum % 10;
        if (chkDigit != 0) // 0 bleibt erhalten
            chkDigit = 10 - chkDigit;
        return chkDigit;
    }

    private static int DigitSum(int number)
    {
        return number >= 10 ? number - 9 : number;
    }
}