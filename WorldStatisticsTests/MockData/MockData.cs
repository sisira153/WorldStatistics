using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WorldStatisticsTests.MockData
{
    public static class MockData
    {
        public static string GetMockData(string dataType)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Split(new[] { "bin" }, StringSplitOptions.None)[0];
            path = path + @"MockData\MockData.json";

            var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(path));
            return data[dataType];
        }
    }
}
