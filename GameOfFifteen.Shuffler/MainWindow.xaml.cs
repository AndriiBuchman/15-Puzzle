using Puzzle15;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace GameOfFifteen.Shuffler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int k = 0;
        Timer timer;
        public LinkedList<State> moves;
        public MainWindow()
        {
            InitializeComponent();
            Solve.IsEnabled = false;
            Replay.IsEnabled = false;
        }

        private void Shuffle_btn(object sender, RoutedEventArgs e)
        {
            RandomShuffler r = new RandomShuffler();
            r.Shuffler(8);
            Solve.IsEnabled = true;
        }

        private void Solve_btn(object sender, RoutedEventArgs e)
        {
            int[,] matrix = WriteReadFile.ReadMatrix();
            Puzzle15State state = new Puzzle15State(matrix);
            BestFirstSolver solver = new BestFirstSolver();
            moves = solver.Solve(state);
            WriteReadFile.WriteMovesToFile(moves);
            Replay.IsEnabled = true;
        }

        private void Replay_btn(object sender, RoutedEventArgs e)
        {
            List<int[,]> matrices = new List<int[,]>();
            foreach (State s in moves)
            {
                matrices.Add(((Puzzle15State)s).Square);
            }
            k = 0;
            int time = Convert.ToInt32(GameTime.Text);
            TimerCallback tm = new TimerCallback(ShowMatrix);
            timer = new Timer(tm, matrices, 0, time*1000);
        }

        public void ShowMatrix(object obj)
        {
           List<int[,]> states = (List<int[,]>)obj;
            this.Dispatcher.Invoke(() =>
            {
                string line1 = "";
                if (k == states.Count) timer.Dispose();
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            line1 += states[k][i, j] + "\t";
                        }
                        line1 += "\n";

                    }
                    k++;
                    Box.Document.Blocks.Clear();
                    Box.Document.Blocks.Add(new Paragraph(new Run(line1)));
                }
            });
        }

        private void GameTime_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
