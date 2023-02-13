using System.Drawing;
using System.Text.Json;
using Weather_Report.Models;
using static System.Net.WebRequestMethods;

internal class Program
{
    private static async Task Main(string[] args)

    {   
        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
       
        Console.Write("Добро пожаловать в ваш погодный помощник Стрибог ");
        Console.WriteLine(" ");
        Console.WriteLine("Введите название пункта: ");

        string townName = Console.ReadLine();
        Console.Clear();


        string userKey = "9a89c7bdc181fb5f0d16eddaa39a37bf";

        HttpClient client = new HttpClient();

        // https://api.openweathermap.org/data/2.5/forecast?q=Москва&appid=9a89c7bdc181fb5f0d16eddaa39a37bf&lang=ru&units=metric
        HttpResponseMessage response = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=" + townName + "&appid=" + userKey + "&lang=ru&units=metric");
        
        string json;
       
        //Условия ввода города

        if (response.IsSuccessStatusCode)
        {
            json = await response.Content.ReadAsStringAsync();

            //Погода
            WeatherReport weather = JsonSerializer.Deserialize<WeatherReport>(json);

            int[] column = { 0, 40, 80, 110 };

            ShowData(column[0], 1, "Город", townName);

            DrawLine(column[0], 2, column[3]);

            ShowData(column[0], 4, "Текущая температура", weather.main.temp.ToString());

            ShowData(column[1], 4, "Ощущается как", weather.main.feels_like.ToString());

            ShowData(column[2], 4, "Влажность", weather.main.humidity.ToString());

            ShowData(column[0], 6, "Описание", weather.weather[0].description.ToString());

            ShowData(column[1], 6, "Скорость ветра", weather.wind.speed.ToString());

            ShowData(column[2], 6, "Направление", GetDirectByDeg(weather.wind.deg));

            ShowData(column[2], 8, "Давление рт.ст", weather.main.pressure.ToString());

            DrawLine(column[0], 10, column[3]);

            response = await client.GetAsync("https://api.openweathermap.org/data/2.5/forecast?q=" + townName + "&appid=" + userKey + "&lang=ru&units=metric");

            if (response.IsSuccessStatusCode)
            {
                json = await response.Content.ReadAsStringAsync();
                WeatherFuture weatherFuture = JsonSerializer.Deserialize<WeatherFuture>(json);

                for (int i = 0; i < 4; i++)
                {
                    ShowData(column[0], 12 + i * 2, $"{weatherFuture.list[i * 9].dt_txt.ToString().Remove(10, 9)}",
                        weatherFuture.list[i * 8].weather[0].description.ToString());
                    ShowData(column[1], 12 + i * 2, "Ветер (мин/макс)",
                        $"{weatherFuture.list[i * 8].main.temp_min.ToString()} / {weatherFuture.list[i * 8].main.temp_max.ToString()}");
                }
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("Рады, что пользуетесь нашим приложением.");


            }
            else
            {
                Console.WriteLine("К сожалению, обнаружена ошибка");
                Console.WriteLine("Благодарим вас за понимание.");
            }
        }
        else
        {
            Console.WriteLine("К сожалению, обнаружена ошибка, проверьте как вы ввели город.");
            Console.WriteLine("Благодарим вас за понимание.");
        }

        Console.ReadLine();
    }

    static void ShowData(int x, int y, string label, string value)
    {
        Console.SetCursorPosition(x, y);
        Console.Write($"{label}: {value}");
    }


    //Направление Ветра
    static string GetDirectByDeg (int deg)
    {
        string[] directions = { "С", "СЗ", "З", "ЮЗ", "Ю", "ЮВ", "В", "СВ" , "С", "-"};
        double totalDeg = 360 - 22.5 - deg;
        int i = 0;

        while((totalDeg > 0) && (i < 9))
        {
            i++;
            totalDeg -= 45;
        }

        return directions[i];
    }
    //Нарисовать Линию
    static void DrawLine(int x, int y, int length)
    {
        for (int i = 0; i < length; i++)
        {
            Console.SetCursorPosition(x + i, y);
            Console.Write("-");
        }
    }
}