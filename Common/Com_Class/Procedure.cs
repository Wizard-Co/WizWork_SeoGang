using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class Procedure
    {
        public string Name { get; set; }
        public string OutputUseYN { get; set; }
        public string OutputName { get; set; }
        public string OutputLength { get; set; }
        public int OutputCount { get; set; }
        public List<string> list_OutputName { get; set; }
        public List<string> list_OutputLength { get; set; }
    }
}

