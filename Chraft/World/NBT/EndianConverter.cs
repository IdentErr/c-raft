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
using System;
using System.Collections.Generic;
using System.Text;

namespace Chraft.World.NBT
{
    public static class EndianConverter
    {
        public static Int16 SwapInt16(Int16 value)
        {
            byte[] cVal = BitConverter.GetBytes(value);

            Array.Reverse(cVal);

            return BitConverter.ToInt16(cVal, 0);
        }

        public static Int32 SwapInt32(Int32 value)
        {
            byte[] cVal = BitConverter.GetBytes(value);

            Array.Reverse(cVal);

            return BitConverter.ToInt32(cVal, 0);
        }

        public static Int64 SwapInt64(Int64 value)
        {
            byte[] cVal = BitConverter.GetBytes(value);

            Array.Reverse(cVal);

            return BitConverter.ToInt64(cVal, 0);
        }

        public static Single SwapSingle(Single value)
        {
            byte[] cVal = BitConverter.GetBytes(value);

            Array.Reverse(cVal);

            return BitConverter.ToSingle(cVal, 0);
        }

        public static Double SwapDouble(Double value)
        {
            byte[] cVal = BitConverter.GetBytes(value);

            Array.Reverse(cVal);

            return BitConverter.ToDouble(cVal, 0);
        }
    }
}
