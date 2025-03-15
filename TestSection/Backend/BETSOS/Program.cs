using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace BETSOS
{
    public struct Struct1DTO
    {
        public List<string> SList { get; set; }
        public int Num { get; set; }
        public List<Dictionary<string, Struct2>> StruDicList { get; set; }
    }

    public struct Struct2
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    class Program
    {
        static void Main()
        {
            string filePath = "data.json";

            // Сериализация
            SerializeToJson(filePath);

            // Десериализация
            DeserializeFromJson(filePath);
        }

        static void SerializeToJson(string filePath)
        {
            var myStruct = new Struct1DTO
            {
                SList = new List<string> { "Аня", "Стёпа", "Глеб", "Игорь" },
                Num = 4,
                StruDicList = new List<Dictionary<string, Struct2>>
                {
                    new Dictionary<string, Struct2>
                    {
                        { "Раб", new Struct2 { Name = "Нерг", Value = 0.5 } }
                    },
                    new Dictionary<string, Struct2>
                    {
                        { "Правитель", new Struct2 { Name = "Король", Value = 66.6 } }
                    }
                }
            };

            string json = JsonConvert.SerializeObject(myStruct, Formatting.Indented);
            File.WriteAllText(filePath, json);

            Console.WriteLine("Объект сериализован и сохранён в data.json!");
        }

        static void DeserializeFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            string json = File.ReadAllText(filePath);
            Struct1DTO myStruct = JsonConvert.DeserializeObject<Struct1DTO>(json);

            Console.WriteLine("\nДесериализованный объект:");
            Console.WriteLine($"Number: {myStruct.Num}");

            Console.WriteLine("StringList:");
            foreach (var str in myStruct.SList)
                Console.WriteLine($" - {str}");

            Console.WriteLine("StructDictionaryList:");
            foreach (var dict in myStruct.StruDicList)
            {
                foreach (var kvp in dict)
                {
                    Console.WriteLine($" - Key: {kvp.Key}, Name: {kvp.Value.Name}, Value: {kvp.Value.Value}");
                }
            }
        }
    }
}
