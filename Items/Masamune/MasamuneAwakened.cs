using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace ANB.Items.Masamune
{
    internal class MasamuneAwakened : ModItem
    {
        Texture2D blade = ModContent.Request<Texture2D>("ANB/Items/Masamune/MasamuneAwakened_Blade").Value;
        Texture2D bladenil = ModContent.Request<Texture2D>("ANB/Items/Masamune/MasamuneAwakened_Blade_nil").Value;
        public override void SetStaticDefaults()
        {
            //Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(2, 5));
            //ItemID.Sets.AnimatesAsSoul[Item.type] = true;

            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            base.SetStaticDefaults();
        }
        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 40;
            Item.useTime = 18;
            Item.knockBack = 4;
            base.SetDefaults();
        }
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.End();
            var Shader = GameShaders.Armor.GetShaderFromItemId(ItemID.GreenFlameDye);
            //Shader.Apply();
            Shader.Apply(null, new DrawData(bladenil, position - new Vector2(1, 1), null, Color.Red, 0, blade.Size() * 0.5f, 1, SpriteEffects.None, 1));
            //Shader.UseTargetPosition(position);
            //position.X = (int)position.X;
            //position.Y = (int)position.Y;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, Shader.Shader, Main.UIScaleMatrix);

            spriteBatch.Draw(blade, position, new Rectangle(10, 0, 38, 42), Color.Black, 0, origin - new Vector2(10, 0), scale, SpriteEffects.None, 0f);
            //spriteBatch.Draw(blade, position + new Vector2(0, 0), new Rectangle(10, 0, 38, 42), Color.Black, 0, blade.Size() * 0.5f - new Vector2(8, 0), scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {/**
          * public static void EndBlendState(this SpriteBatch spriteBatch, bool ui = false)
    {
        spriteBatch.End();
        spriteBatch.Begin((!ui) ? SpriteSortMode.Immediate : SpriteSortMode.Deferred, BlendState.AlphaBlend, ui ? SamplerState.AnisotropicClamp : Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, ui ? Main.UIScaleMatrix : Main.GameViewMatrix.TransformationMatrix);
    }
          * 
          * 
            spriteBatch.End();
            foreach (KeyValuePair<string, MiscShaderData>  sh in GameShaders.Misc)
            {
                Main.NewText(sh.Key);
            }
            //var sss = GameShaders.Misc["ForceField"];
            //sss.Apply(new DrawData(blade, position, Color.White));
            var Shader = GameShaders.Armor.GetShaderFromItemId(ItemID.BrightTealDye);
            Shader.Apply(null, new DrawData(blade, position, Color.Black));
            //Shader.UseTargetPosition(position);
            
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, Shader.Shader, Main.UIScaleMatrix);
            
            spriteBatch.Draw(blade, position , null, Main.DiscoColor, 0, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
            */
            base.PostDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {//i am really sorry for this mess
            Vector2 position = Item.position - Main.screenPosition + new Vector2(Item.width / 2, Item.height - blade.Height * 0.5f + 2f);
            spriteBatch.End();
            var Shader = GameShaders.Armor.GetShaderFromItemId(ItemID.GreenFlameDye);
            //Shader.Apply();
            Shader.Apply(null, new DrawData(bladenil, position - new Vector2(1, 1), null, Color.Red, rotation, blade.Size() * 0.5f, 1, SpriteEffects.None, 1));
            //Shader.UseTargetPosition(position);
            //position.X = (int)position.X;
            //position.Y = (int)position.Y;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.Default, RasterizerState.CullCounterClockwise, Shader.Shader, Main.GameViewMatrix.TransformationMatrix);
            spriteBatch.Draw(blade, position+ new Vector2(0,0) , new Rectangle(10,0,38,42), Color.Black, rotation, blade.Size() * 0.5f - new Vector2(8,0), scale, SpriteEffects.None, 0f);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);

            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
    }
}
