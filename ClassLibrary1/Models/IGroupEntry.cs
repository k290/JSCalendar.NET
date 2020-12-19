using Lib.Builders;
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
