using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Rectanglex
{
    class Polygon : IEnumerable<Point>
    {
        public static int VertexRadius { get; } = 5;

        private List<(Point P, bool? Horizontal)> Vertices = new List<(Point, bool?)>();
        public int VerticesCount { get => Vertices.Count; }
        public bool Finished { get; private set; } = false;

        public void MovePoint(int index, Point newLocation)
        {
            if (index < 0 || index >= VerticesCount)
                throw new ArgumentException();

            if (Vertices[index].Horizontal.HasValue)
            {
                if (Vertices[(index + VerticesCount - 1) % VerticesCount].Horizontal.HasValue)
                    return;
                else
                {
                    bool b = Vertices[index].Horizontal.Value;
                    if (b)
                        Vertices[index] = (new Point(newLocation.X, Vertices[index].P.Y), Vertices[index].Horizontal);
                    else
                        Vertices[index] = (new Point(Vertices[index].P.X, newLocation.Y), Vertices[index].Horizontal);
                }
            }
            else if (Vertices[(index + VerticesCount - 1) % VerticesCount].Horizontal.HasValue)
            {
                bool b = Vertices[(index + VerticesCount - 1) % VerticesCount].Horizontal.Value;
                if (b)
                    Vertices[index] = (new Point(newLocation.X, Vertices[index].P.Y), Vertices[index].Horizontal);
                else
                    Vertices[index] = (new Point(Vertices[index].P.X, newLocation.Y), Vertices[index].Horizontal);
            }
            else
                Vertices[index] = (newLocation, Vertices[index].Horizontal);
        }

        public void MoveEdge(int index, Point offset)
        {
            if (index < 0 || index >= VerticesCount)
                throw new ArgumentException();

            int i;
            int idx = (index + 1) % VerticesCount;
            Vertices[index] = (new Point(Vertices[index].P.X + offset.X, Vertices[index].P.Y + offset.Y), Vertices[index].Horizontal);
            Vertices[idx] = (new Point(Vertices[idx].P.X + offset.X, Vertices[idx].P.Y + offset.Y), Vertices[idx].Horizontal);
            for (i = 1; i < VerticesCount - 1; i++)
            {
                idx = (index + i) % VerticesCount;
                if (Vertices[idx].Horizontal.HasValue)
                {
                    int idx2 = (idx + 1) % VerticesCount;
                    Vertices[idx2] = (new Point(Vertices[idx2].P.X + offset.X, Vertices[idx2].P.Y + offset.Y), Vertices[idx2].Horizontal);
                }
                else
                    break;
            }
            if (i < VerticesCount - 1)
                for (i = 1; i < VerticesCount; i++)
                {
                    idx = (index + VerticesCount - i) % VerticesCount;
                    if (Vertices[idx].Horizontal.HasValue)
                        Vertices[idx] = (new Point(Vertices[idx].P.X + offset.X, Vertices[idx].P.Y + offset.Y), Vertices[idx].Horizontal);
                    else
                        break;
                }
        }

        public void MovePolygon(Point offset)
        {
            for (int i = 0; i < VerticesCount; i++)
            {
                Vertices[i] = (new Point(Vertices[i].P.X + offset.X, Vertices[i].P.Y + offset.Y), Vertices[i].Horizontal);
            }
        }

        public Point GetVertex(int index)
        {
            if (index < 0 || index >= VerticesCount)
                throw new ArgumentException();
            return Vertices[index].P;
        }

        public bool? GetVertexHorizontality(int index)
        {
            if (index < 0 || index >= VerticesCount)
                throw new ArgumentException();
            return Vertices[index].Horizontal;
        }

        public int FindVertex(Predicate<Point> predicate)
        {
            return Vertices.FindIndex(v => predicate.Invoke(v.P));
        }

        public void AddVertex(Point p)
        {
            if (Vertices.Count > 2 &&
                (p.X - Vertices[0].P.X)*(p.X - Vertices[0].P.X) + (p.Y - Vertices[0].P.Y)* (p.Y - Vertices[0].P.Y) < VertexRadius*VertexRadius)
            {
                Finished = true;
                return;
            }
            Vertices.Add((p, null));
        }

        public void AddVertex(Point p, int index)
        {
            if (index < 0 || index > VerticesCount)
                throw new ArgumentException();

            if (Vertices[(index + VerticesCount - 1) % VerticesCount].Horizontal.HasValue)
                SetHorizontality((index + VerticesCount - 1) % VerticesCount, null);
            Vertices.Insert(index, (p, null));
        }

        public void RemoveVertex(int index)
        {
            if (index >= Vertices.Count || index < 0)
                return;
            Vertices.RemoveAt(index);
        }

        public void Draw(Bitmap bmp)
        {
            Graphics G = Graphics.FromImage(bmp);

            var it = Vertices.GetEnumerator();
            it.MoveNext();
            G.FillEllipse(Brushes.LightGray, it.Current.P.X - VertexRadius, it.Current.P.Y - VertexRadius, 2 * VertexRadius, 2 * VertexRadius);
            (Point P, bool? Horizontal) p1 = it.Current;

            while(it.MoveNext())
            {
                G.FillEllipse(Brushes.LightGray, it.Current.P.X - VertexRadius, it.Current.P.Y - VertexRadius, 2 * VertexRadius, 2 * VertexRadius);
                Bresenham.Line(p1.P, it.Current.P, bmp);
                if (p1.Horizontal.HasValue)
                    DrawIcon(G, p1.P, it.Current.P, p1.Horizontal.Value);

                p1 = it.Current;
            }
            if (Finished)
            {
                Bresenham.Line(p1.P, Vertices[0].P, bmp);
                if (p1.Horizontal.HasValue)
                    DrawIcon(G, p1.P, Vertices[0].P, p1.Horizontal.Value);
            }

            G.Dispose();
        }
        private void DrawIcon(Graphics G, Point P1, Point P2, bool horizontal)
        {
            //if (horizontal)
            //    G.DrawIcon(new Icon("horizontal.ico"), (P1.X + P2.X) / 2, (P1.Y + P2.Y) / 2);
            //else
            //    G.DrawIcon(new Icon("vertical.ico"), (P1.X + P2.X) / 2, (P1.Y + P2.Y) / 2);
        }

        public void SetHorizontality(int index, bool? horizontal)
        {
            if (index < 0 || index >= VerticesCount)
                throw new ArgumentException();
            if (horizontal.HasValue)
            {
                (Point P, bool? Horizontal) V0 = Vertices[(index + VerticesCount - 1) % VerticesCount];
                (Point P, bool? Horizontal) V1 = Vertices[index];
                (Point P, bool? Horizontal) V2 = Vertices[(index + 1) % VerticesCount];
                if (V0.Horizontal == horizontal || V2.Horizontal == horizontal) // sprawdzenie sąsiednich krawędzi - czy któraś z nich ma już taką relację
                {
                    MessageBox.Show("Error");
                    return;
                }

                Point P;
                if (V2.Horizontal.HasValue)
                {
                    if (horizontal.Value)
                        P = new Point(V1.P.X, V2.P.Y);
                    else
                        P = new Point(V2.P.X, V1.P.Y);
                    Vertices[index] = (P, horizontal);
                }
                else
                {
                    int length = Convert.ToInt32(Math.Sqrt((V1.P.X - V2.P.X) * (V1.P.X - V2.P.X) + (V1.P.Y - V2.P.Y) * (V1.P.Y - V2.P.Y)));

                    if (horizontal.Value)
                        P = new Point((V1.P.X < V2.P.X ? V1.P.X + length : V1.P.X - length), V1.P.Y);
                    else
                        P = new Point(V1.P.X, (V1.P.Y < V2.P.Y ? V1.P.Y + length : V1.P.Y - length));

                    Vertices[(index + 1) % VerticesCount] = (P, Vertices[(index + 1) % VerticesCount].Horizontal);
                }
            }
            Vertices[index] = (Vertices[index].P, horizontal);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return Vertices.Select(v => v.P).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
