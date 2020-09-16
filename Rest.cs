using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;
using ScpListSharp.Entities;
using System.Net;
using System.IO;

namespace ScpListSharp
{
	public static class Rest
	{
		/// <summary>
		/// Gets a list of servers owned by the provided account
		/// </summary>
		/// <param name="id">Account ID of the server. Use "!id" in the server console to obtain the id. </param>
		/// <param name="key">API key for the account id. Use "!api" in the server console to obtain the key.</param>
		/// <returns></returns>
		public static List<SCPServer> GetOwnServers(int id, string key )
		{
			string url = $"https://api.scpslgame.com/serverinfo.php?id={id}&key={key}&lo=true&players=true&list=true&info=true&pastebin=true&version=true&flags=true&nicknames=true";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.ContentType = "application/json";
			string data;
			WebResponse response = request.GetResponse();
			using (Stream responseStream = response.GetResponseStream() ?? Stream.Null)
			using (StreamReader responseReader = new StreamReader(responseStream))
			{
					data = responseReader.ReadToEnd();
			}
			var outResponse = JsonConvert.DeserializeObject<SCPServersResponse>(data);
			return outResponse.Servers;
		}
	}
}
