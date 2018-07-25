using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AggregateGDPPopulation.Tests
{
    public class GDPAggregateTest
    {
        //This function is used for testing to check the values of gdp and population by continent.
        public static bool AggregatedGDP(string expectedOutput, string originalOutput)
        {
            bool errorMatching = false;
            Dictionary<string, Dictionary<string, float>> expectedOutputDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, float>>>(expectedOutput);
            Dictionary<string, Dictionary<string, float>> OriginalOutputDictionary = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, float>>>(originalOutput);
            foreach (string continent in expectedOutputDictionary.Keys)
            {
                if (expectedOutputDictionary[continent]["GDP_2012"] == OriginalOutputDictionary[continent]["GDP_2012"])
                {
                    if (expectedOutputDictionary[continent]["POPULATION_2012"] != expectedOutputDictionary[continent]["POPULATION_2012"])
                    {
                        errorMatching = true;
                        break;
                    }
                }
                else
                {
                    errorMatching = true;
                    break;
                }
            }
            if (errorMatching)
            {
                return false;
            }
            return true;
        }
    }
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
