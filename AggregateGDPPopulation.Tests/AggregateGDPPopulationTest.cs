using System;
using System.IO;
using Xunit;

namespace AggregateGDPPopulation.Tests
{
    public class AggregateGDPPopulationTest
    {
        [Fact]
        public async void GDPPopulationbyContinentTest()
        {
            await GDPAggregate.Main();
            string expectedOutputPath = @"../../../../AggregateGDPPopulation.Tests/expected-output.json";
            string outputPath = @"../../../../AggregateGDPPopulation.Tests/output.json";
            string expectedResult = await GDPAggregate.ReadfileAsync(expectedOutputPath);
            string output = await GDPAggregate.ReadfileAsync(outputPath);
            Assert.True(GDPAggregate.AggregatedGDP(expectedResult, output)); //AggregatedGDP function is used to match the values of expected output and original output and returns boolean value if test fails or passes.
        }
    }
}
