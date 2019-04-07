using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClassLibrary1
{
    [Serializable]
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Answer> answers { get; set; }

        public Question()
        {
            answers = new List<Answer>();
        }
    }
}
