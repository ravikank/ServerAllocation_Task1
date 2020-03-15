using ServerAllocation.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerAllocatorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Server> servers = GetInitialInput();

            Console.WriteLine("Option 1 to Update CPU Usage");
            Console.WriteLine("Option 2 to get server");

            do
            {
                Console.Write("Enter Option: ");

                bool parsed = int.TryParse(Console.ReadLine(), result: out int selectedOption);

                if (!parsed || selectedOption <= 0 || selectedOption > 2)
                {
                    Console.Write("Enter Valid Option: ");
                }

                switch (selectedOption)
                {
                    case 1:
                        {
                            UpdateCPUUsage(servers);
                        }
                        break;
                    case 2:
                        {
                            AllocateServer(servers);
                        }
                        break;
                    default:
                        break;
                }
            } while (true);
        }

        private static List<Server> GetInitialInput()
        {
            return new List<Server>
            {
                new Server
                {
                    Id = 0,
                    Weight = 100,
                    Threshold = 80
                },
                new Server
                {
                    Id = 1,
                    Weight = 50,
                    Threshold = 90
                },
                new Server
                {
                    Id = 2,
                    Weight = 25,
                    Threshold = 25
                }
            };
        }

        private static void UpdateCPUUsage(List<Server> servers)
        {
            Console.WriteLine("Enter Server #: ");
            bool parsedNumber = int.TryParse(Console.ReadLine(), result: out int serverNumber);

            if (!parsedNumber || !servers.Any(x => x.Id == serverNumber))
            {
                Console.WriteLine("Please enter valid server #.");
            }
            else
            {
                Server server = servers.Where(s => s.Id == serverNumber).First();

                Console.WriteLine("Enter CPU Usage: ");
                bool parsedUsage = int.TryParse(Console.ReadLine(), result: out int cpuUsage);

                if (!parsedUsage || cpuUsage <= 0 || cpuUsage > 100)
                {
                    Console.WriteLine("Please enter valid CPU Usage.");
                }
                else
                {
                    server.UpdateUsage(cpuUsage);
                }
            }
        }

        private static void AllocateServer(List<Server> servers)
        {
            Allocator allocator = new Allocator(servers);
            try
            {
                var allocatedServer = allocator.Allocate();
                if (allocatedServer != null)
                    Console.WriteLine($"New connection from Server{allocatedServer?.Id + 1}.");
                else
                {
                    Console.WriteLine("Server not found.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Server not found.");
            }
        }
    }
}
