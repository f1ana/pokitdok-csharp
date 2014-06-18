﻿// Copyright (C) 2014, All Rights Reserved, PokitDok, Inc.
// http://www.pokitdok.com
//
// Please see the LICENSE.txt file for more information.
// All other rights reserved.
//
//	PokitDok Platform Client for C# Example Application
//		Consume the REST based PokitDok platform API
//		https://platform.pokitdok.com/login#/documentation

using System;
using System.Collections.Generic;
using System.IO;
using pokitdokcsharp;

class MainClass
{
	public static void Main (string[] args)
	{
		try {

			PlatformClient client = new PlatformClient("JcR2P8SmoIaon4vpN9Q9", "JqPijdEL2NYFTJLEKquUzMgAks6JmWyszrbRPk4X");
			client.ApiSite = "http://me.pokitdok.com:5002";
			client.Authenticate();

			Console.WriteLine("==Begin eligibility=======");
			Console.WriteLine(
				client.eligibility(
					new Dictionary<string, object> {
						{"member", new Dictionary<string, object> { 
								{"id", "W000000000"}, 
								{"birth_date", "1970-01-01"}, 
								{"last_name", "Doe"}
							}},
						{"provider", new Dictionary<string, object> { 
								{"npi", "1467560003"}, 
								{"last_name", "AYA-AY"}, 
								{"first_name", "JEROME"}
							}},
						{"service_types", new string[] { "health_benefit_plan_coverage" }},
						{"trading_partner_id", "MOCKPAYER"}
					}).body
			);
			Console.WriteLine("==End eligibility=======");

			Console.WriteLine("==Begin providers json=======");
			Console.WriteLine(
				client.providers(
					new Dictionary<string, string> {
						{"zip_code", "29464"},
						{"radius", "15mi"}
				}).body
			);
			Console.WriteLine("==End providers json=======");

			Console.WriteLine("==Begin providers id=======");
			Console.WriteLine(
				client.providers("1467560003").body
			);
			Console.WriteLine("==End providers id=======");

			Console.WriteLine("==Begin usage=======");
			dynamic usage = client.usage();
			Console.WriteLine(
				string.Format("rate_limit_amount = {0}", usage.rate_limit_amount)
			);
			Console.WriteLine("==End usage=======");

			Console.WriteLine("==Begin Files=======");
			ResponseData resp = client.files(
				"MOCKPAYER",
				"./tests/files/general-physician-office-visit.270"
			);
			Console.WriteLine(resp.body);
			Console.WriteLine(resp.status);
			Console.WriteLine("==End Files=======");

			Console.WriteLine("==Begin Activities=======");
			Console.WriteLine(
				client.activities(new Dictionary<string, string> {
					{"limit", "1"},
					{"offset", "0"}
				}).body
			);
			Console.WriteLine("==End Activities=======");
		}
		catch (Exception ex) 
		{
			Console.Error.WriteLine(ex.Message);
		}
	}
}
