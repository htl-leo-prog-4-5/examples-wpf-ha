namespace GradeCalc.Core.SuccessDetermination
{
    /// <summary>
    ///     An enum for different types of grade based success
    /// </summary>
    public enum SuccessType
    {
        Unknown = -10,
        Failed = 0,
        Passed = 10,
        WithSuccess = 20,
        WithDistinction = 30
    }
}