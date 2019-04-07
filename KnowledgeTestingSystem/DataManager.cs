using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClassLibrary1;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace KnowledgeTestingSystem
{
    class DataManager
    {
        private string path;
        private BinaryFormatter bf;

        public List<Test> Tests { get; set; }

        public DataManager()
        {
            Tests = new List<Test>();
            path = @"..\..\Data\tests.dat";
            bf = new BinaryFormatter();
            LoadData();
            
        }

        //Only once run - for Init dat-file!!!
        private void Init()
        {
            //Question 1:
            Answer[] arr1 = new Answer[]
            {
                new Answer() { Id = 1, Text = "5", Right = false },
                new Answer() { Id = 2, Text = "4", Right = true },
                new Answer() { Id = 3, Text = "7", Right = false },
                new Answer() { Id = 4, Text = "0", Right = false }
            };
            Question q1 = new Question()
            {
                Id = 1,
                Text = "\t> Сколько будет 2х2 ?"
            };
            q1.answers.AddRange(arr1);

            //Question 2:
            Answer[] arr2 = new Answer[]
            {
                new Answer() { Id = 1, Text = "Don`t know", Right = false },
                new Answer() { Id = 2, Text = "Напротив 7 корпуса", Right = true },
                new Answer() { Id = 3, Text = "Кафе", Right = false },
                new Answer() { Id = 4, Text = "Reception", Right = true }
            };
            Question q2 = new Question()
            {
                Id = 2,
                Text = "\t> Как пройти в библиотеку?"
            };
            q2.answers.AddRange(arr2);

            //Test 1:
            Test t1 = new Test()
            {
                Id = 1, Title = "Test 1"
            };
            t1.questions.Add(q1);
            t1.questions.Add(q2);
            Tests.Add(t1);
        }

        public void LoadData()
        {
            //десериализация:
            FileStream fs = new FileStream(path, FileMode.Open,
                FileAccess.Read);
            Tests = (List<Test>)bf.Deserialize(fs);
            fs.Close();
        }

        public void SaveData()
        {
            //сериализация:
            FileStream fs = new FileStream(path, FileMode.Create, 
                FileAccess.Write);
            bf.Serialize(fs, Tests);
            fs.Close();
        }

        ////////////////////////////////

        public void DisplayTests()
        {
            if(Tests.Count == 0)
            {
                Console.WriteLine("  В системе нет сохраненных тестов");
            }
            else
            {
                foreach(Test t in Tests)
                {
                    Console.WriteLine($"  {t.Id}. {t.Title}");
                    foreach(Question q in t.questions)
                    {
                        Console.WriteLine($"     {q.Id}. {q.Text}");
                    }
                }
            }
        }

        public void DoingTest()     //dz|||||||||||||||||||||||||||||
        {
            Console.Write("\n> Ваше ФИО: ");
            string name = Console.ReadLine();
            //HOMETASK

            foreach (Test t in Tests)
            {
                Console.WriteLine($"  {t.Id}. {t.Title}");
            }

            Console.WriteLine("Выберите тест (укажите номер): ");
            int test_id = Convert.ToInt32(Console.ReadLine());
            if(Tests.Count < test_id || test_id < 1)
            {
                Console.WriteLine("Теста с таким ID нет");
            }
            else
            {
                double points = 0;

                Console.WriteLine(Tests.Find(t => t.Id == test_id).Title);
                int num_quest = 1;
                while (Tests.Find(t => t.Id == test_id).questions.Count >= num_quest)
                {
                    Console.WriteLine("Вопрос номер {0}: ", num_quest);
                    Console.WriteLine(Tests.Find(t => t.Id == test_id).questions.Find(q => q.Id == num_quest).Text);
                    Console.WriteLine("Ваш ответ: ");
                    string ans_to_quest = Console.ReadLine();
                    if (ans_to_quest == Tests.Find(t => t.Id == test_id).questions.Find(q => q.Id == num_quest).answers.Find(a => a.Right == true).Text)
                        points++;

                    num_quest++;
                }
                
                double grade = points / (Tests.Find(t => t.Id == test_id).questions.Count) * 100;
                Console.WriteLine("Ваш результат теста: {0} -> {1}%", Tests.Find(t => t.Id == test_id).Title, grade);

                string result = String.Format("{0} -> ФИО: {1}, результат теста: '{2}' : {3}%",
                    DateTime.Now, name, Tests.Find(t => t.Id == test_id).Title, grade);

                string route = @"..\..\Data\Results.txt";
                FileStream fs = new FileStream(route, FileMode.Append,
                    FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(result);
                sw.Close();
                fs.Close();
                Console.WriteLine("   Данные успешно сохранены");
            }
        }           

        public void CreateTest()
        {
            Test t = new Test();
            //1.Вводим название теста:
            Console.WriteLine("\n> Введите название Вашего теста");
            Console.WriteLine("   ");
            t.Title = Console.ReadLine();
            t.Id = Tests.Count + 1;

            //2.Ввод вопросов и ответов:
            string ans;
            do
            {
                Question q = new Question();
                Console.Write("\n> Введите текст вопроса: ");
                q.Text = Console.ReadLine();
                q.Id = t.questions.Count + 1;

                string ans2;
                do
                {
                    Answer a = new Answer();
                    Console.Write("\n> Введите текст ответа: ");
                    a.Text = Console.ReadLine();
                    a.Id = q.answers.Count + 1;
                    Console.Write(" Ответ - правильный -> 1 ");
                    if(Convert.ToInt32(Console.ReadLine()) == 1 )
                        a.Right = true;
                    q.answers.Add(a);

                    Console.WriteLine("\n> Еще ответ (y/n)?");
                    ans2 = Console.ReadLine();
                }
                while (ans2 == "y");
                t.questions.Add(q);

                Console.WriteLine("\n> Еще вопрос (y/n)?");
                ans = Console.ReadLine();
            }
            while (ans == "y");
            Tests.Add(t);
            Console.WriteLine("  Тест успешно добавлен");
            SaveData();
        }

        public void EditTest()      //dz|||||||||||||||||||||||||||||
        {
            DisplayTests();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            string ans;
            do
            {
                Console.WriteLine("\n> Введите ID редактируемого теста: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (Tests.Count < id || id < 1)
                {
                    Console.WriteLine("Теста с таким ID не существует");
                }
                else
                {
                    string ans_title;
                    Console.WriteLine("\n> Хотите изменить название ({0}) теста (y/n)?", id);
                    ans_title = Console.ReadLine();
                    while (ans_title == "y")
                    {
                        Console.WriteLine("Введите новое название {0} теста: ", id);
                        this.Tests.Find(t => t.Id == id).Title = Console.ReadLine();
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`");
                        DisplayTests();
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`");
                        Console.WriteLine("Хотите изменить название еще раз (y/n)?");
                        ans_title = Console.ReadLine();
                    }
                    ////////////////////////////////////////////////////////////////////////////////////////////
                    string ans_question;
                    Console.WriteLine("\n> Хотите изменить вопрос в этом ({0}) тесте (y/n)?", id);
                    ans_question = Console.ReadLine();
                    while (ans_question == "y")
                    {
                        Console.WriteLine("\n> Введите номер вопроса {0} теста", id);
                        int question_id = Convert.ToInt32(Console.ReadLine());
                        if (this.Tests.Find(t => t.Id == id).questions.Count < question_id || id < 1)
                        {
                            Console.WriteLine("\n> Вопроса с таким ID не существует");
                        }
                        else
                        {
                            Console.WriteLine("\n> Введите новое название {0} вопроса: ", question_id);
                            this.Tests.Find(t => t.Id == id).questions.Find(q => q.Id == question_id).Text = Console.ReadLine();
                            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`");
                            DisplayTests();
                        }
                        Console.WriteLine("\n> Хотите еще изменить вопрос (y/n)?");
                        ans_question = Console.ReadLine();
                    }
                    SaveData();
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`");
                    DisplayTests();
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~`");

                    /////////////////////////////////////////////////////////

                    string ans_add_question;
                    Console.WriteLine("Хотите добавить вопрос в {0} тесте (y/n)?", id);
                    ans_add_question = Console.ReadLine();
                    while(ans_add_question == "y")
                    {
                        Question q1 = new Question();
                        Console.Write("\n> Введите текст вопроса: ");
                        q1.Text = Console.ReadLine();
                        q1.Id = this.Tests.Find(t => t.Id == id).questions.Count + 1;

                        string ans3;
                        do
                        {
                            Answer a1 = new Answer();
                            Console.Write("\n> Введите текст ответа: ");
                            a1.Text = Console.ReadLine();
                            a1.Id = q1.answers.Count + 1;
                            Console.Write(" Ответ - правильный -> 1 ");
                            if (Convert.ToInt32(Console.ReadLine()) == 1)
                                a1.Right = true;
                            q1.answers.Add(a1);

                            Console.WriteLine("\n> Еще ответ (y/n)?");
                            ans3 = Console.ReadLine();
                        }
                        while (ans3 == "y");
                        this.Tests.Find(t => t.Id == id).questions.Add(q1);
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        DisplayTests();
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                        Console.WriteLine("\n> Еще вопрос (y/n)?");
                        ans_add_question = Console.ReadLine();
                    }
                }

                SaveData();

                //////////////////////////////////
                Console.WriteLine("Продолжить редактирование тестов (y/n)?");
                ans = Console.ReadLine();
            }
            while (ans == "y");
        }

        public void ViewResults()   //dz|||||||||||||||||||||||||||||
        {
            try
            {   
                using (StreamReader sr = new StreamReader(@"..\..\Data\Results.txt"))
                {
                    string line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteTest()
        {
            Console.Write("\n Enter the test Id which must be deleted: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Tests.RemoveAt(id - 1);
            if (id < Tests.Count +1)
            {
                for(int i = id - 1; i < Tests.Count; i++)
                {
                    Tests[i].Id--;
                }
            }
            SaveData();
            Console.WriteLine("   Теперь он делитнулся");
        }
    }
}
