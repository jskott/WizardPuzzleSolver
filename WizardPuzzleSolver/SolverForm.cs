using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WizardPuzzleSolver
{
    public partial class SolverForm : Form
    {
        Pieces m_pieces = new Pieces();
        List<PieceControl> m_controls = new List<PieceControl>();

        Color[] m_colors = { Color.RoyalBlue, Color.Plum, Color.MediumSpringGreen, Color.DarkOrange, Color.BurlyWood, Color.DarkRed, Color.DeepSkyBlue, Color.LightSeaGreen, Color.IndianRed, Color.PaleGoldenrod, Color.CadetBlue, Color.DarkGreen };

        string[] m_piecesDesc = {"0,0;0,1;0,2;0,3;0,4",
                                 "0,0;0,1;0,2;0,3;1,3",
                                 "0,0;0,1;0,2;0,3;1,2",
                                 "0,0;0,1;0,2;1,2;1,3",
                                 "0,0;0,1;0,2;1,2;2,2",
                                 "0,0;0,1;0,2;1,0;1,1",
                                 "0,0;0,1;1,1;2,0;2,1",
                                 "0,0;1,0;1,1;1,2;2,2",
                                 "0,0;1,0;1,1;1,2;2,1",
                                 "0,0;1,0;1,1;1,2;2,0",
                                 "0,0;0,1;1,1;1,2;2,2",
                                 "0,1;1,0;1,1;1,2;2,1"};

        public SolverForm()
        {
            InitializeComponent();
            LoadPieces();
            SetupPieceControls(false);
        }

        public void LoadPieces()
        {
            try
            {
                int name = 1;
                foreach (string pieceDesc in m_piecesDesc)
                {
                    Piece piece = new Piece(new Rectangle(0, 0, 5, 5), name.ToString());
                    name++;
                    if (piece.Load(pieceDesc))
                    {
                        m_pieces.Add(piece);
                    }
                    else
                    {
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetupPieceControls(bool all)
        {
            int x = 10;
            int y = 10;
            int count = 0;

            if (all)
            {
                for (int i = 0; i < 2; i++)
                {
                    foreach (Piece.Rotation rotation in Enum.GetValues(typeof(Piece.Rotation)))
                    {
                        count = 0;
                        int tx = 10;
                        int ty = 0;

                        foreach (Piece piece in m_pieces)
                        {
                            PieceControl pc = new PieceControl(piece, 14, m_colors[count++], rotation, i == 1);

                            pc.Location = new Point(tx, y);
                            Controls.Add(pc);

                            tx += pc.Width + 10;
                            ty = Math.Max(ty, pc.Height);
                        }

                        x = Math.Max(tx, x);
                        y += ty + 10;
                    }
                }
            }
            else 
            {
                foreach (Piece piece in m_pieces)
                {
                    PieceControl pc = new PieceControl(piece, 14, m_colors[count++], Piece.Rotation.UP, false);

                    piecesFlowLayoutPanel.Controls.Add(pc);
                    m_controls.Add(pc);
                }
            }

        }

        private void m_solveButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Pieces pieces = new Pieces();
            ColorByPiece colors = new ColorByPiece();

            progressBar.Value = 0;
            foreach(PieceControl pc in m_controls)
            {
                if (pc.IsChecked())
                {
                    pieces.Add(pc.GetPiece());
                    colors.Add(pc.GetPiece(), pc.GetColor());
                }
            }

            WorkerArgument arg = new WorkerArgument();

            arg.m_board = new Board(pieces, solverBackgroundWorker);
            arg.m_w = pieces.Count;
            arg.m_h = 5;
            arg.m_colors = colors;

            solverBackgroundWorker.RunWorkerAsync(arg);

            flowLayoutPanel.Controls.Clear();

            Cursor.Current = Cursors.Arrow;
        }

        private void Check(bool check)
        {
            foreach (PieceControl pc in m_controls)
            {
                pc.SetCheck(check);
            }
        }

        private void m_selectAllButton_Click(object sender, EventArgs e)
        {
            Check(true);
        }

        private void m_clearAllButton_Click(object sender, EventArgs e)
        {
            Check(false);
        }

        private void DoSolveWork(object sender, DoWorkEventArgs e)
        {
            WorkerArgument arg = e.Argument as WorkerArgument;

            arg.m_solutions = arg.m_board.Solve();

            e.Result = arg;

        }

        private void solverBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerArgument arg = e.Result as WorkerArgument;
           
            foreach (SolutionPieces solution in arg.m_solutions)
            {
                BoardControl bc = new BoardControl(solution, arg.m_w, arg.m_h, 30, arg.m_colors);
                flowLayoutPanel.Controls.Add(bc);
            }

            progressBar.Value = 100;

        }

        public class WorkerArgument
        {
            public Board           m_board = null;
            public int             m_w = 0;
            public int             m_h = 0;
            public ColorByPiece    m_colors = null;
            public Solutions       m_solutions = null;
        }

        private void solverBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.PerformStep();
        }
    }
    public class ColorByPiece : Dictionary<Piece, Color>
    { }
    public class Solutions : List<SolutionPieces>
    { }
}
