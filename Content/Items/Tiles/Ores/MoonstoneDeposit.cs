//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using Terraria;
//using Terraria.ID;
//using Terraria.Localization;
//using Terraria.ModLoader;
//using Terraria.ModLoader.IO;
//using Terraria.WorldBuilding;

//namespace Vaultaria.Content.Items.Tiles.Ores
//{
	//public class MoonstoneDeposit : ModTile
	//{
		//public override string Texture => "Vaultaria/Content/Items/Tiles/Ores/MoonstoneOre";

		//public override void SetStaticDefaults() {
			//TileID.Sets.Ore[Type] = true;
			//TileID.Sets.FriendlyFairyCanLureTo[Type] = true;
			//Main.tileSpelunker[Type] = true;
			//Main.tileOreFinderPriority[Type] = 410;
			//Main.tileShine2[Type] = true;
			//Main.tileShine[Type] = 975;
			//Main.tileMergeDirt[Type] = true;
			//Main.tileSolid[Type] = true;
			//Main.tileBlockLight[Type] = true;
			//Main.tileLighted[Type] = true;

			//LocalizedText name = CreateMapEntryName();
			//AddMapEntry(new Color(70, 165, 220), name);

			//DustType = DustID.Platinum;
			//VanillaFallbackOnModDeletion = TileID.Silver;
			//HitSound = SoundID.Tink;
		//}

		//public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
		//	float pulse = 0.8f + 0.2f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.5f);
		//	r = 0.18f * pulse;
		//	g = 0.5f * pulse;
		//	b = 0.7f * pulse;
		//}

		//public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor) {
		//	sightColor = Color.Cyan;
		//	return true;
		//}
	//}

	//public class MoonstoneDepositSystem : ModSystem
	//{
	//	private static bool generated;
	//	private static int generationTimer;

	//	public override void PostUpdateWorld() {
	//		if (Main.netMode == NetmodeID.MultiplayerClient || !Main.hardMode || generated) {
	//			return;
	//		}

	//		if (++generationTimer < 600) {
	//			return;
	//		}

	//		if (GenerateDeposits(40, 40, 100)) {
	//			generated = true;
	//		}
	//	}

	//	public override void SaveWorldData(TagCompound tag) {
	//		tag[nameof(generated)] = generated;
	//	}

	//	public override void LoadWorldData(TagCompound tag) {
	//		generated = tag.GetBool(nameof(generated));
	//	}

	//	public override void ClearWorld() {
	//		generated = false;
	//		generationTimer = 0;
	//	}

	//	public override void NetSend(BinaryWriter writer) {
	//		writer.Write(generated);
	//	}

	//	public override void NetReceive(BinaryReader reader) {
	//		generated = reader.ReadBoolean();
	//	}

		//private static bool IsHallowStone(int tileType) {
		//	return tileType == TileID.Pearlstone;
		//}

		//private static bool GenerateDeposits(int deposits, int minimumSteps, int maximumSteps) {
			//int oreType = ModContent.TileType<MoonstoneDeposit>();
			//List<Point> hallowTiles = new();
			//for (int x = 1; x < Main.maxTilesX - 1; x++) {
				//for (int y = (int)GenVars.rockLayer; y < Main.maxTilesY - 200; y++) {
				//	if (Main.tile[x, y].HasTile && IsHallowStone(Main.tile[x, y].TileType)) {
					//	hallowTiles.Add(new Point(x, y));
					//}
				//}
			//}

			//if (hallowTiles.Count == 0) {
			//	return false;
			//}

			//int attempts = deposits * 2;
			//for (int generatedDeposits = 0; generatedDeposits < deposits && attempts-- > 0;) {
			//	Point start = hallowTiles[WorldGen.genRand.Next(hallowTiles.Count)];
			//	int x = start.X;
			//	int y = start.Y;

			//	if (!Main.tile[x, y].HasTile || !IsHallowStone(Main.tile[x, y].TileType)) {
			//		continue;
			//	}

			//	generatedDeposits++;
			//	int steps = WorldGen.genRand.Next(minimumSteps, maximumSteps + 1);
			//	for (int step = 0; step < steps; step++) {
			//		if (WorldGen.InWorld(x, y) && Main.tile[x, y].HasTile && IsHallowStone(Main.tile[x, y].TileType)) {
			//			WorldGen.KillTile(x, y, false, false, true);
			//			WorldGen.PlaceTile(x, y, oreType, mute: true, forced: true);
			//			WorldGen.SquareTileFrame(x, y);
			//			//if (Main.netMode == NetmodeID.Server) {
			//			//	NetMessage.SendTileSquare(-1, x, y, 1);
			//			//}
			//		}

			//		x = Utils.Clamp(x + WorldGen.genRand.Next(-1, 2), 1, Main.maxTilesX - 2);
			//		y = Utils.Clamp(y + WorldGen.genRand.Next(-1, 2), 1, Main.maxTilesY - 2);
			//	}
			//}

			//return true;
		//}
	//}
//}