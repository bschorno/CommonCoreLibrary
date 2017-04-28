using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using CommonCoreLibrary.Algorithm.Noise;

namespace Demo.Noise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //SimplexNoise noise = new SimplexNoise();
            //noise.Octaves = 1;
            //noise.Persistence = 0.5f;
            //noise.Scale = 0.01f;
            //noise.Redistribution = 1f;

            SimplexNoise no = new SimplexNoise();
            no.Scale = 0.1f;
            float[] noo = no.Generate(512);

            PerlinNoise noise = new PerlinNoise();
            noise.Octaves = 1;
            noise.Persistence = 0.5f;
            noise.Scale = 0.01f;
            noise.Redistribution = 1f;

            float[,] n = noise.Generate((int)imgMain.Width, (int)imgMain.Height);

            WriteableBitmap bitmap = new WriteableBitmap((int)imgMain.Width, (int)imgMain.Height, 96, 96, PixelFormats.Bgra32, null);
            uint[] pixels = new uint[(int)bitmap.Width * (int)bitmap.Height];

            for (int i = 0; i < n.GetLength(0); i++)
                for (int j = 0; j < n.GetLength(1); j++)
                {
                    int red = (int)((n[i, j] + 1) * 128);
                    int green = (int)((n[i, j] + 1 ) * 128);
                    int blue = (int)((n[i, j] + 1) * 128);
                    int alpha = 255;
                    pixels[(int)bitmap.Width * j + i] = (uint)((alpha << 24) + (blue << 16) + (green << 8) + (red << 0));
                    
                }
            bitmap.WritePixels(new Int32Rect(0, 0, (int)bitmap.Width, (int)bitmap.Height), pixels, (int)bitmap.Width * 4, 0);

            imgMain.Source = bitmap;
        }
    }
}
