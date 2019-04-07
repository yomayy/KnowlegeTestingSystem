using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuClassLibrary2
{
    public abstract class Menu
    {
        protected string title;
        protected List<string> items;

        public Menu()
        {
            items = new List<string>();
            DefineTitle();
            DefineItems();
        }

        public abstract void DefineTitle();
        public abstract void DefineItems();

        public void Display()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("\t ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("\t {0}", title);
            Console.WriteLine("\t ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            int count = 0;
            foreach (string item in items)
            {
                Console.WriteLine("\t>    {0} - {1}", ++count, item);
            }
            Console.WriteLine("\n ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }

        public int GetChoice()
        {
            Console.WriteLine("\n> Выберите нужный пункт меню");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }

        public bool AllowContinue()
        {
            Console.Write("\n> Continue (y/n)? -");
            string answer = Console.ReadLine();
            return (answer == "y");
        }
    }
}
