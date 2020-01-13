using EdgeWorks.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Console = Colorful.Console;

namespace EdgeWorks.Tools.Services
{
    public class AnalyseService
    {
        public AnalyseService()
        {

        }

        public async Task Test()
        {
            Console.WriteLine("This is a {0} with {1} types and it works: {2}", "test", 3, true);
        }
    }
}
