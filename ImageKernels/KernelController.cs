﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageKernels
{
    public class KernelController
    {
        public static double[,] Personalizado = { { 0, 0, 0 }, { 0, 1, 0 }, { 0, 0, 0 } };
        
        public static Bitmap applyKernel(Bitmap bmp, double[,] kernel)
        {
            Bitmap newBitmap = new Bitmap(bmp.Width, bmp.Height);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    int minX = x - 1;
                    int minY = y - 1;
                    int maxX = x + 1;
                    int maxY = y + 1;

                    //Solution to the image edges
                    minX = minX < 0 ? x : minX;
                    minY = minY < 0 ? y : minY;
                    maxX = maxX >= bmp.Width ? x : maxX;
                    maxY = maxY >= bmp.Height ? y : maxY;
                    
                    //Get Pixels to combine
                    Color pixel00 = bmp.GetPixel(minX, maxY);
                    Color pixel01 = bmp.GetPixel(x, maxY);
                    Color pixel02 = bmp.GetPixel(maxX, maxY);
                    Color pixel10 = bmp.GetPixel(minX, y);
                    Color pixel11 = bmp.GetPixel(x, y);
                    Color pixel12 = bmp.GetPixel(maxX, y);
                    Color pixel20 = bmp.GetPixel(minX, minY);
                    Color pixel21 = bmp.GetPixel(x, minY);
                    Color pixel22 = bmp.GetPixel(maxX, minY);
                    
                    //Pixel Operations
                    int pixelValueRed = (int)(
                        ((pixel00.R * kernel[0, 0]) + (pixel01.R * kernel[0, 1]) + (pixel02.R * kernel[0, 2]) +
                         (pixel10.R * kernel[1, 0]) + (pixel11.R * kernel[1, 1]) + (pixel12.R * kernel[1, 2]) +
                         (pixel20.R * kernel[2, 0]) + (pixel21.R * kernel[2, 1]) + (pixel22.R * kernel[2, 2]))
                    );

                    int pixelValueGreen = (int)(
                        ((pixel00.G * kernel[0, 0]) + (pixel01.G * kernel[0, 1]) + (pixel02.G * kernel[0, 2]) +
                         (pixel10.G * kernel[1, 0]) + (pixel11.G * kernel[1, 1]) + (pixel12.G * kernel[1, 2]) +
                         (pixel20.G * kernel[2, 0]) + (pixel21.G * kernel[2, 1]) + (pixel22.G * kernel[2, 2]))
                    );
                    int pixelValueBlue = (int)(
                        ((pixel00.B * kernel[0, 0]) + (pixel01.B * kernel[0, 1]) + (pixel02.B * kernel[0, 2]) +
                         (pixel10.B * kernel[1, 0]) + (pixel11.B * kernel[1, 1]) + (pixel12.B * kernel[1, 2]) +
                         (pixel20.B * kernel[2, 0]) + (pixel21.B * kernel[2, 1]) + (pixel22.B * kernel[2, 2]))
                    );

                    //Fix Limits
                    pixelValueRed = pixelValueRed < 0 ? 0 : pixelValueRed > 255 ? 255 : pixelValueRed;
                    pixelValueGreen = pixelValueGreen < 0 ? 0 : pixelValueGreen > 255 ? 255 : pixelValueGreen;
                    pixelValueBlue = pixelValueBlue < 0 ? 0 : pixelValueBlue > 255 ? 255 : pixelValueBlue;

                    //Set new value
                    Color newColor = Color.FromArgb(pixelValueRed, pixelValueGreen, pixelValueBlue);
                    newBitmap.SetPixel(x, y, newColor);
                }
            }
            return newBitmap;
        }

        public Color[][] GetBitMapColorMatrix(Bitmap bmp)
        {
            int hight = bmp.Height;
            int width = bmp.Width;

            Color[][] colorMatrix = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = bmp.GetPixel(i, j);
                }
            }
            return colorMatrix;
        }
    }
}
