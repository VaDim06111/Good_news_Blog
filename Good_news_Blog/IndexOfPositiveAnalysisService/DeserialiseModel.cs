using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexOfPositiveAnalysisService
{
    public class DeserialiseModel
    {
        public string text { get; set; }
        public Annotations annotations { get; set; }
    }

    public class Lemma
    {
        public int start { get; set; }
        public int end { get; set; }
        public string value { get; set; }
    }

    public class Annotations
    {
        public List<Lemma> lemma { get; set; }
    }
}
