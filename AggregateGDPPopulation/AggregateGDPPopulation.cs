using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AggregateGDPPopulation
{
    public class FileOperations
    {
        //Function to read file Asynchronously
        public static async Task<string> ReadfileAsync(string filepath)
        {
            string data;
            using (StreamReader fileRead = new StreamReader(filepath))
            {
                data = await fileRead.ReadToEndAsync();
            }
            return data;
        }
        //Function to write to File Asynchronously
        public static async void WriteFileAsync(string outputpath, string result)
        {
            using (StreamWriter fileWrite = new StreamWriter(outputpath))
            {
                await fileWrite.WriteAsync(result);
            }
        }
    }
    public class GDPAggregate
    {
        //Adding values of GDP and Population of a Country to the existing continent data in finaljson.
        public static void AddExistingContinentData(Dictionary<string, Dictionary<string, float>> finaljson, string continent, float gdp, float population)
        {
            finaljson[continent]["GDP_2012"] += gdp;
            finaljson[continent]["POPULATION_2012"] += population;
        }
        //Adding values of GDP and Population of a Country to the New continent data in finaljson.
        public static void AddNewContinentData(Dictionary<string, Dictionary<string, float>> finaljson, string continent, float gdp, float population)
        {
            Dictionary<string, float> CountryDetails = new Dictionary<string, float>();
            CountryDetails.Add("GDP_2012", gdp);
            CountryDetails.Add("POPULATION_2012", population);
            finaljson.Add(continent, CountryDetails);
        }
        //Main Function for the Program
        public static async Task Main()
        {
            string InputFilePath = @"../../../../AggregateGDPPopulation/data/datafile.csv";
            string CountryContinentMapperPath = @"../../../../AggregateGDPPopulation/data/continent.json";
            string outputPath = @"../../../../AggregateGDPPopulation.Tests/output.json";
            Task<string> ReadInputFileTask = FileOperations.ReadfileAsync(InputFilePath);
            Task<string> ReadMapperTask = FileOperations.ReadfileAsync(CountryContinentMapperPath);
            string Mapper = await ReadMapperTask;
            Dictionary<string, string> MapperData = JsonConvert.DeserializeObject<Dictionary<string, string>>(Mapper);      //  Converting the json string to Dictionary. 
            string InputFiledata = await ReadInputFileTask;
            var dataarray = InputFiledata.Replace("\"", "").Split('\n');                                                     //  Removing " from the datafile read, and splitting the string to array by \n character.
            Dictionary<string, Dictionary<string, float>> Resultjson = new Dictionary<string, Dictionary<string, float>>();
            for (int i = 1; i < dataarray.Length; i++)
            {
                float gdpValue = float.Parse(dataarray[i].Split(',')[10]);
                float populationValue = float.Parse(dataarray[i].Split(',')[4]);
                string country = dataarray[i].Split(',')[0];
                if (country == "European Union")
                {
                    break;
                }
                string continent = MapperData[country];
                if (Resultjson.ContainsKey(continent))
                {
                    AddExistingContinentData(Resultjson, continent, gdpValue, populationValue);
                }
                else
                {
                    AddNewContinentData(Resultjson, continent, gdpValue, populationValue);
                }
            }
            string resultString = JsonConvert.SerializeObject(Resultjson, Formatting.Indented);
            FileOperations.WriteFileAsync(outputPath, resultString);
        }
    }
}
