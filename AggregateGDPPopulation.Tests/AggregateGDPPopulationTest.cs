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
            string expectedOutputPath = @"../../../../AggregateGDPPopulation.Tests/expected-output.json";
            string outputPath = @"../../../../AggregateGDPPopulation.Tests/output.json";
            string expectedResult = await FileOperations.ReadfileAsync(expectedOutputPath);
            string output = await FileOperations.ReadfileAsync(outputPath);
            await GDPAggregate.Main();
            Assert.True(GDPAggregateTest.AggregatedGDP(expectedResult, output)); //AggregatedGDP function is used to match the values of expected output and original output and returns boolean value if test fails or passes.
        }
    }
}
