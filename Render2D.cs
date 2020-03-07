using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

namespace  Render2D
{

    class Program
    {
        public static void Main(string[] args)
        {
            Screen s = new Screen();
            s.CreateScreen(640,480);
            Application.Run(s);
           // s.DrawPixel(5,5,Color.Red);
        }
    }

    class Screen : Form
    {
        private PictureBox pb;
        private Bitmap rendTex;
        private Screen Instance = null;
        System.Timers.Timer t; 
        private int x = 0;
        private int y = 0;
        private float dist = 5;
        
        public Screen(){}


        public Screen CreateScreen(int h,int w)
        {
            if(Instance == null)
            {
                Height = h;
                Width = w;
                pb =  new PictureBox();
                pb.Width = w;
                pb.Height = h;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                Instance = this;
                InitSreen();
                Activated += FrameUpdate; 
            }
          
            return Instance;
        }

        public void InitSreen()
        {
            rendTex = new Bitmap(pb.Height,pb.Width);

            for(int i = 0; i < rendTex.Height-1; i++)
            {
                for(int k = 0; k < rendTex.Width-1; k++)
                {
                    rendTex.SetPixel(k,i,Color.Black);
                }
            }

            pb.Image = rendTex;
            Controls.Add(pb);

        }

        public void FrameUpdate(object sender, System.EventArgs e)
        {

            for(dist = 5; dist >=0; dist--)
            {
                OnTimedEvent();
            }

   

        }

        private void OnTimedEvent()
        {
           // Console.WriteLine("Line Drawing");
         
            triangle t1;
            t1.v = new Vertex[3];
            t1.v[0].point = new Vector3D(10,10,5);
            t1.v[1].point = new Vector3D(50,50,5);
            t1.v[2].point = new Vector3D(100,50,5);
           
            triangle t2;
            t2.v = new Vertex[3];
            t2.v[0].point = new Vector3D(10,10,5);
            t2.v[1].point = new Vector3D(80,20,5);
            t2.v[2].point = new Vector3D(100,50,5);

            Mesh box;
            box.triangles = new triangle[2]{t1,t2};
            DrawMesh(box);

           // Console.WriteLine("Line Drawn");
           Console.ReadKey();
        }

        public void DrawMesh(Mesh m)
        {
            foreach (triangle t in m.triangles)
            {
                DrawPoly(t);
            }
        }

        public void DrawPoly(triangle tri)
        {
            Line(tri.v[0].point,tri.v[1].point);
            Line(tri.v[1].point,tri.v[2].point);
            Line(tri.v[0].point,tri.v[2].point);
        }

        public void Line(Vector3D start,Vector3D end)
        {   
            
            float x = 0;
            float delta = 0;
            float y = 0;

            float x_pers_start = dist*start.x/start.z;
            float x_pers_end = dist*end.x/end.z;

            float y_pers_start = dist*start.y/start.z;
            float y_pers_end = dist*end.y/end.z;
            
           // Console.WriteLine( end.x.ToString());
            for(x = start.x; x <= end.x;x ++)
            {
                //Console.WriteLine(x.ToString());
                if(x > x_pers_start)
                    delta = (x - x_pers_start)/(x_pers_end  - x_pers_start);
               // Console.WriteLine(delta.ToString());    
                y = (y_pers_end - y_pers_start)*delta + y_pers_start;
                
                DrawPixel((int)x,(int)y,Color.Red);
            }
        }

        

        public void DrawPixel(int x, int y,Color c)
        {
            rendTex.SetPixel(x,y,c);
            pb.Image = rendTex;
        }
          



    }

    public struct Vertex
    {
        public Vector3D point;
    }

    public struct triangle
    {
        public Vertex[] v;
    }
    
    public struct Mesh
    {
        public triangle[] triangles;
    }    
    

    public struct Vector3D
    {
        public float x;
        public float y;
        public float z;

        public Vector3D(float x,float y,float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

}
