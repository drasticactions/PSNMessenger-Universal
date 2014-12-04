using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayStationApp.Core.Exceptions
{
    public class RefreshTokenException : Exception
    {
        public RefreshTokenException()
        {
        }

        public RefreshTokenException(string message)
            : base(message)
        {
        }
    }
}
