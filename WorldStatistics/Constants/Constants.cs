using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldStatistics
{
    public static class Constants
    {
        public static class RequestQueryParam
        {
            public const string Format = "format=";
            public const string Date = "date=";
            public const string Page = "page=";
        }

        public static class Format
        {
            public const string Json = "json";
            public const string Xml = "xml";
        }
        public static class RequestPath
        {
            public const string Country = "/country/";
            public const string Indicator = "/indicator";
        }

        public static class Indicators
        {
            public const string GDP = "/NY.GDP.MKTP.CD";
        }
    }
}
