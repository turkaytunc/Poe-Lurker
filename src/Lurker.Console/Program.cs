﻿//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Wohs">
//     Missing Copyright information from a valid stylecop.json file.
// </copyright>
//-----------------------------------------------------------------------

namespace Lurker.Console
{
    using Lurker.Models.TradeAPI;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var service = new Lurker.Patreon.PatreonService();
            Console.WriteLine(service.IsPledging().Result);

            //var lurker = new ClipboardLurker();
            using (var lurker = new ClientLurker())
            {
                lurker.LocationChanged += Lurker_ChangedLocation;
                lurker.RemainingMonsters += Watcher_RemainingMonsters;
                lurker.PlayerJoined += Watcher_PlayerJoined;
                lurker.PlayerLeft += Watcher_PlayerLeft;
                lurker.Whispered += Watcher_Whispered;
                lurker.IncomingOffer += Lurker_NewOffer;
                Console.Read();
            }

            Console.Read();
        }

        private static void Lurker_NewOffer(object sender, Events.TradeEvent e)
        {
            Console.WriteLine($"({e.Date})--[{e.GuildName}]{e.PlayerName} [item: {e.ItemName}] [price: {e.Price}] [position: {e.Location}] ");
        }

        private static void Watcher_Whispered(object sender, Events.WhisperEvent e)
        {
            Console.WriteLine($"[{e.GuildName}]{e.PlayerName}: {e.WhisperMessage}");
        }

        private static void Watcher_PlayerLeft(object sender, Events.PlayerLeftEvent e)
        {
            Console.WriteLine($"{e.PlayerName} left");
        }

        private static void Watcher_PlayerJoined(object sender, Events.PlayerJoinedEvent e)
        {
            Console.WriteLine($"{e.PlayerName} joined");
        }

        private static void Watcher_RemainingMonsters(object sender, Events.MonstersRemainEvent e)
        {
            Console.WriteLine($"Monster {e.MonsterCount}");
        }

        private static void Lurker_ChangedLocation(object sender, Events.LocationChangedEvent e)
        {
            Console.WriteLine(e.Location);
        }
    }
}
