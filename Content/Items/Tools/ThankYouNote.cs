using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vaultaria.Content.Items.Tools
{
	public class ThankYouNote : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Blue;
			Item.value = Item.buyPrice(silver: 1);
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			if (Main.keyState.IsKeyDown(Keys.LeftShift))
			{
				AddText(tooltips, "A Personal Note from Demiurge.");
				AddText(tooltips, "I wanted to take a moment to leave a personal note for all of you.");
				AddText(tooltips, "");
				AddText(tooltips, "This is the first update that I have developed completely on my own.\nUp until this point, all of the coding was handled by Doctor Zeus,\nthe creator of the mod. Taking on development myself has been a new\nand exciting experience, and I hope I was able to bring some meaningful\nquality-of-life improvements to the mod with this update.");
				AddText(tooltips, "");
				AddText(tooltips, "More importantly, I hope this update leaves you wanting more, because I have a lot planned for all of you.");
				AddText(tooltips, "");
				AddText(tooltips, "My goal is for this mod to become THE mod for Borderlands fans\nwho want to experience that familiar world and gameplay within Terraria.\nThere is still a long way to go, but I am incredibly excited about\nthe direction we're heading.");
				AddText(tooltips, "");
				AddText(tooltips, "I strongly encourage everyone to join our Discord and share your\nhonest feedback, suggestions, and ideas. Your input is incredibly\nvaluable and will help shape the future of the mod.");
				AddText(tooltips, "");
				AddText(tooltips, "Thank you for playing, supporting, and following along with the\ndevelopment. There's a lot more to come.");
				AddText(tooltips, "");
				AddText(tooltips, "— Demiurge");
				return;
			}

			AddText(tooltips, "Thank YOU! for playing our mod. It means a lot to us!\nWe hope you enjoy everything Update v1.2 has in store for you!\n\nFor a full overview of the changes introduced in Update v1.2,\nplease visit our Discord or Workshop page. Alongside the release\nof this update, we are also preparing an internal roadmap outlining\nour plans all the way through to version 2.0 and beyond!\n\nBy version 2.0, you can look forward to a complete rework of the Vaults,\ncustom music tracks, skill trees, and much more. There is plenty more\nto come, and we are excited to share our plans as development continues.\nAnd for a personal note from our Spriter, and Vault Architect hold Left-Shift!.");
			AddText(tooltips, "Hold Left-Shift to see more information.", Color.Gray);
		}

		private void AddText(List<TooltipLine> tooltips, string text, Color? color = null)
		{
			tooltips.Add(new TooltipLine(Mod, "ThankYouNote", text)
			{
				OverrideColor = color
			});
		}
	}
}
