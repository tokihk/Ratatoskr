using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.ProtocolParser
{
	internal class ProtocolParseInfo
	{
		public class InformationObject
		{
			public InformationObject(string text = "")
			{
				Text = text;
			}

			public string					Text;
			public List<InformationObject>	SubItems { get; } = new List<InformationObject>();

			public InformationObject Add(string text)
			{
				var item = new InformationObject(text);

				SubItems.Add(item);

				return (item);
			}
		}
	}
}
