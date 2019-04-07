using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary1
{
    [Serializable]
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Question> questions { get; set; }

        public Test()
        {
            questions = new List<Question>();
        }
    }
}
