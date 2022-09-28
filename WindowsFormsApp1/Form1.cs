using System;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {


        double ScreenW, ScreenH;

        public double x, y, h, X1, Y1, f, g, k1, k2, k3, k4, l1, l2, l3, l4, M, T, num;
        int n;

        double[,] mass;
        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Draw();
        }

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "N";
            dataGridView1.Columns[1].Name = "X";
            dataGridView1.Columns[2].Name = "Y";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            {
                Glut.glutInit();
                Glut.glutInitDisplayMode(Glut.GLUT_RGBA | Glut.GLUT_DOUBLE);
                Gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
                Gl.glMatrixMode(Gl.GL_PROJECTION);
                Gl.glLoadIdentity();

                

                Glu.gluOrtho2D(-15,15,-15,15);

                Gl.glMatrixMode(Gl.GL_MODELVIEW);
                Gl.glLoadIdentity();
            }
        }

        double Function_g(double a, double b) => M * a - b - a * (Math.Pow(a, 2) + Math.Pow(b, 2));

        double Function_f(double a, double b) => a + M * b - b * (Math.Pow(a, 2) + Math.Pow(b, 2));

        public void Calculation()
        {
            dataGridView1.Rows.Clear();

            T = (double)trackBar2.Value;
            textBox1.Text = trackBar2.Value.ToString();
            n = trackBar3.Value;
            textBox2.Text = trackBar3.Value.ToString();
            x = (double)trackBar4.Value / 100.0;
            textBox3.Text = trackBar4.Value.ToString();
            y = (double)trackBar5.Value / 100.0;
            textBox4.Text = trackBar5.Value.ToString();
            M = (double)trackBar6.Value / 100.0;
            textBox5.Text = trackBar6.Value.ToString();

            num = 0;
            h = T / n;
            mass = new double[(n + 1), 2];
            mass[0, 0] = x;
            mass[0, 1] = y;

            for (int i = 1; i <= n; i++)
            {
                k1 = h * Function_f(mass[i - 1, 0], mass[i - 1, 1]);
                l1 = h * Function_g(mass[i - 1, 0], mass[i - 1, 1]);

                k2 = h * Function_f(mass[i - 1, 0] + k1 / 2, mass[i - 1, 1] + l1 / 2);
                l2 = h * Function_g(mass[i - 1, 0] + k1 / 2, mass[i - 1, 1] + l1 / 2);

                k3 = h * Function_f(mass[i - 1, 0] + k2 / 2, mass[i - 1, 1] + l2 / 2);
                l3 = h * Function_g(mass[i - 1, 0] + k2 / 2, mass[i - 1, 1] + l2 / 2);

                k4 = h * Function_f(mass[i - 1, 0] + k3, mass[i - 1, 1] + l3);
                l4 = h * Function_g(mass[i - 1, 0] + k3, mass[i - 1, 1] + l3);

                mass[i, 0] = mass[i - 1, 0] + (k1 + 2 * k2 + 2 * k3 + k4) / 6.0;
                mass[i, 1] = mass[i - 1, 1] + (l1 + 2 * l2 + 2 * l3 + l4) / 6.0;

                double fRez = (mass[i, 0]);
                double gRez = (mass[i, 1]);
                double xRez = (0 + i);

                dataGridView1.Rows.Add(xRez, fRez, gRez);
               
                num++;
            }
        }
        public void Draw()
        {
            Calculation();

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
    
            Gl.glBegin(Gl.GL_LINE_STRIP);
            Gl.glColor3f(1.0f, 0, 0);
            
            for (int i = 0; i < mass.GetLength(0); i += 2)
            {
                Gl.glVertex2d(mass[i, 0], mass[i, 1]);
            }
            Gl.glEnd();

            Gl.glBegin(Gl.GL_LINES);
            Gl.glColor3f(0, 0, 0);
            Gl.glVertex2d(0, -15);
            Gl.glVertex2d(0, 15);
            Gl.glVertex2d(-15, 0);
            Gl.glVertex2d(15, 0);
            Gl.glEnd();
            Gl.glFlush();

            AnT.Invalidate();
        }
    }
}