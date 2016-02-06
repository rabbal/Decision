using System;

namespace Decision.Common.OpenGraph.Structs
{
    /// <summary>
    /// Represents the name and price of a menu item variation.
    /// </summary>
    public class OpenGraphMenuItemVariation
    {
        private readonly string _name;
        private readonly OpenGraphCurrency _price;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphMenuItemVariation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="price">The price.</param>
        /// <exception cref="System.ArgumentNullException">name or price is <c>null</c>.</exception>
        public OpenGraphMenuItemVariation(string name, OpenGraphCurrency price)
        {
            if (name == null) { throw new ArgumentNullException("name"); }
            if (price == null) { throw new ArgumentNullException("price"); }

            this._name = name;
            this._price = price;
        }

        /// <summary>
        /// Gets the name of the menu item variation.
        /// </summary>
        public string Name { get { return this._name; } }

        /// <summary>
        /// Gets the price of the menu item variation.
        /// </summary>
        public OpenGraphCurrency Price { get { return this._price; } }
    }
}
