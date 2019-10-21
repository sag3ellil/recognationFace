using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using detecteur_de_visage_0_;

namespace detecteur_de_visage_0_
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private bool camRun = false;
        public Mat mat = new Mat(640, 480, DepthType.Cv8U, 3);
        Image<Bgr, Byte> bgr = new Image<Bgr, byte>(640, 480);
        Image<Hsv, Byte> hsv = new Image<Hsv, byte>(640, 480);
        //
        double LCannyThreshold = 20;
        double cannyThresholdLinking = 20;
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
            /* string FName = GetOneFile(3, "visage");
             if (FName == "") return;*/
            IImage image;
            image = capture.QueryFrame(); //UMat version
            //
            long detectionTime;
            DetecteFace.Detect(
              image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",//il faut les mettre dans le \bin\x64\debug
              faces, eyes,
              out detectionTime);
            foreach (Rectangle face in faces)
                CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
            foreach (Rectangle eye in eyes)
                CvInvoke.Rectangle(image, eye, new Bgr(Color.Blue).MCvScalar, 2);
            pictureBox1.Image = image.Bitmap;
           
           
        }



        private void button1_Click(object sender, EventArgs e)
        {
          /*  List<Rectangle> faces = new List<Rectangle>();
            List<Rectangle> eyes = new List<Rectangle>();
           /* string FName = GetOneFile(3, "visage");
            if (FName == "") return;
            IImage image;
            image = capture.QueryFrame(); //UMat version
            //
            long detectionTime;
            DetecteFace.Detect(
              image, "haarcascade_frontalface_default.xml", "haarcascade_eye.xml",//il faut les mettre dans le \bin\x64\debug
              faces, eyes,
              out detectionTime);
            foreach (Rectangle face in faces)
                CvInvoke.Rectangle(image, face, new Bgr(Color.Red).MCvScalar, 2);
            foreach (Rectangle eye in eyes)
                CvInvoke.Rectangle(image, eye, new Bgr(Color.Blue).MCvScalar, 2);
            pictureBox1.Image = image.Bitmap;*/



            if (capture == null)
            {
                try
                {
                    capture = new VideoCapture(0);
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                    return;
                }
            }
            //02: Start/Stop de la capture
            if (camRun)
            {
                button1.Text = "start!";//
                Application.Idle -= ProcessFrame;
                camRun = false;
            }
            else
            {
                button1.Text = "Stop";
                Application.Idle += ProcessFrame;
                camRun = true;
            }
        }

      
    }
}

