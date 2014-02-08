﻿using System;
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
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using System.IO;

namespace doraemon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor sensor;
        WriteableBitmap depthBitmap;
        WriteableBitmap colorBitmap;
        DepthImagePixel[] depthPixels;
        byte[] colorPixels;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.sensor != null)
            {
                this.sensor.Stop();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var potentialSensor in KinectSensor.KinectSensors)
            {
                if (potentialSensor.Status == KinectStatus.Connected)
                {
                    this.sensor = potentialSensor;
                    break;
                }

                if (null == this.sensor)
                {
                    Console.WriteLine("No sensor found.");
                }

                if (null != this.sensor)
                {
                    this.sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    this.sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                    this.sensor.SkeletonStream.Enable();
                    this.colorPixels = new byte[this.sensor.ColorStream.FramePixelDataLength];
                    this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];
                    this.colorBitmap = new WriteableBitmap(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);
                    this.depthBitmap = new WriteableBitmap(this.sensor.DepthStream.FrameWidth, this.sensor.DepthStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);

                    this.sensor.AllFramesReady += sensor_AllFramesReady;

                    try
                    {
                        this.sensor.Start();
                    }
                    catch (IOException)
                    {
                        this.sensor = null;
                    }
                }
            }
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
                {
                    using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
                    {

                    }
                }
            }
        }
    }
}
