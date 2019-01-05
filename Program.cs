using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Foods
{
    public class Program
    {
        public class Food
        {
            public int id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public int carbs { get; set; }
            public int proteins { get; set; }
            public int fats { get; set; }
        }

        public static void Main(string[] args)
        {
            Task T = new Task(FoodsCall);
            T.Start();
            Console.WriteLine("Data from food-rest-api ...");
            Console.ReadLine();
        }

        static async void FoodsCall()
        {

            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync("https://food-rest-api-manasa-mannava.herokuapp.com/foods/");

                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseBody.Substring(0, 50) + "........");

                    List<Food> foods = JsonConvert.DeserializeObject<List<Food>>(responseBody);

                    foreach (Food food in foods)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", food.id, food.name, food.carbs, food.proteins, food.fats, food.description);
                    }

                }

            }

        }
    }
}
