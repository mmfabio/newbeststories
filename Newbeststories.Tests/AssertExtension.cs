using System;
using Xunit;

namespace Newbeststories.Tests
{
    public static class AssertExtension
    {
        public static void WithMessage(this ArgumentException exception, string message)
        {
            if(exception.Message.Equals(message))
                Assert.True(true);
            else
                Assert.False(true);
        }
    }
}