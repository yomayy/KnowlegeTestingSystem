using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuClassLibrary2;
using ModelClassLibrary1;

namespace KnowledgeTestingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager dm = new DataManager();

            TopMenu tm = new TopMenu();

            bool flagExit = false;

            do
            {
                tm.Display();
                switch(tm.GetChoice())
                {
                    case 1:
                        dm.DisplayTests();
                        break;
                    case 2:
                        dm.DoingTest();     //dzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz
                        break;
                    case 3:
                        dm.CreateTest();
                        break;
                    case 4:
                        dm.EditTest();      //dzzzzzzzzzzzzzzzzzzzzzzzzzzzz
                        break;
                    case 5:
                        dm.DeleteTest();
                        break;
                    case 6:
                        dm.ViewResults();
                        break;
                    case 7:
                        flagExit = true;
                        break;
                    default:
                        break;
                }
                if (flagExit == true)
                    break;
            }
            while (tm.AllowContinue());

            Console.WriteLine("\nПрограмма завершена");
        }
    }
}
