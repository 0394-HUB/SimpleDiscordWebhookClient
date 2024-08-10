using Discord.Webhook;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SimpleDiscordWebhookClient
{
    internal class Program
    {
        public static DiscordWebhookClient client;

        static async Task Main(string[] args)
        {
            string header = " ____  _  ____  ____  ____  ____  ____    _      _____ _     ____  ____  _  __\r\n/  _ \\/ \\/ ___\\/   _\\/  _ \\/  __\\/  _ \\  / \\  /|/  __// \\ /|/  _ \\/  _ \\/ |/ /\r\n| | \\|| ||    \\|  /  | / \\||  \\/|| | \\|  | |  |||  \\  | |_||| / \\|| / \\||   / \r\n| |_/|| |\\___ ||  \\_ | \\_/||    /| |_/|  | |/\\|||  /_ | | ||| \\_/|| \\_/||   \\ \r\n\\____/\\_/\\____/\\____/\\____/\\_/\\_\\\\____/  \\_/  \\|\\____\\\\_/ \\|\\____/\\____/\\_|\\_\\\r\n                                                                              ";
            Console.WriteLine(header);
            Console.WriteLine("by c0d3x94");
            string configPath = @"config.json";
            if (!File.Exists(configPath))
            {
                var defaultConfig = new
                {
                    WebhookUrl = "your token here",
                    MessageText = "Hello, World!",
                    Username = "MyBot",
                    AvatarUrl = "https://example.com/avatar.png"
                };
                File.WriteAllText(configPath, JsonConvert.SerializeObject(defaultConfig, Formatting.Indented));
                Console.WriteLine("Config file has been created. Please insert your webhook URL and restart the program.");
                Console.ReadLine();
                return;
            }

            dynamic config = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(configPath));
            if (config.WebhookUrl == "your token here")
            {
                Console.WriteLine("Please insert your webhook URL in the config.json and restart the program.");
                Console.ReadLine();
                return;
            }
            var client = new DiscordWebhookClient((string)config.WebhookUrl);

            client.SendMessageAsync(
                (string)config.MessageText,
                false,
                null,
                (string)config.Username,
                (string)config.AvatarUrl,
                null,
                null
            ).Wait();

            Console.WriteLine("Message was sent successfully.");
            Console.ReadLine();
        }
    }
}
