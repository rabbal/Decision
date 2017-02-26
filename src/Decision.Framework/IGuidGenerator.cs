using System;
using System.Security.Cryptography;

namespace Decision.Framework
{
    public enum SequentialGuidDatabaseType
    {
        SqlServer,

        Oracle,

        MySql,

        PostgreSql,
    }
    public interface IGuidGenerator
    {
        Guid Create();
    }
    /// <summary>
    /// Implements <see cref="IGuidGenerator"/> by creating sequential Guids.
    /// This code is taken from https://github.com/jhtodd/SequentialGuid/blob/master/SequentialGuid/Classes/SequentialGuid.cs
    /// </summary>
    public class SequentialGuidGenerator : IGuidGenerator
    {
        /// <summary>
        /// Gets the singleton <see cref="SequentialGuidGenerator"/> instance.
        /// </summary>
        public static SequentialGuidGenerator Instance { get; } = new SequentialGuidGenerator();

        private static readonly RandomNumberGenerator Rng = RandomNumberGenerator.Create();

        public SequentialGuidDatabaseType DatabaseType { get; set; }

        private SequentialGuidGenerator()
        {
            DatabaseType = SequentialGuidDatabaseType.SqlServer;
        }

        public Guid Create()
        {
            return Create(DatabaseType);
        }

        public Guid Create(SequentialGuidDatabaseType databaseType)
        {
            switch (databaseType)
            {
                case SequentialGuidDatabaseType.SqlServer:
                    return Create(SequentialGuidType.SequentialAtEnd);
                case SequentialGuidDatabaseType.Oracle:
                    return Create(SequentialGuidType.SequentialAsBinary);
                case SequentialGuidDatabaseType.MySql:
                    return Create(SequentialGuidType.SequentialAsString);
                case SequentialGuidDatabaseType.PostgreSql:
                    return Create(SequentialGuidType.SequentialAsString);
                default:
                    throw new InvalidOperationException();
            }
        }

        public Guid Create(SequentialGuidType guidType)
        {
            var randomBytes = new byte[10];
            Rng.GetBytes(randomBytes);

            var timestamp = DateTime.UtcNow.Ticks / 10000L;
            var timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            var guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to reverse the order
                    // of the Data1 and Data2 blocks on little-endian systems.
                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }
                    break;

                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }

       

        /// <summary>
        /// Describes the type of a sequential GUID value.
        /// </summary>
        public enum SequentialGuidType
        {
            /// <summary>
            /// The GUID should be sequential when formatted using the
            /// <see cref="Guid.ToString()" /> method.
            /// </summary>
            SequentialAsString,

            /// <summary>
            /// The GUID should be sequential when formatted using the
            /// <see cref="Guid.ToByteArray" /> method.
            /// </summary>
            SequentialAsBinary,

            /// <summary>
            /// The sequential portion of the GUID should be located at the end
            /// of the Data4 block.
            /// </summary>
            SequentialAtEnd
        }
    }
}
