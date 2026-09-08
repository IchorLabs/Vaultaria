using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace Vaultaria.Content.Items.Tiles.Ores
{
	public class EridiumOre : ModTile
	{
		public override void SetStaticDefaults() {
			TileID.Sets.Ore[Type] = true;
			TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
			Main.tileSpelunker[Type] = true; // The tile will be affected by spelunker highlighting
			Main.tileOreFinderPriority[Type] = 410; // Metal Detector value, see https://terraria.wiki.gg/wiki/Metal_Detector
			Main.tileShine2[Type] = true; // Modifies the draw color slightly.
			Main.tileShine[Type] = 975; // How often tiny dust appear off this tile. Larger is less frequently
			Main.tileMergeDirt[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;

			LocalizedText name = CreateMapEntryName();
			AddMapEntry(new Color(150, 82, 205), name);

			DustType = DustID.Platinum;
			VanillaFallbackOnModDeletion = TileID.Silver;
			HitSound = SoundID.Tink;
			// MineResist = 4f;
			// MinPick = 200;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
			float flicker = Main.rand.NextFloat(0.75f, 1.05f);
			r = 0.20f * flicker;
			g = 0.08f * flicker;
			b = 0.32f * flicker;
		}

		// Example of how to enable the Biome Sight buff to highlight this tile. Biome Sight is technically intended to show "infected" tiles, so this example is purely for demonstration purposes.
		public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor) {
			sightColor = Color.Blue;
			return true;
		}
	}

	public class EridiumOreSystem : ModSystem
	{
		public static LocalizedText EridiumOrePassMessage { get; private set; }

		public override void SetStaticDefaults() {
			EridiumOrePassMessage = Mod.GetLocalization($"WorldGen.{nameof(EridiumOrePassMessage)}");
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight) {
			int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
			if (shiniesIndex != -1) {
				tasks.Insert(shiniesIndex + 1, new EridiumOrePass("Eridium Ore", 1f));
			}
		}

		public static void GenerateOreVeins() {
			int oreType = ModContent.TileType<EridiumOre>();
			int minimumY = (int)GenVars.rockLayer + 20;
			int maximumY = Main.maxTilesY - 220;
			int attempts = Main.maxTilesX / 10;

			for (int attempt = 0; attempt < attempts; attempt++) {
				int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
				int y = WorldGen.genRand.Next(minimumY, maximumY);
				float depth = (y - minimumY) / (float)(maximumY - minimumY);
				float spawnChance = 0.03f + 0.12f * depth * depth;

				if (WorldGen.genRand.Next(10000) >= spawnChance * 10000f) {
					continue;
				}

				int veinSize = WorldGen.genRand.Next(13, 36);
				PlaceOreVein(x, y, veinSize, oreType);
			}
		}

		private static bool PlaceOreVein(int centerX, int centerY, int veinSize, int oreType) {
			List<Point> candidates = new();
			for (int offsetY = -3; offsetY <= 3; offsetY++) {
				for (int offsetX = -5; offsetX <= 5; offsetX++) {
					float horizontal = offsetX / 5f;
					float vertical = offsetY / 3f;
					if (horizontal * horizontal + vertical * vertical > 1f) {
						continue;
					}

					int x = centerX + offsetX;
					int y = centerY + offsetY;
					if (WorldGen.InWorld(x, y) && Main.tile[x, y].HasTile && Main.tile[x, y].TileType == TileID.Stone) {
						candidates.Add(new Point(x, y));
					}
				}
			}

			if (candidates.Count < veinSize) {
				return false;
			}

			candidates.Sort((first, second) =>
				(first.X - centerX) * (first.X - centerX) + (first.Y - centerY) * (first.Y - centerY)
				- ((second.X - centerX) * (second.X - centerX) + (second.Y - centerY) * (second.Y - centerY)));

			for (int index = 0; index < veinSize; index++) {
				Point point = candidates[index];
				WorldGen.KillTile(point.X, point.Y, false, false, true);
				WorldGen.PlaceTile(point.X, point.Y, oreType, mute: true, forced: true);
				WorldGen.SquareTileFrame(point.X, point.Y);
			}

			return true;
		}

	}

	public class EridiumOrePass : GenPass
	{
		public EridiumOrePass(string name, float loadWeight) : base(name, loadWeight) {
		}

		protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration) {
			progress.Message = EridiumOreSystem.EridiumOrePassMessage.Value;
			EridiumOreSystem.GenerateOreVeins();
		}
	}
}
