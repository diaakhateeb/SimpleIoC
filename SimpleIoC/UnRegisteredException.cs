using System;

namespace SimpleIoC
{
    public class UnRegisteredException : Exception
    {
        public UnRegisteredException(string message)
            : base(message)
        {
        }
    }
}
