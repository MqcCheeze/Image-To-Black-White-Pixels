using System;
using System.Drawing;
using System.IO;

namespace ImageToBLPixels {
    public static class Program {

        public static void Main(String[] args) {

            if (args.Length != 0) {

                string filePath = args[0];
                Console.WriteLine("File found at - " + filePath);

                Image image;

                try {

                    image = Image.FromFile(filePath);
                } catch (ArgumentException) {

                    Console.WriteLine("File specified is not an image file...");
                    return;
                }

                try {

                    int blackPixels = 0;
                    int whitePixels = 0;

                    for (int y = 0; y < image.Height; y++) {
                        for (int x = 0; x < image.Width; x++) {
                            Color pixelColor = ((Bitmap)image).GetPixel(x, y);

                            int threshold = 128;
                            Color newColor = (pixelColor.R + pixelColor.G + pixelColor.B) / 3 < threshold ? Color.Black : Color.White;

                            if (pixelColor.ToArgb() == Color.Black.ToArgb()) {
                                blackPixels++;
                            } else {
                                whitePixels++;
                            }
                        }
                    }

                    Console.WriteLine("");
                    Console.WriteLine("Number of black pixels: " + blackPixels);
                    Console.WriteLine("Number of white pixels: " + whitePixels);

                    float blackToWhiteRatio = (float)blackPixels / whitePixels;
                    blackToWhiteRatio = (float)Math.Round(blackToWhiteRatio, 5);

                    Console.WriteLine("Roughly " + blackToWhiteRatio + " for every white pixel");
                } catch {

                    Console.WriteLine("Error while converting/calculating image...");
                    return;
                }
            } else {

                Console.WriteLine("No file detected...");
            }

            Console.ReadLine();
        }
    }
}