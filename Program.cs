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

                    Program p = new Program();                   
                    Console.WriteLine("Total Carb content of all these foods together is:" + p.calculateCarbs(foods)+"g");
                    Console.WriteLine("Total Protein content of all these foods together is:" + p.calculateProteins(foods) + "g");
                    Console.WriteLine("Total Fat content of all these foods together is:" + p.calculateFats(foods) + "g");
                }

            }

        }

        /** These methods are specifically written for illustrating tests **/

        public  async Task<List<Food>> getAllFoods()
        {
            List<Food> foods = new List<Food>();
            using (var client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync("https://food-rest-api-manasa-mannava.herokuapp.com/foods/");

                response.EnsureSuccessStatusCode();
                
                using (HttpContent content = response.Content)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseBody.Substring(0, 50) + "........");

                    foods = JsonConvert.DeserializeObject<List<Food>>(responseBody);                    
                }

            }
            return foods;
        }

        public int calculateCarbs(List<Food> foods)
        {
            int carbs_sum = 0;
            foreach (Food food in foods)
            {
                carbs_sum += food.carbs;
            }
            return carbs_sum;
        }

        public int calculateProteins(List<Food> foods)
        {
            int proteins_sum = 0;
            foreach (Food food in foods)
            {
                proteins_sum += food.proteins;
            }
            return proteins_sum;
        }

        public int calculateFats(List<Food> foods)
        {
            int fats_sum = 0;
            foreach (Food food in foods)
            {
                fats_sum += food.fats;
            }
            return fats_sum;
        }
    }
}
