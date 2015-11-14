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
    public partial class PieceControl : UserControl
    {
        Piece               m_piece = null;
        int                 m_blockSize = 10;
        SolidBrush          m_brush = null;
        Piece.Rotation      m_rotation = Piece.Rotation.UP;
        bool                m_flip = false;
        CheckBox            m_checkBox = new CheckBox();

        public PieceControl(Piece piece, int blockSize, Color color, Piece.Rotation rotation, bool flip)
        {
            InitializeComponent();
            m_piece = piece;
            m_blockSize = blockSize;
            m_brush = new SolidBrush(color);
            m_rotation = rotation;
            m_flip = flip;

            UpdateSize();

            m_checkBox.Location = new Point(10, Height - 25);
            m_checkBox.Text = piece.GetName();
            Controls.Add(m_checkBox);
        }
        public bool IsChecked()
        {
            return m_checkBox.Checked;
        }
        public void SetCheck(bool check)
        {
            m_checkBox.Checked = check;
        }
        public Color GetColor()
        {
            return m_brush.Color;
        }
        public Piece GetPiece()
        {
            return m_piece;
        }
        private void UpdateSize()
        {
            Width = m_blockSize * 5;
            Height = m_blockSize * 6 + 25;
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Points points = m_piece.GetPoints(m_rotation, m_flip);
  
            foreach(Point pt in points)
            {
                int x = pt.X * m_blockSize + m_blockSize / 2;
                int y = pt.Y * m_blockSize + m_blockSize / 2;

                e.Graphics.FillRectangle(m_brush, x, y, m_blockSize, m_blockSize);
            }
        }
    }
}
