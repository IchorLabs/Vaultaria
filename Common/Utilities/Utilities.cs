using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Setup.Configuration;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using System.IO;
using static System.Math;

namespace Vaultaria.Common.Utilities
{
    public static class Utilities
    {
        public static bool startedVault1BossRush = false;
        public static bool startedVault2BossRush = false;

        // An array of tiles that should be taken into consideration when trying to generate a vault when a world is made
        public static int[] badTiles =
        [
            // TileID.DemonAltar,
            TileID.BlueDungeonBrick,
            TileID.PinkDungeonBrick, 
            TileID.GreenDungeonBrick,
            TileID.CrackedBlueDungeonBrick,
            TileID.CrackedPinkDungeonBrick, 
            TileID.CrackedGreenDungeonBrick,
            TileID.Ebonstone,
            TileID.ShadowOrbs, 
            TileID.Crimstone,
            TileID.HoneyBlock,
            TileID.Hive, 
            TileID.LihzahrdBrick,
            TileID.LihzahrdAltar,
            TileID.HeavenforgeBrick,
            // TileID.Containers,
            // TileID.Containers2,
            // TileID.FakeContainers,
            // TileID.FakeContainers2,
            TileID.Sand,
            TileID.SandFallBlock,
            // TileID.Silt,
            // TileID.Slush,
            TileID.PlatinumBrick,
            TileID.AstraBrick,

            TileID.HeavyWorkBench,
            TileID.Bottles,
            TileID.OpenDoor,
            TileID.ClosedDoor,

            TileID.Glass,
        ];

        public static int[] badLiquids =
        [
            LiquidID.Shimmer,
            // LiquidID.Lava,
        ];

        public static ArrayList gunGunItemArray = new ArrayList();

        /// <summary>
        /// A wrapper method for the randomizer.
        /// <br/> To use chance, put in a float from 1 - 100. So if you put in 23.5, there would be a 23.5% chance of something happening.
        /// </summary>
        /// <param name="chance"></param>
        /// <returns>True if the randomizer picks a number within your range, and false otherwise.</returns>
        public static bool Randomizer(float chance)
        {
            if (Main.rand.Next(1, 101) <= chance)
            {
                return true;
            }

            return false;
        }
        
        public static void DisplayStatusMessage(Vector2 position, Color colour, string msg)
        {
            // Use MessageID.ChatText to send a chat message to all players.
            // remoteClient: -1 (all clients)
            // ignoreClient: -1 (no client ignored)
            // Used for multiplayer to send the message to everyone
            if(Main.netMode != NetmodeID.SinglePlayer)
            {
                NetMessage.SendData(MessageID.CombatTextString, -1, -1, NetworkText.FromLiteral(msg), (int) colour.PackedValue, position.X, position.Y);
            }

            // Display the text at the position
            CombatText.NewText(
                new Rectangle((int)position.X, (int)position.Y, 1, 1), 
                colour, // The color of the text (e.g., gold)
                msg, // The message you want to display
                dramatic: true, // Optional: Makes the text larger and appear more impactful
                dot: false
            );
        }

        public static bool IsWearing(Player player, int accessory)
        {
            // Ignore empty accessory slots and check if the player is wearing the accessory
            for (int i = 0; i < 8 + player.extraAccessorySlots; i++)
            {
                if (player.armor[i].ModItem != null && player.armor[i].ModItem.Type == accessory)
                {
                    return true;
                }
            }

            return false;
        }

        // // Used for most tile and wall checks to see if the current area is a vault
        // public static bool VaultArea(Point16 vaultDimensions, int positionX, int positionY, int i, int j)
        // {
        //     int topLeftCorner = positionX;
        //     int topRightCorner = positionX + vaultDimensions.X;
        //     int bottomLeftCorner = positionY;
        //     int bottomRightCorner = positionY + vaultDimensions.Y;

        //     if(i >= topLeftCorner && i < topRightCorner && j >= bottomLeftCorner && j < bottomRightCorner)
        //     {
        //         return true;
        //     }

        //     return false;
        // }

        // // Used just for KillWall() in VaultWalls.cs
        // public static void VaultArea(Point16 vaultDimensions, int positionX, int positionY, int i, int j, ref bool fail)
        // {
        //     int topLeftCorner = positionX;
        //     int topRightCorner = positionX + vaultDimensions.X;
        //     int bottomLeftCorner = positionY;
        //     int bottomRightCorner = positionY + vaultDimensions.Y;

        //     if(i >= topLeftCorner && i < topRightCorner && j >= bottomLeftCorner && j < bottomRightCorner)
        //     {
        //         fail = true;
        //     }
        // }
    }
}