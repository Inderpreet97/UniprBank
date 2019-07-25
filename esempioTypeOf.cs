using System;
using System.Collections.Generic;

namespace testGround
{
    class Program
    {
        static void Main(string[] args) {

            List<object> lista = new List<object>() { };
            lista.Add(10);
            lista.Add("string");


            if(lista[0].GetType() == typeof(int)) {

               Console.WriteLine("int == int");
            }

            if (lista[1].GetType() == typeof(string)) {

                Console.WriteLine("string == string");
            }

            Console.WriteLine("Hello World!");
        }
    }
}
