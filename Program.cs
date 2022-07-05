using ECommerceApp.Models;
using ECommerceApp.Repositories;
using ECommerceApp.Menu;
using ECommerceApp.Enums;
using System;

namespace ECommerceApp.Menu
{
    class Program
    {
        static void Main(string[] args)
        {
            (new MainMenu()).Menu();

          int[] array = new int[5] {2,3,4,5,6};
          foreach (var item in array)
          {
              Console.WriteLine(item);
          }
        }
        
    }
}

