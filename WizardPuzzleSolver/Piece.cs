using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WizardPuzzleSolver
{
    public class Piece
    {
        Rectangle   m_rect;
        Points      m_points = new Points();
        string      m_name = "";
        List<Transformation> m_redundantTransformation = new List<Transformation>();
        public  Piece(Rectangle rect, string name)
        {
            m_rect = rect;
            m_name = name;
        }

        public bool Load(string desc)
        {
            string[] ptsDesc = desc.Split(';');

            foreach(string pDesc in ptsDesc)
            {
                string[] vals = pDesc.Split(',');

                if(vals.Length != 2)
                {
                    return false;
                }

                Point pt = new Point();

                pt.X = int.Parse(vals[0]);
                pt.Y = int.Parse(vals[1]);

                m_points.Add(pt);
            }

            SetupRedundantTransformations();

            return true;
        }
        private bool ComparePoints(Points p1, Points p2)
        {
            if(p1.Count != p2.Count)
            {
                return false;
            }

            foreach(Point p in p1)
            {
                if(!p2.Contains(p))
                {
                    return false;
                }
            }

            return true;
        }
        private void SetupRedundantTransformations()
        {
            List<Points> pointsList = new List<Points>();

            for (int i = 0; i < 2; i++)
            {
                foreach (Piece.Rotation rotation in Enum.GetValues(typeof(Piece.Rotation)))
                {
                    Points points = GetPoints(rotation, i == 1);
                    bool redundant = false;

                    foreach(Points oldPoints in pointsList)
                    {
                        if(ComparePoints(oldPoints, points))
                        {
                            Transformation trans = new Transformation();
                            trans.m_rotation = rotation;
                            trans.m_flip = i == 1;

                            m_redundantTransformation.Add(trans);
                            redundant = true;
                            break;
                        }
                    }

                    if(!redundant)
                    {
                        pointsList.Add(points);
                    }
                }
            }
        }
        public string GetName()
        {
            return m_name;
        }
        private Points Transform(ref Points points, Rotation rotation, bool flip)
        {
            int ox = m_rect.Width;
            int oy = m_rect.Height;
            int x0 = 0;
            int y0 = 0;
            bool first = true;

            foreach (Point pt in m_points)
            {
                if(first)
                {
                    x0 = pt.X;
                    y0 = pt.Y;

                    first = false;
                }
                int x1 = pt.X;
                int y1 = pt.Y;
                int aco = 0;
                int asi = 0;

                switch (rotation)
                {
                    case Rotation.UP:
                        aco = (int)Math.Cos(0);
                        asi = (int)Math.Sin(0);
                        break;
                    case Rotation.RIGHT:
                        aco = (int) Math.Cos(Math.PI / 2.0);  
                        asi = (int) Math.Sin(Math.PI / 2.0);
                        break;
                    case Rotation.DOWN:
                        aco = (int) Math.Cos(Math.PI);  
                        asi = (int) Math.Sin(Math.PI);
                        break;
                    case Rotation.LEFT:
                        aco = (int) Math.Cos(3.0 * Math.PI / 2.0);  
                        asi = (int) Math.Sin(3.0 * Math.PI / 2.0);
                        break;
                }
                int x2 = aco * (x1 - x0) - asi * (y1 - y0) + x0;
                int y2 = asi * (x1 - x0) + aco * (y1 - y0) + y0;


                ox = Math.Min(ox, x2);
                oy = Math.Min(oy, y2);

                points.Add(new Point(x2, y2));

            }


            if (flip)
            {
                ox = m_rect.Width;
                oy = m_rect.Height;

                for (int i = 0; i < points.Count; i++)
                {
                    Point pt = points[i];

                    pt.Y = -pt.Y;

                    points[i] = pt;

                    ox = Math.Min(pt.X, ox);
                    oy = Math.Min(pt.Y, oy);
                }
            }


            for (int i = 0; i < points.Count; i++)
            {
                Point pt = points[i];

                pt.X -= ox;
                pt.Y -= oy;

                points[i] = pt;
            }

            return points;
        }
        public bool RedundantTransformation(Rotation rotation, bool flip)
        {
            foreach(Transformation trans in m_redundantTransformation)
            {
                if(trans.m_rotation == rotation && trans.m_flip == flip)
                {
                    return true;
                }
            }
            return false;
        }
        public Points GetPoints(Rotation rotation, bool flip)
        {
            Points points = new Points();

            return Transform(ref points, rotation, flip);
        }

        public void Transform(Rotation rotation, bool flip)
        {
            Transform(ref m_points, rotation, flip);
        }


        public enum Rotation
        {
            UP = 0,
            RIGHT,
            DOWN,
            LEFT
        }
    }
    public class Points : List<Point>
    {}
    public class Transformation
    {
        public Piece.Rotation   m_rotation = Piece.Rotation.UP;
        public bool             m_flip = false;
    }
}
