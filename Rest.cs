using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using ScpListSharp.Entities;
using System.Net;
using System.IO;
using System.Linq;

namespace ScpListSharp
{
	public static class Rest
	{
		static DateTime NextAllowed = DateTime.Now;
		/// <summary>
		/// Gets a list of servers owned by the provided account
		/// </summary>
		/// <param name="id">Account ID of the server. Use "!id" in the server console to obtain the id. </param>
		/// <param name="key">API key for the account id. Use "!api" in the server console to obtain the key.</param>
		/// <returns></returns>
		public static async Task<List<SCPServer>> GetOwnServersAsync(int id, string key)
		{
			if (NextAllowed > DateTime.Now)
				throw new WebException($"You are being rate limited, try again at: {NextAllowed}");

			string url = $"https://api.scpslgame.com/serverinfo.php?id={id}&key={key}&lo=true&players=true&list=true&info=true&pastebin=true&version=true&flags=true&nicknames=true";
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			request.ContentType = "application/json";
			string data;
			WebResponse response = request.GetResponse();
			using (Stream responseStream = response.GetResponseStream() ?? Stream.Null)
			using (StreamReader responseReader = new StreamReader(responseStream))
			{
					data = await responseReader.ReadToEndAsync();
			}
			var outResponse = JsonConvert.DeserializeObject<SCPServersResponse>(data);
			NextAllowed = DateTime.Now.AddSeconds(outResponse.Cooldown);
			foreach (var cur in outResponse.Servers)
			{
				cur.Info = Encoding.UTF8.GetString(Convert.FromBase64String(cur.Info));
			}
			return outResponse.Servers;
		}

		public static List<SCPServer> GetOwnServers(int id, string key)
		{
			if (NextAllowed > DateTime.Now)
				throw new WebException($"You are being rate limited, try again at: {NextAllowed}");

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
			NextAllowed = DateTime.Now.AddSeconds(outResponse.Cooldown);
			foreach (var cur in outResponse.Servers)
			{
				cur.Info = Encoding.UTF8.GetString(Convert.FromBase64String(cur.Info));
			}
			return outResponse.Servers;
		}
	}
}
