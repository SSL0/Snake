using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Linq;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для GameOverWindow.xaml
    /// </summary>
    public partial class GameOverWindow : Window
    {
        private int playerScore;
        public GameOverWindow(int playerScore)
        {
            this.playerScore = playerScore;
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string playerName = PlayerNameTextBox.Text;
            if(playerName.Contains(" "))
            {
                MessageBox.Show("Запишите имя без пробелов");
                return;
            }

            // Open file if it does not exist
            if (!File.Exists("Scores.txt"))
            {
                FileStream file = File.Create("Scores.txt");
                file.Dispose();  
            }

            bool isPlayerExist = false;
            string[] lines = File.ReadAllLines("Scores.txt");
            List<Tuple<string, int>> scoreList = new List<Tuple<string, int>>();
            foreach(string line in lines)
            {
                string[] split = line.Split(' ');
                if(playerName == split[0])
                {
                    if (playerScore > Convert.ToInt32(split[1]))
                    {
                        split[1] = playerScore.ToString();
                    }
                    isPlayerExist = true;
                    
                }
                scoreList.Add(new Tuple<string, int>(split[0], Convert.ToInt32(split[1])));
            }
            if(!isPlayerExist)
                scoreList.Add(new Tuple<string, int>(playerName, playerScore));

            scoreList = scoreList.OrderByDescending(scoreTuple => scoreTuple.Item2).ToList();

            int lineCount = scoreList.Count < 15 ? scoreList.Count : 15;

            using (StreamWriter fs = new StreamWriter("Scores.txt")) {
                for(int i = 0; i < lineCount; i++)
                {
                    fs.Write(scoreList[i].Item1 + " " + scoreList[i].Item2 + " очков\n");
                }
            }
          
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ScoreLabel.Content = "Счет: " + playerScore.ToString();
        }
    }
}
