using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rectanglex
{
    public partial class Form1 : Form
    {

        private List<Polygon> Polygons = new List<Polygon>();
        private bool PolygonInProgress = false;
        private int MagnesRadius = 5;   //px

        private Polygon SelectedPolygon;
        private int SelectedVertexIndex = -1;
        private bool IsEdgeSelected = false;
        private Point MovingStartPoint;
        private bool PolygonInMove = false;

        public Form1()
        {
            InitializeComponent();

            Polygon P = new Polygon();
            P.AddVertex(new Point(155, 66));
            P.AddVertex(new Point(414, 66));
            P.AddVertex(new Point(659, 177));
            P.AddVertex(new Point(842, 177));
            P.AddVertex(new Point(842, 473));
            P.AddVertex(new Point(621, 473));
            P.AddVertex(new Point(621, 667));
            P.AddVertex(new Point(155, 319));
            P.AddVertex(new Point(155, 66));

            P.SetHorizontality(0, true);
            P.SetHorizontality(2, true);
            P.SetHorizontality(3, false);
            P.SetHorizontality(4, true);
            P.SetHorizontality(5, false);
            P.SetHorizontality(7, false);
            Polygons.Add(P);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox.Image = new Bitmap(pictureBox.ClientRectangle.Width, pictureBox.ClientRectangle.Height);
        }

        // sprawdza, czy kliknięto w jakiś wierzchołek. Jeśli tak - ustawia SelectedPolygon i SelectedVertex
        private void SetClickedVertex(Point p)
        {
            SelectedPolygon = Polygons.Find(polygon =>
            {
                SelectedVertexIndex = polygon.FindVertex(vertex => IsPointInCircle(vertex, p, Polygon.VertexRadius));
                return SelectedVertexIndex != -1;
            });
        }

        // sprawdza, czy kliknięto w jakąś krawędź
        private void SetClickedEdge(Point P)
        {
            bool vertexFound = false;
            foreach (Polygon polygon in Polygons)
            {
                IEnumerator<Point> it = polygon.GetEnumerator();
                it.MoveNext();

                int index = -1;
                Point P1 = it.Current, P2, P3;  // P3 - rzut punktu P na odcinek P1_P2; P1, P2 - punkty iterujące po wierzchołkach
                double u, distance;
                while (it.MoveNext())
                {
                    index++;
                    P2 = it.Current;
                    u = (double)((P2.X - P1.X) * (P.X - P1.X) + (P2.Y - P1.Y) * (P.Y - P1.Y)) / ((P2.X - P1.X) * (P2.X - P1.X) + (P2.Y - P1.Y) * (P2.Y - P1.Y));
                    if (u > 0.0 && u < 1.0)
                    {
                        P3 = new Point((int)(P1.X + u * (P2.X - P1.X)), (int)(P1.Y + u * (P2.Y - P1.Y)));
                        distance = Math.Sqrt((P.X - P3.X) * (P.X - P3.X) + (P.Y - P3.Y) * (P.Y - P3.Y));
                        if (distance < MagnesRadius)
                        {
                            SelectedPolygon = polygon;
                            SelectedVertexIndex = index;
                            vertexFound = true;
                            break;
                        }
                    }

                    P1 = P2;
                }
                if (!vertexFound)   // ostatnia iteracja w wielokącie została wydzielona
                {
                    P2 = polygon.GetVertex(0);
                    u = (double)((P2.X - P1.X) * (P.X - P1.X) + (P2.Y - P1.Y) * (P.Y - P1.Y)) / ((P2.X - P1.X) * (P2.X - P1.X) + (P2.Y - P1.Y) * (P2.Y - P1.Y));
                    if (u > 0.0 && u < 1.0)
                    {
                        P3 = new Point((int)(P1.X + u * (P2.X - P1.X)), (int)(P1.Y + u * (P2.Y - P1.Y)));
                        distance = Math.Sqrt((P.X - P3.X) * (P.X - P3.X) + (P.Y - P3.Y) * (P.Y - P3.Y));
                        if (distance < 10.0)
                        {
                            SelectedPolygon = polygon;
                            SelectedVertexIndex = ++index;
                            break;
                        }
                    }
                }
                else
                    break;
            }

        }

        private bool IsPointInCircle(Point p1, Point p2, int radius)
        {
            return (p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y) < radius * radius;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (PolygonInMove)
            {
                PolygonInMove = false;
                SelectedPolygon = null;
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                if (!PolygonInProgress)     // początek nowego wielokąta
                {
                    PolygonInProgress = true;
                    Polygons.Add(new Polygon());
                }
                int currPolygon = Polygons.Count - 1;
                Polygons[currPolygon].AddVertex(new Point(e.Location.X, e.Location.Y));

                if (Polygons[currPolygon].Finished)
                    PolygonInProgress = false;
            }

            if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Middle)
            {
                SetClickedVertex(e.Location);
                if (SelectedPolygon is null)    // nie kliknięto w wierzchołek, sprawdzam czy kliknięto w krawędź
                {
                    SetClickedEdge(e.Location);
                    if (!(SelectedPolygon is null))
                        IsEdgeSelected = true;
                }
                else if (IsEdgeSelected)
                    IsEdgeSelected = false;
            }

            if (e.Button == MouseButtons.Middle)
                MovingStartPoint = e.Location;

            Refresh();
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (PolygonInMove)
            {
                SelectedPolygon.MovePolygon(new Point(e.Location.X - MovingStartPoint.X, e.Location.Y - MovingStartPoint.Y));
                MovingStartPoint = e.Location;
                Refresh();
            }
            else if (MouseButtons == MouseButtons.Middle)
            {
                if (!(SelectedPolygon is null))
                {
                    if (IsEdgeSelected)
                    {
                        SelectedPolygon.MoveEdge(SelectedVertexIndex, new Point(e.Location.X - MovingStartPoint.X, e.Location.Y - MovingStartPoint.Y));
                    }
                    else
                    {
                        Point P1 = SelectedPolygon.GetVertex(SelectedVertexIndex);
                        SelectedPolygon.MovePoint(SelectedVertexIndex, new Point(P1.X + e.Location.X - MovingStartPoint.X, P1.Y + e.Location.Y - MovingStartPoint.Y));
                    }
                    MovingStartPoint = e.Location;
                    Refresh();
                }
            }
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                SelectedPolygon = null;
                IsEdgeSelected = false;
            }

            if (e.Button == MouseButtons.Right && !(SelectedPolygon is null))
            {
                if (IsEdgeSelected)
                    edgeContextMenuStrip.Show(pictureBox, e.Location);
                else
                    vertexContextMenuStrip.Show(pictureBox, e.Location);
            }
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (Polygons is null || Polygons.Count == 0)
                return;

            Bitmap newBmp = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            foreach (Polygon polygon in Polygons)
                polygon.Draw(newBmp);
            if (PolygonInProgress)
                Bresenham.Line(Polygons.Last().GetVertex(Polygons.Last().VerticesCount - 1), pictureBox.PointToClient(MousePosition), newBmp);

            pictureBox.Image.Dispose();
            pictureBox.Image = newBmp;
        }

        private void deleteVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedPolygon.VerticesCount > 3)
                SelectedPolygon.RemoveVertex(SelectedVertexIndex);
            else
                MessageBox.Show("You cannot remove this vertex.");
            SelectedPolygon = null;
        }

        private void addVertexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = (SelectedPolygon.GetVertex(SelectedVertexIndex).X + SelectedPolygon.GetVertex((SelectedVertexIndex + 1) % SelectedPolygon.VerticesCount).X) / 2;
            int y = (SelectedPolygon.GetVertex(SelectedVertexIndex).Y + SelectedPolygon.GetVertex((SelectedVertexIndex + 1) % SelectedPolygon.VerticesCount).Y) / 2;
            SelectedPolygon.AddVertex(new Point(x, y), SelectedVertexIndex + 1);

            SelectedPolygon = null;
            IsEdgeSelected = false;
        }

        private void stiffAngleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void movePolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovingStartPoint = pictureBox.PointToClient(MousePosition);
            PolygonInMove = true;
            IsEdgeSelected = false;
        }

        private void makeHorisontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedPolygon.SetHorizontality(SelectedVertexIndex, true);
            SelectedPolygon = null;
            IsEdgeSelected = false;
        }

        private void makeVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedPolygon.SetHorizontality(SelectedVertexIndex, false);
            SelectedPolygon = null;
            IsEdgeSelected = false;
        }

        private void removeRelationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectedPolygon.SetHorizontality(SelectedVertexIndex, null);
            SelectedPolygon = null;
            IsEdgeSelected = false;
        }

        private void edgeContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (SelectedPolygon.GetVertexHorizontality(SelectedVertexIndex).HasValue)
            {
                if (SelectedPolygon.GetVertexHorizontality(SelectedVertexIndex).Value)
                {
                    makeHorisontalToolStripMenuItem.Visible = false;
                    makeVerticalToolStripMenuItem.Visible = true;
                    removeRelationToolStripMenuItem.Visible = true;
                }
                else
                {
                    makeHorisontalToolStripMenuItem.Visible = true;
                    makeVerticalToolStripMenuItem.Visible = false;
                    removeRelationToolStripMenuItem.Visible = true;
                }
            }
            else
            {
                makeHorisontalToolStripMenuItem.Visible = true;
                makeVerticalToolStripMenuItem.Visible = true;
                removeRelationToolStripMenuItem.Visible = false;
            }
        }
    }
}
