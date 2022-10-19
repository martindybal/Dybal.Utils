using System;
using System.Diagnostics.CodeAnalysis;

namespace Dybal.Utils.Guards
{
    internal static class ThrowHelper
    {
        [DoesNotReturn]
        public static void ThrowArgumentNullException(string paramName, string? message = null)
        {
            if (message is null)
            {
                throw new ArgumentNullException(paramName);
            }
            throw new ArgumentNullException(paramName, message);
        }
        
        [DoesNotReturn]
        public static void ThrowArgumentException(string message, string paramName)
        {
            throw new ArgumentException(message, paramName);
        }
    }
}
