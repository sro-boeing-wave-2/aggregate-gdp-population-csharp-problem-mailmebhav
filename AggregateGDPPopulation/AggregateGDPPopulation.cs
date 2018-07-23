using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace AggregateGDPPopulation
{
    public class Class1
    {
        public static string Readfile(string filepath)
        {
            string data = File.ReadAllText(filepath);
            return data;
        }
        public static void Main()
        {
            string FilePath = @"..\..\..\..\AggregateGDPPopulation\data\datafile.csv";
            string jsonPath = @"..\..\..\..\AggregateGDPPopulation\data\continent.json";
            string Filedata = Readfile(FilePath);
            string json = Readfile(jsonPath);
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
            File.WriteAllText(@"..\..\..\..\AggregateGDPPopulation.Tests\output.json", result);
        }
    }
}
