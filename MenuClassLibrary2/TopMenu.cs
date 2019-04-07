using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuClassLibrary2
{
    public class TopMenu : Menu
    {
        public TopMenu() : base()
        { }

        public override void DefineItems()
        {
            items.Add("Просмотр тестов");
            items.Add("Прохождение теста");
            items.Add("Создание теста");
            items.Add("Редактирование теста");
            items.Add("Удаление теста");
            items.Add("Просмотр результата");
            items.Add("Выход из программы");
        }

        public override void DefineTitle()
        {
            title = "Cистема тестирования знаний  |";
        }
    }
}
