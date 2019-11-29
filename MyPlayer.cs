using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Microstructures
{
    public class MyPlayer : ModPlayer
    {

        public bool gemPickaxe = false;
        public override void ResetEffects()
        {
            gemPickaxe = false;
        }

    }
}
