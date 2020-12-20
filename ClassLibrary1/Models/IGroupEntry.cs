using Lib.Builders;
using Lib.JsonConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Models
{
    [JsonInterfaceConverter(typeof(GroupEntryConverter))]
    public interface IGroupEntry
    {
    }
}
