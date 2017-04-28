using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonCoreLibrary.Algorithm.Noise
{
    public abstract class Noise
    {
        protected int       _seed           = 1;
        protected int       _octaves        = 1;
        protected float     _persistence    = 0.5f;
        protected float     _scale          = 1.0f;
        protected float     _redistribution = 1.0f;
        protected Random    _random;
        protected byte[]    _permutation;

        private static readonly byte[] PermutationOriginal = new byte[]
        {
            253,239, 64, 72,202,175, 39,211, 30,164,148,240,249,116,203, 59,147,211, 98,202,236, 40,142,150, 16,109, 43, 74,106,191, 56,245,
            147,179,216, 67,228, 15,252,157,233,244, 62,185,136,132,217,151,108, 22, 45,138,195, 29, 10,212, 96,169, 55, 92,132,220,104, 95,
            108,158, 92, 69,155,135, 86,131,215,196,225,247,234,212, 14,140,148,147,222, 83,146,173, 50,117,169,  3,226, 59,215,160, 24, 13,
            140,222, 76,233, 39,147,252,228,192,169,179,254, 60, 18,234,140,155,171,199,241,253, 20,205,191,107,230,156,151,164, 27,171, 55,
            171,211,107, 11,135, 36,101,  1,150,250,147,232,249, 52, 57,150, 25, 86,159, 44,216, 39, 16,119, 17,141,125, 64,247,100, 64,164,
            253,123, 82,105,167,222,  4,118, 69,198, 90,  2,129,228,197, 55,173,  5,126, 77,123,127, 95,132,195,243,249,250,166, 36, 10, 50,
            186,238,234,126,225,207,239, 58, 82, 40,230, 17,205, 13,246, 45,199, 70,146,241,229,242, 40,126, 28,205,165,179,228,  9,208, 33,
            188, 39, 72,146,249, 71, 85,221, 22, 68,251, 83,152, 77,183,181,253,204, 50, 96,146, 24, 77,199,197,107, 97, 20, 42, 59, 85, 72,
             88,197, 85,165,196, 99, 51,128,240,180,218,160,246,213,230,206, 22,239,182, 24,214,111,142,163,218,129,205,142,229, 65,179,218,
            239,179,250,210,248, 17,241,153,153,178,111,221, 62,172,209,240,133,146,253, 58,133, 60,113,232, 54,177,202, 67,150,165,154,174,
              0,255,177, 67,219, 20,214,  4,254, 28,127, 36,255,177,101, 45,176,220,  9, 82, 68,121, 84,178, 66, 17,155, 70, 70,174,  3,243,
             23,192,220, 62,251,141, 21,112,188,248, 60,105,189, 76,232,133, 76,157,146,185,156, 72,106,135,170,254,  0,201,120,143,189, 79,
             24,139, 12, 63,212, 38, 50, 34,199,152,150, 75, 61,235,244,245, 40, 21,250,169, 27, 73, 87,207, 86, 85,148,253,140, 75,248, 44,
             58,236, 80, 50, 64,169,177, 38,107,112,241,  4,177, 31, 74,191,  9, 11,160, 98,149, 20, 52,207, 52, 60,232,126,145, 53,149,124,
            159,105, 16, 81,154,194,180,126,100,211, 62,171, 87, 82, 30, 54, 11,213,243,129,170,150,150, 38,183,129,253,  4,192, 51,148, 53,
            219,117,101, 71, 14,248, 36, 71, 86, 33,254,193,213,137,119,225, 30,178,115,188, 65, 19,209,  8,211,233,153, 25,197,176,189, 49
        };

        /// <summary>
        /// Seed
        /// </summary>
        public int Seed
        {
            get
            {
                return this._seed;
            }
            set
            {
                this._seed = value;
                if (this._seed == 0)
                {
                    this._permutation = PermutationOriginal;
                }
                else
                {
                    this._permutation = new byte[512];
                    new Random(this._seed).NextBytes(this._permutation);
                }
            }
        }

        /// <summary>
        /// Octaves
        /// </summary>
        public int Octaves
        {
            get
            {
                return this._octaves;
            }
            set
            {
                this._octaves = value;
            }
        }

        /// <summary>
        /// Persistence
        /// </summary>
        public float Persistence
        {
            get
            {
                return this._persistence;
            }
            set
            {
                this._persistence = value;
            }
        }

        /// <summary>
        /// Scale
        /// </summary>
        public float Scale
        {
            get
            {
                return this._scale;
            }
            set
            {
                this._scale = value;
            }
        }

        /// <summary>
        /// Redistribution
        /// </summary>
        public float Redistribution
        {
            get
            {
                return this._redistribution;
            }
            set
            {
                this._redistribution = value;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Noise()
        {
            _permutation = PermutationOriginal;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="seed">Seed</param>
        public Noise(int seed)
        {
            this._seed = seed;
            this._permutation = new byte[512];
            new Random(this._seed).NextBytes(this._permutation);
        }
        
        /// <summary>
        /// Generate 1D noise
        /// </summary>
        /// <param name="x">X-Size</param>
        /// <returns>Noise</returns>
        public virtual float[] Generate(int x)
        {
            float[] var1 = new float[x];
            for (int i = 0; i < x; i++)
            {
                float var2 = 0f;
                float var3 = 1f; //Frequency
                float var4 = 1f; //Amplitude 
                float var5 = 0f; //Max. amplitude
                for (int l = 0; l < this._octaves; l++)
                {
                    var2 += this.GetNoise(i * this._scale * var3) * var4;
                    var5 += var4;
                    var3 *= 2.0f;
                    var4 *= this._persistence;
                }
                var1[i] = (float)Math.Pow(var2, this._redistribution) / var5;
            }
            return var1;
        }

        /// <summary>
        /// Generate 2D noise
        /// </summary>
        /// <param name="x">X-Size</param>
        /// <param name="y">Y-Size</param>
        /// <returns>Noise</returns>
        public virtual float[,] Generate(int x, int y)
        {
            float[,] var1 = new float[x, y];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    float var2 = 0f;
                    float var3 = 1f; //Frequency
                    float var4 = 1f; //Amplitude 
                    float var5 = 0f; //Max. amplitude
                    for (int l = 0; l < this._octaves; l++)
                    {
                        var2 += this.GetNoise(i * this._scale * var3, j * this._scale * var3) * var4;
                        var5 += var4;
                        var3 *= 2.0f;
                        var4 *= this._persistence;
                    }
                    var1[i, j] = (float)Math.Pow(var2, this._redistribution) / var5;
                }
            return var1;
        }              
                       
        /// <summary>
        /// Generate 3D noise
        /// </summary>
        /// <param name="x">X-Size</param>
        /// <param name="y">Y-Size</param>
        /// <param name="z">Z-Size</param>
        /// <returns>Noise</returns>
        public virtual float[,,] Generate(int x, int y, int z)
        {
            float[,,] var1 = new float[x, y, z];
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < z; k++)
                    {
                        float var2 = 0;
                        float var3 = 1; //Frequency
                        float var4 = 1; //Amplitude 
                        float var5 = 0; //Max. amplitude
                        for (int l = 0; l < this._octaves; l++)
                        {
                            var2 += this.GetNoise(i * this._scale * var3, j * this._scale * var3, k * this._scale * var3) * var4;
                            var5 += var4;
                            var3 *= 2;
                            var4 *= this._persistence;
                        }
                        var1[i, j, j] = (float)Math.Pow(var2, this._redistribution) / var5; 
                    }
            return var1;
        }

        /// <summary>
        /// Get 1D noise
        /// </summary>
        /// <param name="x">X-Point</param>
        /// <returns></returns>
        protected abstract float GetNoise(float x);

        /// <summary>
        /// Get 2D noise
        /// </summary>
        /// <param name="x">X-Point</param>
        /// <param name="y">Y-Point</param>
        /// <returns></returns>
        protected abstract float GetNoise(float x, float y);

        /// <summary>
        /// Get 3D noise
        /// </summary>
        /// <param name="x">X-Point</param>
        /// <param name="y">Y-Point</param>
        /// <param name="z">Z-Point</param>
        /// <returns></returns>
        protected abstract float GetNoise(float x, float y, float z);

        /// <summary>
        /// Get grad
        /// </summary>
        /// <param name="hash">Hash</param>
        /// <param name="x">X</param>
        /// <returns></returns>
        protected virtual float Grad(int hash, float x)
        {
            int h = hash & 15;
            float grad = 1.0f + (h & 7);
            if ((h & 8) != 0)
                grad = -grad;
            return grad * x;
            //return (hash & 1) == 0 ? x : -x;
        }

        /// <summary>
        /// Get grad
        /// </summary>
        /// <param name="hash">Hash</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <returns></returns>
        protected virtual float Grad(int hash, float x, float y)
        {
            //int h = hash & 7;
            //float u = h < 4 ? x : y;
            //float v = h < 4 ? y : x;
            //return ((h & 1) != 0 ? -u : u) + ((h & 2) != 0 ? -2.0f * v : 2.0f * v);
            return ((hash & 1) == 0 ? x : -x) + ((hash & 2) == 0 ? y : -y);
        }

        /// <summary>
        /// Get grad
        /// </summary>
        /// <param name="hash">Hash</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        /// <returns></returns>
        protected virtual float Grad(int hash, float x, float y, float z)
        {
            //int h = hash & 15;
            //float u = h < 8 ? x : y;
            //float v = h < 4 ? y : h == 12 || h == 14 ? x : z;
            //return ((h & 1) != 0 ? -u : u) + ((h & 2) != 0 ? -v : v);
            var h = hash & 15;
            var u = h < 8 ? x : y;
            var v = h < 4 ? y : (h == 12 || h == 14 ? x : z);
            return ((h & 1) == 0 ? u : -u) + ((h & 2) == 0 ? v : -v);
        }
    }
}
