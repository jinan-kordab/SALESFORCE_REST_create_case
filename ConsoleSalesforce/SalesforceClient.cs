using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Salesforce.Common;
using Salesforce.Force;
using System.Dynamic;
using Newtonsoft.Json.Linq;

namespace ConsoleSalesforce
{
	class SalesforceClient
	{

        //developer salesforce account REST endpoint
		private const string LOGIN_ENDPOINT = "https://login.salesforce.com/services/oauth2/token";

		// sandbox REST end point
		//private const string LOGIN_ENDPOINT = "https://test.salesforce.com/services/oauth2/token";


		private const string API_ENDPOINT = "/services/data/v36.0/";
		private const string SF_CASE_ENDPOINT = "/services/data/v39.0/sobjects/Case";

		public string Username { get; set; }
		public string Password { get; set; }
		public string Token { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string AuthToken { get; set; }
		public string InstanceUrl { get; set; }

		public string ResponseCreateCaseID { get; set; }
		public string ResponseCreateCaseErrors { get; set; }
		public bool ResponseCreateCaseSuccess { get; set; }

		public string statusException { get; set; }

		public void LoginAndCreateACase()
		{
			//Login first, perform Authentication adn Authorization with SF

			String jsonResponse;
			using (var client = new HttpClient())
			{
				var request = new FormUrlEncodedContent(new Dictionary<string, string>
			{
				{"grant_type", "password"},
				{"client_id", ClientId},
				{"client_secret", ClientSecret},
				{"username", Username},
				{"password", Password + Token}
			}
				);
				request.Headers.Add("X-PrettyPrint", "1");
				var response = client.PostAsync(LOGIN_ENDPOINT, request).Result;
				jsonResponse = response.Content.ReadAsStringAsync().Result;
			}
			var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
			AuthToken = values["access_token"];
			InstanceUrl = values["instance_url"];


			//2. If logged in successful, create a case 
			using (var clientCase = new HttpClient())
			{
				try
				{
					clientCase.DefaultRequestHeaders.Add("Authorization", "Bearer " + AuthToken);
					HttpResponseMessage responseCase = clientCase.PostAsJsonAsync(InstanceUrl + SF_CASE_ENDPOINT, new Case
					{
						Status = "New",                                           // Class Case has more details on this parameter. It is SF API parameter
						Reason = "NEW CASE - TOP",
						Subject = "THOUGHTS ON PROGRAMMING",                      //
						SuppliedName = "TESTING",                                 //
						Priority = "Low",                                         //
						Type = "TEST -TEST ",                                   // 
						Origin = "TOP",                                    //
						Description = "DESCRIPTION OF THE CASE",                  //
						IsEscalated = false,                                      // 
						SuppliedEmail = "testemail@someserver.com",         // 
						SuppliedPhone = "555-555-5555",                           //
						SuppliedCompany = ""                                      //Leave Blank

					}).Result;

					responseCase.EnsureSuccessStatusCode();

					string statusText = responseCase.StatusCode.ToString();		 // This value should be "Created", if the new case was created successfully.
				}
				catch (HttpRequestException hexp)
				{
					statusException = hexp.ToString();
				}
				catch (Exception exp)
				{
					statusException = exp.ToString();
				}
			}
		}

		static SalesforceClient()
		{
			// SalesForce requires TLS 1.1 or 1.2
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
		}


		// A raw query method, if needed, to query SF directly
		public string Query(string soqlQuery)
		{
			using (var client = new HttpClient())
			{
				string restRequest = InstanceUrl + API_ENDPOINT + "query/?q=" + soqlQuery;
				var request = new HttpRequestMessage(HttpMethod.Get, restRequest);
				request.Headers.Add("Authorization", "Bearer " + AuthToken);
				request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				request.Headers.Add("X-PrettyPrint", "1");
				var response = client.SendAsync(request).Result;
				return response.Content.ReadAsStringAsync().Result;
			}
		}
	}
}
