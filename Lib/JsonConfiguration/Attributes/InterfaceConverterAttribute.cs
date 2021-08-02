using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lib.JsonConfiguration
{

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
	public class InterfaceConverterAttribute : JsonConverterAttribute
	{
		public InterfaceConverterAttribute(Type converterType)
			: base(converterType)
		{
		}
	}
}
