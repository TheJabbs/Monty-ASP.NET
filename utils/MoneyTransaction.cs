using api.core.Dtos.Account;
using api.Models;

namespace api.utils;

public static class MoneyTransaction
{
    private static readonly Dictionary<(AccountCurrency From, AccountCurrency To), decimal> _conversionRates = new()
    {
        { (AccountCurrency.Eur, AccountCurrency.Usd), 1.10m },
        { (AccountCurrency.Usd, AccountCurrency.Ll), 90000m },
        { (AccountCurrency.Usd, AccountCurrency.Eur), Math.Round(1m / 1.10m, 6) },
        { (AccountCurrency.Ll, AccountCurrency.Usd), Math.Round(1m / 90000m, 6) },
        { (AccountCurrency.Eur, AccountCurrency.Ll), Math.Round(1.10m * 90000m, 6) },
        { (AccountCurrency.Ll, AccountCurrency.Eur), Math.Round(1m / (1.10m * 90000m), 9) },
    };
    
    public static decimal CalculateFeeInSenderCurrency(decimal amount, decimal feePercent = 0.02m)
    {
        return Math.Round(amount * feePercent, 3); 
    }

   
    public static decimal ConvertCurrency(AccountCurrency fromCurrency, AccountCurrency toCurrency, decimal amount)
    {
        if (fromCurrency == toCurrency)
            return amount;

        if (_conversionRates.TryGetValue((fromCurrency, toCurrency), out var rate))
        {
            return Math.Round(amount * rate, 3);
        }

        throw new NotSupportedException($"Unsupported currency conversion: {fromCurrency} → {toCurrency}");
    }
}
