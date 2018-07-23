using System;
using System.IO;
using Xunit;

namespace AggregateGDPPopulation.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Class1.Main();
            string expectedResult = File.ReadAllText(@"E:\Workspace\aggregate-gdp-population-csharp-problem-mailmebhav\AggregateGDPPopulation.Tests\expected-output.json");
            string output = File.ReadAllText(@"E:\Workspace\aggregate-gdp-population-csharp-problem-mailmebhav\AggregateGDPPopulation.Tests\output.json");
            Assert.Equal(expectedResult, output);
        }
    }
}
