using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleScraper.Models
{
	public class InstagramUser
	{
		public string FullName { get; set; }
		public int FollowerCount { get; set; }
		public int FollowingCount { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
		public void Display()
		{
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("--------------------------------");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Full name: {FullName}"); 
			Console.WriteLine($"Follower Count: { FollowerCount }");
			Console.WriteLine($"Following Count: { FollowingCount }");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine("--------------------------------");
			Console.ForegroundColor = ConsoleColor.White;
		
		}
	}
}
