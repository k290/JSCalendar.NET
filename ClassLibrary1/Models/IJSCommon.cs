using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.Models
{

    //Properties common to all 3 types. These are here in case there are any common validation rules
    public interface IJSCommon//this will probably become an abstract class when implementations come in to play as we wont want to allow them to be overrideen
    {
        public string @type { get; }

    }
}
