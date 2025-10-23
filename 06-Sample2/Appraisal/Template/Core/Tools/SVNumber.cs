
namespace Core.Tools;

public class SVNumber
{
    static int CharToDigit(char ch)
    {
        return ch - '0';
    }

    public static bool IsSvNumberValid(string svNumber)
    {
        int[] weight = { 3, 7, 9, 0, 5, 8, 4, 2, 1, 6 };

        //TODO Implement SVNumber check

        throw new NotImplementedException();

        // 1.) length must be 10
        // 2.) only contains digits
        // 3.) sum of digit (with weight) => modulo % 11 must be 4. digit (svNumber[3])
    }
}