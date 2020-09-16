using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ScpListSharp.Entities
{
	/// <summary>
	/// An SCP server
	/// </summary>
	public class SCPServer
	{
		/// <summary>
		/// The server's ID
		/// </summary>
		[JsonProperty]
		public int ID { get; internal set; }

		/// <summary>
		/// The server's port
		/// </summary>
		[JsonProperty]
		public int Port { get; internal set; }

		/// <summary>
		/// Last time the server was online. 
		/// This will only have a date
		/// </summary>
		[JsonProperty]
		public DateTime LastOnline { get; internal set; }

		/// <summary>
		/// The number of players connected to the server.
		/// Formatted as x/y
		/// </summary>
		[JsonProperty]
		public string Players { get; internal set; }

		/// <summary>
		/// List of players connected to the server.
		/// Null if no players are connected.
		/// </summary>
		[JsonProperty]
		public List<SCPPlayer> PlayerList { get; internal set; }

		/// <summary>
		/// The server title.
		/// This will include any formatting.
		/// </summary>
		[JsonProperty]
		public string Info { get; internal set; }

		/// <summary>
		/// The version of the game the server is running.
		/// </summary>
		[JsonProperty]
		public string Version { get; internal set; }

		/// <summary>
		/// The id of the server's info pastebin.
		/// </summary>
		[JsonProperty]
		public string Pastebin { get; internal set; }

		/// <summary>
		/// Friendly fire enabled
		/// </summary>
		[JsonProperty]
		public bool FF { get; internal set; }

		/// <summary>
		/// Whitelist enabled
		/// </summary>
		[JsonProperty]
		public bool WL { get; internal set; }

		/// <summary>
		/// Server modded
		/// </summary>
		[JsonProperty]
		public bool Modded { get; internal set; }

		[JsonProperty]
		public int Mods { get; internal set; }

		[JsonProperty]
		public bool Suppress { get; internal set; }

		[JsonConstructor]
		internal SCPServer(int ID, int Port, DateTime LastOnline, string Players, List<SCPPlayer> PlayerList, string Info, string Version, string Pastebin, bool FF, bool WL, bool Modded, int Mods, bool Suppress)
		{
			this.ID = ID;
			this.Port = Port;
			this.LastOnline = LastOnline;
			this.Players = Players;
			this.PlayerList = PlayerList;
			this.Info = Info;
			this.Version = Version;
			this.Pastebin = Pastebin;
			this.FF = FF;
			this.WL = WL;
			this.Modded = Modded;
			this.Mods = Mods;
			this.Suppress = Suppress;
		}
	}

	/// <summary>
	/// A player currently connected to the server
	/// </summary>
	public struct SCPPlayer
	{
		/// <summary>
		/// The player's ID
		/// Will be formatted as id@platform
		/// </summary>
		[JsonProperty]
		public string ID { get; internal set; }

		/// <summary>
		/// The player's name
		/// </summary>
		[JsonProperty]
		public string Name { get; internal set; }

		[JsonConstructor]
		internal SCPPlayer(string ID, string Name)
		{
			this.ID = ID;
			this.Name = Name;
		}
	}

	internal class SCPServersResponse
	{
		[JsonProperty]
		public bool Success { get; internal set; }

		[JsonProperty]
		public int Cooldown { get; internal set;  }

		[JsonProperty]
		public List<SCPServer> Servers { get; internal set; }

		[JsonConstructor]
		internal SCPServersResponse(bool Success, int Cooldown, List<SCPServer> Servers)
		{
			this.Success = Success;
			this.Cooldown = Cooldown;
			this.Servers = Servers;
		}
	}
}
