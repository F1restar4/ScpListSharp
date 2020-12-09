using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ScpListSharp.Entities;
using System.Net;
using System.IO;

namespace ScpListSharp
{
	/// <summary>
	/// 
	/// </summary>
	public static class Rest
	{
		static DateTime NextAllowed = DateTime.Now;

		/// <summary>
		/// Gets a list of servers owned by the provided account
		/// </summary>
		/// <param name="id">Account ID of the server. Use "!id" in the server console to obtain the id. </param>
		/// <param name="key">API key for the account id. Use "!api" in the server console to obtain the key.</param>
		/// <param name="LastOnline">Adds "LastOnline" field to the response.</param>
		/// <param name="Players">Adds "Players" field to the response.</param>
		/// <param name="PlayerList">Adds "PlayersList" field to the response.</param>
		/// <param name="Info">Adds "Info" field to the response.</param>
		/// <param name="Pastebin">Adds "Pastebin" field to the response.</param>
		/// <param name="Version">Adds "Version" field to the response.</param>
		/// <param name="Flags">Adds flags (eg. friendly-fire, whitelist) to the response.</param>
		/// <param name="Nicknames">Adds nicknames to the "PlayersList". Ignored if "list" parameter is not set to true.</param>
		/// <param name="Online">Adds "Online" (value true if the server is online) to the response.</param>
		/// <returns>A list of servers owned by this account</returns>
		public static async Task<List<SCPServer>> GetOwnServersAsync(int id = 0, string key = null, bool LastOnline = false, bool Players = false, bool PlayerList = false, bool Info = false, bool Pastebin = false, bool Version = false, bool Flags = false, bool Nicknames = false, bool Online = false)
		{
			if (NextAllowed > DateTime.Now)
				throw new WebException($"You are being rate limited, try again at: {NextAllowed}");

			string url = "https://api.scpslgame.com/serverinfo.php?";
			if (key != null)
				url += $"id={id}&key={key}";
			url += $"&lo={LastOnline}&players={Players}&list={PlayerList}&info={Info}&pastebin={Pastebin}&version={Version}&flags={Flags}&nicknames={Nicknames}&online={Online}";

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

		/// <summary>
		/// Gets a list of servers owned by the provided account
		/// </summary>
		/// <param name="id">Account ID of the server. Use "!id" in the server console to obtain the id. </param>
		/// <param name="key">API key for the account id. Use "!api" in the server console to obtain the key.</param>
		/// <param name="LastOnline">Adds "LastOnline" field to the response.</param>
		/// <param name="Players">Adds "Players" field to the response.</param>
		/// <param name="PlayerList">Adds "PlayersList" field to the response.</param>
		/// <param name="Info">Adds "Info" field to the response.</param>
		/// <param name="Pastebin">Adds "Pastebin" field to the response.</param>
		/// <param name="Version">Adds "Version" field to the response.</param>
		/// <param name="Flags">Adds flags (eg. friendly-fire, whitelist) to the response.</param>
		/// <param name="Nicknames">Adds nicknames to the "PlayersList". Ignored if "list" parameter is not set to true.</param>
		/// <param name="Online">Adds "Online" (value true if the server is online) to the response.</param>
		/// <returns>A list of servers owned by this account</returns>
		public static List<SCPServer> GetOwnServers(int id = 0, string key = null, bool LastOnline = false, bool Players = false, bool PlayerList = false, bool Info = false, bool Pastebin = false, bool Version = false, bool Flags = false, bool Nicknames = false, bool Online = false)
		{
			if (NextAllowed > DateTime.Now)
				throw new WebException($"You are being rate limited, try again at: {NextAllowed}");

			string url = "https://api.scpslgame.com/serverinfo.php?";
			if (key != null)
				url += $"id={id}&key={key}";
			url += $"&lo={LastOnline}&players={Players}&list={PlayerList}&info={Info}&pastebin={Pastebin}&version={Version}&flags={Flags}&nicknames={Nicknames}&online={Online}";

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

		/// <summary>
		/// Returns current IP.
		/// </summary>
		/// <returns>Current IP</returns>
		public static async Task<string> GetIPAsync()
		{
			var request = WebRequest.CreateHttp("https://api.scpslgame.com/ip.php");
			var response = request.GetResponse();
			using (var responseStream = response.GetResponseStream() ?? Stream.Null)
			using (var responseReader = new StreamReader(responseStream))
			{
				return await responseReader.ReadToEndAsync();
			}
		}


		/// <summary>
		/// Returns current IP.
		/// </summary>
		/// <returns>Current IP</returns>
		public static string GetIP()
		{
			var request = WebRequest.CreateHttp("https://api.scpslgame.com/ip.php");
			var response = request.GetResponse();
			using (var responseStream = response.GetResponseStream() ?? Stream.Null)
			using (var responseReader = new StreamReader(responseStream))
			{
				return responseReader.ReadToEnd();
			}
		}
	}
}
