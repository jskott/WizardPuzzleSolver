using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;

namespace WizardPuzzleSolver
{
    public class Board
    {
        Rectangle           m_rect;
        Pieces              m_pieces = new Pieces();
        int                 m_count = 0;
        Solutions           m_solutions = new Solutions();
        BackgroundWorker    m_worker = null;
        int                 m_max = 0;
        int                 m_step = 10;
        public Board(Pieces pieces, BackgroundWorker worker)
        {
            m_pieces = pieces;
            m_rect = new Rectangle(0, 0, pieces.Count, 5);
            m_worker = worker;
            m_max = (int) Math.Pow(6.2, pieces.Count);
            m_step = m_max / 20;
        }

        public Pieces GetPieces()
        {
            return m_pieces;
        }

        public Solutions Solve()
        {
            bool[] v = new bool[m_rect.Width * m_rect.Height];

            Solve(new SolutionPieces(), m_pieces, v);

            return m_solutions;
        }

        public void Solve(SolutionPieces piecesAdded, Pieces piecesLeft, bool[] v)
        {
            if(piecesLeft.Count == 0)
            {
                m_solutions.Add(piecesAdded);
            }

            foreach (Piece piece in piecesLeft)
            {
                foreach (Piece.Rotation rotation in Enum.GetValues(typeof(Piece.Rotation)))
                {
                    for (int flip = 0; flip < 2; flip++)
                    {
                        if (!piece.RedundantTransformation(rotation, flip == 1))
                        {
                            Points points = piece.GetPoints(rotation, flip == 1);
                            UpdatePoints(points, piece, piecesAdded, piecesLeft, v.Clone() as bool[], rotation, flip == 1);
                        }
                    }
                }
            }
        }
        private void UpdatePoints(Points points, Piece piece, SolutionPieces piecesAdded, Pieces piecesLeft, bool[] v, Piece.Rotation rotation, bool flip)
        {
            int x = 0;
            int y = 0;
            GetNextPos(v, ref x, ref y);
            if (MovePoints(ref points, x, y))
            {
                if (AddPoints(ref v, points))
                {
                    Pieces newPiecesLeft = new Pieces();
                    SolutionPieces newPiecesAdded = new SolutionPieces();

                    newPiecesLeft.AddRange(piecesLeft);
                    newPiecesLeft.Remove(piece);

                    SolutionPiece solutionPiece = new SolutionPiece(piece, rotation, flip, new Point(x, y));
                    newPiecesAdded.AddRange(piecesAdded);
                    newPiecesAdded.Add(solutionPiece);


                    Solve(newPiecesAdded, newPiecesLeft, v.Clone() as bool[]);
                }
            }
            m_count++;

            if((m_count % m_step) == 0)
            {
                m_worker.ReportProgress(100 * m_count / m_max);
            }
        }
        private bool AddPoints(ref bool[] v, Points points)
        {
            foreach(Point pt in points)
            {
                int index = pt.Y * m_rect.Width + pt.X;

                if(index >= v.Length || v[index])
                {
                    return false;
                }

                v[index] = true;
            }

            return true;
        }
        private bool GetNextPos(bool[] v, ref int x, ref int y)
        {
            int index = 0;
            while(index < v.Length && v[index])
            {
                index++;
            }

            if(index >= v.Length)
            {
                return false;
            }

            x = index % m_rect.Width;
            y = index / m_rect.Width;

            return true;
        }

        private bool MovePoints(ref Points points, int x, int y)
        {
            return MovePoints(ref points, x, y, m_rect);
        }

        static public bool MovePoints(ref Points points, int x, int y, Rectangle rect)
        {
            Points tp = new Points();

            int o = rect.Width; ;
            foreach(Point pt in points)
            {
                if(pt.Y == 0)
                {
                    o = Math.Min(pt.X, o);
                }
            }

            x -= o;
            for (int i = 0; i < points.Count; i++)
            {
                Point pt = points[i];

                pt.X += x;
                pt.Y += y;

                if (!rect.Contains(pt))
                {
                    return false;
                }

                tp.Add(pt);
            }

            points = tp;

            return true;
        }

    }
    public class Pieces : List<Piece>
    {}
    public class SolutionPieces : List<SolutionPiece>
    {}

    public class SolutionPiece
    {
        public SolutionPiece(Piece piece, Piece.Rotation rotation, bool flip, Point offset)
        {
            m_piece = piece;
            m_rotation = rotation;
            m_flip = flip;
            m_offset = offset;
        }

        public Piece            m_piece = null;
        public Piece.Rotation   m_rotation = Piece.Rotation.UP;
        public bool             m_flip = false;
        public Point            m_offset;

    }

}
