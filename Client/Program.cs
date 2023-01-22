using System;
using System.Collections.Generic;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("welcome to server status service");
            Console.Write("Enter server addres : ");
            string address = Console.ReadLine();
            Console.Write("Enter server token : ");
            string token = Console.ReadLine();
            Console.Clear();
            sdk SystemStatus = new sdk(address, token);
            (string Message, bool LoginStatus) = SystemStatus.login();
            if (LoginStatus)
            {
                Console.WriteLine($"Username : {Message}\nLogin Time : {DateTime.Now}");
                Console.WriteLine("1>Info\n2>process\n3>command\n0>exit");
                while (true)
                {
                    Console.Write("~>#");
                    string command = Console.ReadLine();
                    if (command == "1" || command == "info")
                    {
                        (Dictionary<string, string> value, bool status) = SystemStatus.Info();
                        if (status)
                        {
                            foreach (var item in value)
                            {
                                Console.WriteLine($"{item.Key} : {item.Value}");
                            }
                        }
                    }
                    else if (command == "2" || command == "process")
                    {
                        (List<Dictionary<int, string>> value, bool status) = SystemStatus.Process();
                        if (status)
                        {
                            foreach (var items in value)
                            {
                                foreach (var item in items)
                                {
                                    Console.WriteLine($"ID : {item.Key} | Name : {item.Value}");
                                }
                            }
                        }
                    }
                    else if (command == "3" || command == "command")
                    {
                        while (true)
                        {
                            Console.Write($"<{Message}>enter command >");
                            string cmd = Console.ReadLine();
                            if (cmd == "exit")
                            {
                                break;
                            }
                            else if (!string.IsNullOrEmpty(cmd))
                            {
                                (Dictionary<string, string> value, bool status) = SystemStatus.Command(cmd);
                                if (status)
                                {
                                    foreach (var item in value)
                                    {
                                        Console.WriteLine(item.Value);
                                    }
                                }
                            }
                        }
                    }
                    else if (command == "0" || command == "exit")
                    {
                        Console.WriteLine($"Exit time : {DateTime.Now}");
                        break;
                    }
                    else if (command=="cls")
                    {
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine($"'{command}' is not recognized as an internal or external command");
                    }
                }
            }
            else
            {
                Console.WriteLine(Message);
                Console.ReadKey();
            }
        }
    }
}
