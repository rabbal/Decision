using System;
using System.Collections.Generic;

namespace Decision.Common.Utility
{
    /// <summary>
    ///     This class is used to compare any
    ///     type(property) of a class for sorting.
    ///     This class automatically fetches the
    ///     type of the property and compares.
    /// </summary>
    public sealed class GenericComparer<T> : IComparer<T>
    {
        #region Enums

        /// <summary>
        ///     The sort order direction for sorting the collection
        /// </summary>
        public enum SortOrder
        {
            /// <summary>
            ///     Ascending
            /// </summary>
            Ascending,

            /// <summary>
            ///     Descending
            /// </summary>
            Descending
        }

        #endregion

        #region Ctor

        /// <summary>
        ///     Creates a new instance of the GenericComparer class
        /// </summary>
        /// <param name="sortColumn">The property on which the collection should be sorted</param>
        /// <param name="sortingOrder">The direction of the sort</param>
        public GenericComparer(string sortColumn, SortOrder sortingOrder)
        {
            SortColumn = sortColumn;
            SortingOrder = sortingOrder;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Compare interface implementation
        /// </summary>
        /// <param name="x">custom Object</param>
        /// <param name="y">custom Object</param>
        /// <returns>int</returns>
        public int Compare(T x, T y)
        {
            var propertyInfo = typeof(T).GetProperty(SortColumn);
            var obj1 = (IComparable) propertyInfo.GetValue(x, null);
            var obj2 = (IComparable) propertyInfo.GetValue(y, null);
            if (SortingOrder == SortOrder.Ascending)
            {
                return obj1.CompareTo(obj2);
            }
            return obj2.CompareTo(obj1);
        }

        #endregion

        #region Fields

        #endregion

        #region Properties

        /// <summary>
        ///     Column Name(public property of the class) to be sorted.
        /// </summary>
        public string SortColumn { get; }

        /// <summary>
        ///     Sorting order.
        /// </summary>
        public SortOrder SortingOrder { get; }

        #endregion
    }
}