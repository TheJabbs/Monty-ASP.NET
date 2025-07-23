namespace api.Exceptions;

public class IbanGenerate
{
    public static string GenerateIban(string country)
    { var random = new Random();
        string branchCode = random.Next(1000, 9999).ToString();
        string accountNumber = random.Next(1000000000, int.MaxValue).ToString().Substring(0, 10);

        return $"{country.ToUpper()}{branchCode}{accountNumber}";
    }
}