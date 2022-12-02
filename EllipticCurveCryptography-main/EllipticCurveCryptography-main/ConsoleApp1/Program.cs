using ConsoleApp1.Entities;
using ConsoleApp1.Management;
using System.Threading;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Function fn = new Function(-2,3,137,new ToaDo(51,22));
            fn.khoitao();
            Thread t = new Thread(()=>{
                   Thread t1 = new Thread(() =>
                    {
                        fn.sinhkt();
                    });

                   Thread t2 = new Thread(() =>
                   {
                       fn.sinhtd();
                   });

                   t1.Start();
                   t2.Start();
                   t2.Join();
               });
             t.Start();
             t.Join();

            string a = File.ReadAllText("ECCtext.txt");
            Console.WriteLine(a);
            int option=0;
            string str;
            int key;
            int k;

            while (true)
            {
                do
                {
                    Console.WriteLine("OPTION:");
                    option = int.Parse(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            fn.displayBKT();
                            break;
                        case 2:
                            Console.WriteLine("ENTER THE VALUES");

                            Console.WriteLine("KEY:");
                            key = int.Parse(Console.ReadLine());
                            Console.WriteLine("K:");
                            k = int.Parse(Console.ReadLine());
                            Console.WriteLine("STRING:");
                            str = Console.ReadLine();

                            fn.mahoachuoi(str, key, k);

                            Console.WriteLine("==========RESULT==========");
                            fn.displayMa();

                            break;

                        case 3:
                            Console.WriteLine("ENTER THE VALUES");

                            Console.WriteLine("STRING");
                            str = Console.ReadLine();
                            Console.WriteLine("KEY");
                            key = int.Parse(Console.ReadLine());

                            fn.giaimachuoi(str, key);
                            Console.WriteLine("==========RESULT==========");
                            fn.displayRo();
                            Console.WriteLine();
                            break;

                        case 4:
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine(a);
                            break;
                    }

                } while (option <= 0 || option > 5);
            }
        }
    }
}
