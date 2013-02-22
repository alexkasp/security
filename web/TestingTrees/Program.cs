using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SandBox;
using SandBox.WebUi;

namespace TestingTrees
{
    class Program
    {
        static void Main(string[] args)
        {
            SandBox.
            CompareTrees ct = new CompareTrees();
            ct.GetRschTree(109, 115);
        }
    }
}
