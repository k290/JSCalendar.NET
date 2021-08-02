using Lib.Builders;
using Lib.JsonConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    //todo removing this attribute and the entries don't appear in client but the tests pass. Add or fix tests
    [JsonInterfaceConverter(typeof(GroupEntryConverter))]
    public interface IGroupEntry
    {
    }
}
