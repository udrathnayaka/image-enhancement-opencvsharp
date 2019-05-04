using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Preprocessing
    {

        IplImage src, gray, binary, neg;
        IplImage oriRed,oriGreen,oriBlue;
        IplImage resultRed, resultGreen, resultBlue;
        IplImage resultedImage; 

        public void LoadOriginalImage(String fname)
        {
            src = Cv.LoadImage(fname, LoadMode.Color);
            Cv.SaveImage("1.jpg", src);
        }

        public void extract()
        {
            System.Windows.Forms.MessageBox.Show(" Height Of the Image: " + src.Height + " Width of the Image: " + src.Width + " No of the channels: " + src.NChannels);
        }



        public void ConvertToGray()
        {
           gray = Cv.CreateImage(src.Size, BitDepth.U8,1);
           Cv.CvtColor(src,gray,ColorConversion.RgbaToGray);
           Cv.SaveImage("2.jpg",gray);
        }

        public void ConvertGrayToBinary(int thresh)
        {
            ConvertToGray();
            binary = Cv.CreateImage(gray.Size, BitDepth.U8, 1);
            Cv.Threshold(gray,binary,thresh,255,ThresholdType.Binary);
            Cv.SaveImage("3.jpg", binary);
        }

        public void convertToNegative()
        {
            //Convert the Original to gray and then convert to Negative
            gray = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            Cv.CvtColor(src, gray, ColorConversion.RgbaToGray);
            neg = Cv.CreateImage(gray.Size,BitDepth.U8,1);
            Cv.Not(gray,neg);
            Cv.SaveImage("neg.jpg", neg);
            
            //Convert the Original to Negative directly
            //neg = Cv.CreateImage(src.Size,BitDepth.U8,3);
            //Cv.Not(src, neg);
            //Cv.SaveImage("neg.jpg", neg);
        }

        public void negative()
        {
            oriRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            Cv.Split(src, oriRed, oriBlue, oriGreen,null);

            resultRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            resultedImage = Cv.CreateImage(src.Size, BitDepth.U8, 3);

            for(int y=1;y<src.Height-1;y++)
            {
                for(int x = 1; x<src.Width-1; x++)
                {
                    double getRed = 0;
                    double getGreen = 0;
                    double getBlue = 0;

                   getRed= Cv.GetReal2D(oriRed, y, x);
                   getGreen=Cv.GetReal2D(oriGreen, y, x);
                   getBlue= Cv.GetReal2D(oriBlue, y, x);

                    double r = 255 - getRed;
                    double g = 255 - getGreen;
                    double b = 255 - getBlue;

                    Cv.SetReal2D(resultRed,y,x,r);
                    Cv.SetReal2D(resultGreen, y, x, g);
                    Cv.SetReal2D(resultBlue, y, x, b);
                }
            }

            Cv.Merge(resultRed, resultGreen, resultBlue,null,resultedImage);
            Cv.SaveImage("3.jpg", resultedImage);

        }

        public void log()
        {
            oriRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            Cv.Split(src, oriRed, oriBlue, oriGreen, null);

            resultRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            resultedImage = Cv.CreateImage(src.Size, BitDepth.U8, 3);

            for (int y = 1; y < src.Height - 1; y++)
            {
                for (int x = 1; x < src.Width - 1; x++)
                {
                    double getRed = 0;
                    double getGreen = 0;
                    double getBlue = 0;
                    double c = 105.886;

                    getRed = Cv.GetReal2D(oriRed, y, x);
                    getGreen = Cv.GetReal2D(oriGreen, y, x);
                    getBlue = Cv.GetReal2D(oriBlue, y, x);

                    double r = c * Math.Log(1+ getRed);
                    double g = c * Math.Log(1 + getGreen);
                    double b = c * Math.Log(1 + getBlue);

                    Cv.SetReal2D(resultRed, y, x, r);
                    Cv.SetReal2D(resultGreen, y, x, g);
                    Cv.SetReal2D(resultBlue, y, x, b);


                }
            }

            Cv.Merge(resultRed, resultGreen, resultBlue, null, resultedImage);
            Cv.SaveImage("4.jpg", resultedImage);

        }

        public void inverseLog()
        {
            oriRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            Cv.Split(src, oriRed, oriBlue, oriGreen, null);

            resultRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            resultedImage = Cv.CreateImage(src.Size, BitDepth.U8, 3);

            for (int y = 1; y < src.Height - 1; y++)
            {
                for (int x = 1; x < src.Width - 1; x++)
                {
                    double getRed = 0;
                    double getGreen = 0;
                    double getBlue = 0;
                    double c = 255* Math.Pow(10,-255);

                    getRed = Cv.GetReal2D(oriRed, y, x);
                    getGreen = Cv.GetReal2D(oriGreen, y, x);
                    getBlue = Cv.GetReal2D(oriBlue, y, x);

                    double r = c * Math.Pow(10, getRed);
                    double g = c * Math.Pow(10, getGreen);
                    double b = c * Math.Pow(10, getBlue);

                    Cv.SetReal2D(resultRed, y, x, r);
                    Cv.SetReal2D(resultGreen, y, x, g);
                    Cv.SetReal2D(resultBlue, y, x, b);


                }
            }

            Cv.Merge(resultRed, resultGreen, resultBlue, null, resultedImage);
            Cv.SaveImage("5.jpg", resultedImage);

        }

        public void gammaCorrection()
        {
            oriRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            Cv.Split(src, oriRed, oriBlue, oriGreen, null);

            resultRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            resultedImage = Cv.CreateImage(src.Size, BitDepth.U8, 3);

            for (int y = 1; y < src.Height - 1; y++)
            {
                for (int x = 1; x < src.Width - 1; x++)
                {
                    double getRed = 0;
                    double getGreen = 0;
                    double getBlue = 0;
                    double gamma = 0.5;
                    double c = 255 * Math.Pow(255, -gamma);

                    getRed = Cv.GetReal2D(oriRed, y, x);
                    getGreen = Cv.GetReal2D(oriGreen, y, x);
                    getBlue = Cv.GetReal2D(oriBlue, y, x);

                    double r = c * Math.Pow(getRed, gamma);
                    double g = c * Math.Pow(getGreen, gamma);
                    double b = c * Math.Pow(getBlue, gamma);

                    Cv.SetReal2D(resultRed, y, x, r);
                    Cv.SetReal2D(resultGreen, y, x, g);
                    Cv.SetReal2D(resultBlue, y, x, b);


                }
            }

            Cv.Merge(resultRed, resultGreen, resultBlue, null, resultedImage);
            Cv.SaveImage("6.jpg", resultedImage);

        }

        public void grayLevelSlicing()
        {
            oriRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            oriBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            Cv.Split(src, oriRed, oriBlue, oriGreen, null);

            resultRed = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultGreen = Cv.CreateImage(src.Size, BitDepth.U8, 1);
            resultBlue = Cv.CreateImage(src.Size, BitDepth.U8, 1);

            resultedImage = Cv.CreateImage(src.Size, BitDepth.U8, 3);

            for (int y = 1; y < src.Height - 1; y++)
            {
                for (int x = 1; x < src.Width - 1; x++)
                {
                    double getRed = 0;
                    double getGreen = 0;
                    double getBlue = 0;
                    double A = 175;
                    double B = 150;
                    
                    getRed = Cv.GetReal2D(oriRed, y, x);
                    getGreen = Cv.GetReal2D(oriGreen, y, x);
                    getBlue = Cv.GetReal2D(oriBlue, y, x);

                    if(getRed >= A && getRed <= B)
                    {
                        getRed = 255;
                    }
                    else
                    {
                        getRed = 20;
                    }

                    if (getGreen >= A && getGreen <= B)
                    {
                        getGreen = 255;
                    }
                    else
                    {
                        getGreen = 20;
                    }

                    if (getBlue >= A && getBlue <= B)
                    {
                        getBlue = 255;
                    }
                    else
                    {
                        getBlue = 20;
                    }

                    Cv.SetReal2D(resultRed, y, x, getRed);
                    Cv.SetReal2D(resultGreen, y, x, getGreen);
                    Cv.SetReal2D(resultBlue, y, x, getBlue);


                }
            }

            Cv.Merge(resultRed, resultGreen, resultBlue, null, resultedImage);
            Cv.SaveImage("7.jpg", resultedImage);

        }

    }

}


