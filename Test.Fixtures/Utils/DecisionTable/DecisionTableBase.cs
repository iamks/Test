using System.Diagnostics;

namespace Test.Fixtures.Utils.DecisionTable
{
    public abstract class DecisionTableBase
    {
        private string fitnesseId;
        
        public string FitnesseId
        {
            get
            {
                return fitnesseId;
            }
            set
            {
                fitnesseId = value.ToString();
            }
        }

        public bool? Debug
        {
            set
            {
                if(value == true)
                {
                    Debugger.Launch();
                }
            }
        }

        public async virtual Task Table()
        {
            Console.WriteLine("Table");
        }

        public async virtual Task BeginTable()
        {
            Console.WriteLine("BeginTable");
        }

        public async virtual Task Reset()
        {
            Console.WriteLine("Reset");
        }

        public async virtual Task Execute()
        {
            Console.WriteLine("Execute");
        }

        public async virtual Task EndTable()
        {
            Console.WriteLine("EndTable");
        }
    }
}
