using System;

namespace Decision.Common.OpenGraph.Structs
{
    /// <summary>
    /// Represents a currency type and amount.
    /// </summary>
    public class OpenGraphCurrency
    {
        private readonly double _amount;
        private readonly string _currency;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphCurrency"/> class.
        /// </summary>
        /// <param name="amount">The actual currency amount.</param>
        /// <param name="currency">The currency type.</param>
        /// <exception cref="System.ArgumentNullException">currency is <c>null</c>.</exception>
        public OpenGraphCurrency(double amount, string currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException("currency");
            }

            this._amount = amount;
            this._currency = currency;
        }

        /// <summary>
        /// Gets the actual currency amount.
        /// </summary>
        public double Amount { get { return this._amount; } }

        /// <summary>
        /// Gets the currency type.
        /// </summary>
        public string Currency { get { return this._currency; } }
    }
}
