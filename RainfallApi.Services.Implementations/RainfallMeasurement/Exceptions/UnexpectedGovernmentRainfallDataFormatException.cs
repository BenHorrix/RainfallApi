using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainfallApi.Services.Implementations.RainfallMeasurement.Exceptions
{
    public class UnexpectedGovernmentRainfallDataFormatException : System.Exception
    {
        public UnexpectedGovernmentRainfallDataFormatException(string responseBody, Type deserializeType) : base($"The following response body could not be deserialized to {deserializeType.Name}: {responseBody})")
        {

        }
    }
}
