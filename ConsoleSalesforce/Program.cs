using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConsoleSalesforce
{
	public class Program
	{
		private static SalesforceClient CreateClient()
		{
			return new SalesforceClient
			{
				Username = ConfigurationManager.AppSettings["username"],
				Password = ConfigurationManager.AppSettings["password"],
				Token = ConfigurationManager.AppSettings["token"],
				ClientId = ConfigurationManager.AppSettings["clientId"],
				ClientSecret = ConfigurationManager.AppSettings["clientSecret"]
			};
		}

		static void Main(string[] args)
		{
			var client = CreateClient();
			client.LoginAndCreateACase();
		}
	}
}
