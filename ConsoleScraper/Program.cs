using ConsoleScraper.Models;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleScraper
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// receive a profile name
			
			//Console.WriteLine($"The profile na is {profileName}");
			//Console.WriteLine($"URL to be queried {url}");
			foreach (var arg in args)
			{
				if (string.IsNullOrEmpty(arg)) continue;

				var profileName = arg;
				var url = $"https://instagram.com/{profileName}";
				ScrapeInstagram(url);
			}

			Console.ReadLine();
		}

		public static async Task ScrapeInstagram(string url)
		{
			using (var httpClient = new HttpClient())
			{
				var response = await httpClient.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					// create html document
					var htmlBody = await response.Content.ReadAsStringAsync();
					var htmlDocument = new HtmlDocument();
					htmlDocument.LoadHtml(htmlBody);

					//Console.WriteLine(htmlBody);
					// select script tags
					var scripts = htmlDocument.DocumentNode.SelectNodes("/html/body/script");
					//preprocess result
					var uselessString = "window._sharedData = ";
					//Console.WriteLine($"Got cool array of scripts of size { scripts.Count }");					
					var scriptInnerText = scripts[0].InnerText.Substring(uselessString.Length).Replace(";", "");

					// serialize objects and fetch the user data
					dynamic jsonStuff = JObject.Parse(scriptInnerText);
					dynamic userProfile = jsonStuff["entry_data"]["ProfilePage"][0]["graphql"]["user"];


					var instagramUser = new InstagramUser
					{
						FullName = userProfile.full_name,
						FollowerCount = userProfile.edge_followed_by.count,
						FollowingCount = userProfile.edge_follow.count
					};

					instagramUser.Display();
				}
			}
		}
	}
}
