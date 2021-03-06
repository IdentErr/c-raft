﻿#region C#raft License
// This file is part of C#raft. Copyright C#raft Team 
// 
// C#raft is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as
// published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.
#endregion
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chraft.Entity;
using Chraft.Interfaces;
using Chraft.Net;
using Chraft.Plugins.Events.Args;
using Chraft.World.Blocks.Interfaces;

namespace Chraft.World.Blocks
{
    class BlockCrops : BlockBase, IBlockGrowable
    {
        public BlockCrops()
        {
            Name = "Crops";
            Type = BlockData.Blocks.Crops;
            IsAir = true;
            IsSingleHit = true;
            Opacity = 0x0;
        }

        public bool CanGrow(StructBlock block, Chunk chunk)
        {
            // Crops grow from 0x0 to 0x7
            if (chunk == null || block.MetaData == 0x07)
                return false;
            if (block.Coords.WorldY == 127)
                return false;
            /*byte blockAboveLight = block.World.GetBlockLight(block.Coords.WorldX, block.Coords.WorldY + 1,
                                                             block.Coords.WorldZ);
            if (blockAboveLight < 9)
                return false;*/
            return true;
        }

        protected override void DropItems(EntityBase who, StructBlock block)
        {
            LootTable = new List<ItemStack>();
            // TODO: Fully grown drops 1 Wheat & 0-3 Seeds. 0 seeds - very rarely
            if (block.MetaData == 7)
            {
                LootTable.Add(new ItemStack((short)BlockData.Items.Wheat, 1));
                sbyte seeds = (sbyte)block.World.Server.Rand.Next(3);
                if (seeds > 0)
                    LootTable.Add(new ItemStack((short)BlockData.Items.Seeds, seeds));
            }
            else if (block.MetaData >= 5)
            {
                sbyte seeds = (sbyte)block.World.Server.Rand.Next(3);
                if (seeds > 0)
                    LootTable.Add(new ItemStack((short)BlockData.Items.Seeds, seeds));
            }
            base.DropItems(who, block);
        }

        public void Grow(StructBlock block, Chunk chunk)
        {
            if (!CanGrow(block, chunk))
                return;

            UniversalCoords blockUp = UniversalCoords.FromWorld(block.Coords.WorldX, block.Coords.WorldY + 1, block.Coords.WorldZ);
            if (block.World.GetEffectiveLight(blockUp) < 9)
                return;

            // TODO: Check if the blocks nearby are hydrated and grow faster
            if (block.World.Server.Rand.Next(10) == 0)
            {
                block.MetaData++;
                chunk.SetData(block.Coords, block.MetaData);
            }
        }

        protected override bool CanBePlacedOn(EntityBase who, StructBlock block, StructBlock targetBlock, BlockFace targetSide)
        {
            if (!BlockHelper.Instance(targetBlock.Type).IsPlowed || targetSide != BlockFace.Up)
                return false;
            return base.CanBePlacedOn(who, targetBlock, targetBlock, targetSide);
        }

        public override void NotifyDestroy(EntityBase entity, StructBlock sourceBlock, StructBlock targetBlock)
        {
            if ((targetBlock.Coords.WorldY - sourceBlock.Coords.WorldY) == 1 &&
                targetBlock.Coords.WorldX == sourceBlock.Coords.WorldX &&
                targetBlock.Coords.WorldZ == sourceBlock.Coords.WorldZ)
                Destroy(targetBlock);
            base.NotifyDestroy(entity, sourceBlock, targetBlock);
        }
    }
}