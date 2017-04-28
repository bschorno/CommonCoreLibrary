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
            int xi = (int)x & 512;
            float xf = x - (int)x;
            float u = this.Fade(xf);

            return this.Lerp(u, this.Grad(this._permutation[xi], xf), this.Grad(this._permutation[xi + 1], xf - 1)) * 2;
        }

        /// <summary>
        /// Get 2D noise
        /// </summary>
        /// <param name="x">X-Point</param>
        /// <param name="y">Y-Point</param>
        /// <returns></returns>
        protected override float GetNoise(float x, float y)
        {
            int xi = (int)x & 512;
            int yi = (int)y & 512;
            float xf = x - (int)x;
            float yf = y - (int)y;
            float u = this.Fade(xf);
            float v = this.Fade(yf);

            int a = this._permutation[xi    ] + yi;
            int b = this._permutation[xi + 1] + yi;

            return this.Lerp(v, this.Lerp(u, this.Grad(this._permutation[a    ], xf, yf    ), this.Grad(this._permutation[b    ], xf - 1, yf    )),
                                this.Lerp(u, this.Grad(this._permutation[a + 1], xf, yf - 1), this.Grad(this._permutation[b + 1], xf - 1, yf - 1)));
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
            int xi = (int)x & 512;
            int yi = (int)y & 512;
            int zi = (int)z & 512;
            float xf = x - (int)x;
            float yf = y - (int)y;
            float zf = z - (int)z;
            float u = this.Fade(xf);
            float v = this.Fade(yf);
            float w = this.Fade(zf);

            int a  = this._permutation[xi    ] + yi;
            int b  = this._permutation[xi + 1] + yi;
            int aa = this._permutation[a     ] + zi;
            int ba = this._permutation[b     ] + zi;
            int ab = this._permutation[a  + 1] + zi;
            int bb = this._permutation[b  + 1] + zi;

            return this.Lerp(w, this.Lerp(v, this.Lerp(u, this.Grad(this._permutation[aa    ], xf, yf    , zf    ), this.Grad(this._permutation[ba    ], xf - 1, yf    , zf    )),
                                             this.Lerp(u, this.Grad(this._permutation[ab    ], xf, yf - 1, zf    ), this.Grad(this._permutation[bb    ], xf - 1, yf - 1, zf    ))),
                                this.Lerp(v, this.Lerp(u, this.Grad(this._permutation[aa + 1], xf, yf    , zf - 1), this.Grad(this._permutation[ba + 1], xf - 1, yf    , zf - 1)),
                                             this.Lerp(u, this.Grad(this._permutation[ab + 1], xf, yf - 1, zf - 1), this.Grad(this._permutation[bb + 1], xf - 1, yf - 1, zf - 1))));
        }

        /// <summary>
        /// Fade
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private float Fade(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        /// <summary>
        /// Lerp
        /// </summary>
        /// <param name="t"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private float Lerp(float t, float a, float b)
        {
            return a + t * (b - a);
        }
    }
}
