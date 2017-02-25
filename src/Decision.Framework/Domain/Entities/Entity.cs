using System;
using System.Collections.Generic;

namespace Decision.Framework.Domain.Entities
{
    public abstract class Entity : Entity<long>, IEntity
    {
    }

    public abstract class Entity<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Properties
        public TKey Id { get; set; }
        public byte[] RowVersion { get; set; }

        #endregion

        #region Public Methods
        public bool IsTransient()
        {
            if (EqualityComparer<TKey>.Default.Equals(Id, default(TKey)))
            {
                return true;
            }

            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(Id) <= 0;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<TKey>;
            if (other == null) return false;

            if (IsTransient() && other.IsTransient())
                return false;

            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfOther.IsAssignableFrom(typeOfThis) && !typeOfThis.IsAssignableFrom(typeOfOther))
                return false;

            return Id.Equals(other.Id);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return $"[{GetType().Name} {Id}]";
        }
        #endregion

        #region Operators
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            return Equals(left, null) ? Equals(right, null) : left.Equals(right);
        }
        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
        #endregion
    }
}