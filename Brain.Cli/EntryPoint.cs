using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Brain.Cli.Dtos;
using Brain.Cli.Services;

namespace Brain.Cli
{
    public class EntryPoint
    {
        private readonly HttpClient _client;
        
        public async void Run(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0].ToLower() == "login")
                {
                    Console.WriteLine("Login attempt");
                }
               
            }

        }
    }
}