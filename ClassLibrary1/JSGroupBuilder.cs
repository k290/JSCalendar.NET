using Models;
using System;
using System.Text.Json;

namespace Builder
{
    public class JSGroupBuilder
    {
        private JSGroup _jsGroup;

        public JSGroupBuilder()
        {
            _jsGroup = new JSGroup();
        }

 
        public JSGroupBuilder WithEntry(IGroupEntry entry)
        {
            _jsGroup.AddEntry(entry);
            return this;
        }
  
        internal JSGroup Build()
        {
            return _jsGroup;
        }
    }
}
