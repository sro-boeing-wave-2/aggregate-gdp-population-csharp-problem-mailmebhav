using System;
using System.IO;
using Xunit;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            await Class1.Main();
            string expectedOutputPath = @"../../../../AggregateGDPPopulation.Tests/expected-output.json";
            string outputPath = @"../../../../AggregateGDPPopulation.Tests/output.json";
            string expectedResult = await Class1.ReadfileAsync(expectedOutputPath);
            string output = await Class1.ReadfileAsync(outputPath);
            Assert.Equal(expectedResult, output);
        }
    }
}
