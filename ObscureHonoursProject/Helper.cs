using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObscureHonoursProject
{
    class Helper
    {
        static Random random = new Random();

        public static int GetRandomInt(int max)
        {
            return random.Next(max);
        }

        /// <summary>
        /// Get random number
        /// </summary>
        /// <returns>\result = [0, ..., 1]</returns>
        public static double GetRandom()
        {
            return random.NextDouble();
        }
    }
}
