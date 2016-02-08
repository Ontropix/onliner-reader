using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Onliner
{

	public class Comment
	{
		public string AuthorName { get; set; }

		public string AuthorAvatar { get; set; }

		public DateTime CreatedAt { get; set; }

		public string Text { get; set; }
	}
}
