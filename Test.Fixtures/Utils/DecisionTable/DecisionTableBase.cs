using System.Diagnostics;
using System.Reflection;
using Test.Fixtures.Utils.Extensions;

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

        public async virtual Task BeginTable()
        {
        }

        public async virtual Task Reset()
        {
        }

        public async virtual Task Execute()
        {
        }

        public async virtual Task EndTable()
        {
        }

        public async virtual Task Table(List<List<string>> table)
        {
            var firstTableItem = table.First();
            foreach (var header in firstTableItem)
            {
                this.CheckColumnConventions(header);
            }
        }

        protected virtual void CheckColumnConventions(string header)
        {
            if (header.StartsWith("#")) //Commented column, to be ignored
            {
                return;
            }

            if (header.EndsWith("?"))
            {
                string propertyCase = header.ToPropertyCase();
                MemberInfo[] member = this.GetType().GetMember(propertyCase, BindingFlags.Instance | BindingFlags.Public);

                this.CheckOutputColumn(header, ((IEnumerable<MemberInfo>)member).Single<MemberInfo>());
                return;
            }
        }


        private void CheckOutputColumn(string header, MemberInfo memberInfo)
        {
            if (memberInfo.MemberType == MemberTypes.Property && !memberInfo.Name.StartsWith("Expected"))
            {
                throw new Exception("An Output column must begin with 'Expected'");
            }
        }
    }
}
