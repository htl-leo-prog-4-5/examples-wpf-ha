
namespace Core.Tools;

public class SVNumber
{
    static bool OnlyContainsDigits(string str)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] < '0' || str[i] > '9')
            {
                return false;
            }
        }

        return true;
    }

    static int CharToDigit(char ch)
    {
        return ch - '0';
    }

    public static bool IsSvNumberValid(string svNumber)
    {
        int[] weight = { 3, 7, 9, 0, 5, 8, 4, 2, 1, 6 };

        bool isSvOk = svNumber.Length == 10 && OnlyContainsDigits(svNumber);

        if (isSvOk)
        {
            int sum = 0;
            for (int i = 0; i < svNumber.Length; i++)
            {
                sum += weight[i] * CharToDigit(svNumber[i]);
            }

            isSvOk = CharToDigit(svNumber[3]) == sum % 11;
        }

        return isSvOk;
    }
}