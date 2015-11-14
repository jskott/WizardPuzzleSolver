using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WizardPuzzleSolver
{
    public partial class BoardControl : UserControl
    {
        SolutionPieces  m_solution = null;
        Rectangle       m_rect;
        int             m_blockSize = 10;
        Dictionary<Piece, Color> m_colors = null;

        public BoardControl(SolutionPieces solution, int w, int h, int blockSize, Dictionary<Piece, Color> colors)
        {
            InitializeComponent();
            m_solution = solution;
            m_rect = new Rectangle(0, 0, w, h);
            m_blockSize = blockSize;
            Width = w * blockSize;
            Height = h * blockSize;
            m_colors = colors;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            foreach(SolutionPiece solutionPiece in m_solution)
            {
                SolidBrush brush = new SolidBrush(m_colors[solutionPiece.m_piece]);
                Points points = solutionPiece.m_piece.GetPoints(solutionPiece.m_rotation, solutionPiece.m_flip);
                Board.MovePoints(ref points, solutionPiece.m_offset.X, solutionPiece.m_offset.Y, m_rect);
                foreach (Point pt in points)
                {
                    int x = pt.X * m_blockSize;
                    int y = pt.Y * m_blockSize;

                    e.Graphics.FillRectangle(brush, x, y, m_blockSize, m_blockSize);
                }
            }
        }
    }
}
