using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Algorithm.Noise
{
    public class PerlinNoise : Noise
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PerlinNoise()
            : base()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seed">Seed</param>
        public PerlinNoise(int seed)
            : base(seed)
        {

        }

        /// <summary>
        /// Get 1D noise
        /// </summary>
        /// <param name="x">X-Point</param>
        /// <returns></returns>
        protected override float GetNoise(float x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get 2D noise
        /// </summary>
        /// <param name="x">X-Point</param>
        /// <param name="y">Y-Point</param>
        /// <returns></returns>
        protected override float GetNoise(float x, float y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get 3D noise
        /// </summary>
        /// <param name="x">X-Point</param>
        /// <param name="y">Y-Point</param>
        /// <param name="z">Z-Point</param>
        /// <returns></returns>
        protected override float GetNoise(float x, float y, float z)
        {
            throw new NotImplementedException();
        }
    }
}
