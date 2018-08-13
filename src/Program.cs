using CommandLine;
using System;
using System.Net.Http;

namespace Webcaller
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandLine
                .Parser
                .Default
                .ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunWebCall(opts));
            //.WithNotParsed<Options>((errs) => HandleParseError(errs));
        }

        private static void RunWebCall(Options opts)
        {
            try
            {
                switch (opts.Method.ToUpper())
                {
                    case "GET":
                        Console.WriteLine(
                        client
                            .GetAsync(opts.url)
                            .Result
                            .Content
                            .ReadAsStringAsync()
                            .Result);
                        break;
                    case "PUT":
                        Console.WriteLine(
                        client
                            .PutAsync(opts.url, opts.Content)
                            .Result
                            .Content
                            .ReadAsStringAsync()
                            .Result);
                        break;
                    case "POST":
                        Console.WriteLine(
                        client
                            .PostAsync(opts.url, opts.Content)
                            .Result
                            .Content
                            .ReadAsStringAsync()
                            .Result);
                        break;
                    default:
                        Console.WriteLine("Missing METHOD: " + opts.Method);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static readonly HttpClient client = new HttpClient();

        class Options
        {
            [Option(Required = true, HelpText = "URL to the web api.")]
            public string url { get; set; }

            [Option("Method", Required = false, Default = "GET", HelpText = "Set the method type.")]
            public string Method { get; set; }

            [Option("Content", Required = false, Default = null, HelpText = "Set key value content to be uses on POST and PUT methods.")]
            public HttpContent Content { get; set; }
        }
    }
}
