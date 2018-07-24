using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AggregateGDPPopulation
{
    public class Class1
    {
        public static async Task<string> ReadfileAsync(string filepath)
        {
            string data;
            using (StreamReader fileRead = new StreamReader(filepath))
            {
                data = await fileRead.ReadToEndAsync();
            }
            return data;
        }
        public static async void WriteFileAsync(string outputpath, string result)
        {
            using (StreamWriter fileWrite = new StreamWriter(outputpath))
            {
                await fileWrite.WriteAsync(result);
            }
        }
        public static async Task Main()
        {
            string FilePath = @"../../../../AggregateGDPPopulation/data/datafile.csv";
            string jsonPath = @"../../../../AggregateGDPPopulation/data/continent.json";
            string outputPath = @"../../../../AggregateGDPPopulation.Tests/output.json";
            Task<string> Filedatatask = ReadfileAsync(FilePath);
            Task<string> jsontask = ReadfileAsync(jsonPath);
            string json = await jsontask;
            string Filedata = await Filedatatask;
            Dictionary<string, string> jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var dataarray = Filedata.Replace("\"", "").Split('\n');
            Dictionary<string, Dictionary<string, float>> finaljson = new Dictionary<string, Dictionary<string, float>>();
            for (int i = 1; i < dataarray.Length; i++)
            {
                float gdp = float.Parse(dataarray[i].Split(',')[10]);
                float population = float.Parse(dataarray[i].Split(',')[4]);
                string country = dataarray[i].Split(',')[0];
                if(country == "European Union")
                {
                    break;
                }
                string continent = jsonData[country];
                Dictionary<string, float> CountryDetails = new Dictionary<string, float>();
                CountryDetails.Add("GDP_2012", gdp);
                CountryDetails.Add("POPULATION_2012", population);
                if (finaljson.ContainsKey(continent))
                {
                    finaljson[continent]["GDP_2012"] += gdp;
                    finaljson[continent]["POPULATION_2012"] += population;
                }
                else
                {
                    finaljson.Add(continent, CountryDetails);
                }
            }
            string result = JsonConvert.SerializeObject(finaljson, Formatting.Indented);
            WriteFileAsync(outputPath, result);
        }
    }
}
