using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        const int SQUARE_SIZE = 16;

        const string SNAKE_HEAD_COLOR = "#7C73C0";
        const string SNAKE_BODY_COLOR = "#94ADD7";

        const string FRUIT_COLOR = "#E8FFCE";
        const string OBSTACLE_COLOR = "#EF6262";

        const int FRUIT_SCORE = 100;

        const int MIN_OBSTACLE_HEIGHT = 2;

        private int FIELD_WIDTH;
        private int FIELD_HEIGHT;

        private bool directionSwitched = false;

        private int level = 1;
        private int score = 0;

        private DispatcherTimer gameTimer = new DispatcherTimer();
        private Random random = new Random();
        private List<Point> snakeArray = new List<Point>();
        private List<Point> obstacleArray = new List<Point> ();

        private Point fruitPos;
        private bool isFruitExist = false;

        private enum Direction
        {
            UP,
            RIGHT,
            DOWN,
            LEFT
        }

        private Direction currentDirection;

        private void GetScoreTable()
        {
            if (!File.Exists("Scores.txt")) return;

            string[] lines = File.ReadAllLines("Scores.txt");
            ScoreList.Items.Clear();
            foreach (string line in lines)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = line;
                ScoreList.Items.Add(item);
            }

        }

        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FIELD_WIDTH  = (int)GameCanvas.Width / SQUARE_SIZE;
            FIELD_HEIGHT = (int)GameCanvas.Height / SQUARE_SIZE;

            GetScoreTable();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void GameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(!gameTimer.IsEnabled || directionSwitched) return;
                
            if ((e.Key == Key.Up || e.Key == Key.W) && currentDirection != Direction.DOWN)
                currentDirection = Direction.UP;
            else if ((e.Key == Key.Down || e.Key == Key.S) && currentDirection != Direction.UP)
                currentDirection = Direction.DOWN;
            else if ((e.Key == Key.Right || e.Key == Key.D) && currentDirection != Direction.LEFT)
                currentDirection = Direction.RIGHT;
            else if ((e.Key == Key.Left || e.Key == Key.A) && currentDirection != Direction.RIGHT)
                currentDirection = Direction.LEFT;
            directionSwitched = true;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            Point headPoint = snakeArray[0];
            Point tailPoint = snakeArray[snakeArray.Count - 1];
            
            // If Snake is full map
            if(snakeArray.Count == FIELD_HEIGHT*FIELD_WIDTH)
            {
                FinishGame();
                return;
            }

            // If snake crushed into itself
            for(int i = 1; i < snakeArray.Count; i++)
            {
                if(headPoint.X == snakeArray[i].X && headPoint.Y == snakeArray[i].Y)
                {
                    FinishGame();
                    return;
                }
            }

            // If snake crushed into obstacle
            for (int i = 0; i < obstacleArray.Count; i++)
            {
                if (headPoint.X == obstacleArray[i].X && headPoint.Y == obstacleArray[i].Y)
                {
                    FinishGame();
                    return;
                }
            }

            if (currentDirection == Direction.UP)
                headPoint.Y--;
            else if(currentDirection == Direction.DOWN)
                headPoint.Y++;
            else if(currentDirection == Direction.LEFT)
                headPoint.X--;
            else if(currentDirection == Direction.RIGHT) 
                headPoint.X++;
            
            directionSwitched = false;

            // Head check 
            if (headPoint.X < 0)
                headPoint.X += FIELD_WIDTH;
            else if (headPoint.X > FIELD_WIDTH - 1)
                headPoint.X -= FIELD_WIDTH;

            if (headPoint.Y < 0)
                headPoint.Y += FIELD_HEIGHT;
            else if (headPoint.Y > FIELD_HEIGHT - 1)
                headPoint.Y -= FIELD_HEIGHT;

            if (!isFruitExist)
            {
                // Creating fruit
                do
                {
                    fruitPos = new Point(random.Next(0, FIELD_WIDTH), random.Next(0, FIELD_HEIGHT));
                } while (snakeArray.Any(   cord => (cord.X == fruitPos.X && cord.Y == fruitPos.Y)) ||
                         obstacleArray.Any(cord => (cord.X == fruitPos.X && cord.Y == fruitPos.Y)));
                isFruitExist = true;
            }
            else if (headPoint.X == fruitPos.X && headPoint.Y == fruitPos.Y)
            {
                // Eat fruit
                score += level * FRUIT_SCORE;
                ScoreLabel.Content = "Счёт: " + score;
                snakeArray.Add(tailPoint);
                isFruitExist = false;
            }

            snakeArray.Insert(0, headPoint);
            snakeArray.RemoveAt(snakeArray.Count - 1);

            RenderField();
        }

        private void GenerateObstacles()
        {
            obstacleArray.Clear();

            for (int i = 0; i < level - 1; i++)
            {

                int obstacleX;
                int obstacleY;

                do
                {
                    obstacleX = random.Next(0, FIELD_WIDTH);
                    obstacleY = random.Next(1, 3) == 1 ?
                                random.Next(MIN_OBSTACLE_HEIGHT, FIELD_HEIGHT / 2 - 2) :
                                random.Next(FIELD_HEIGHT / 2 + 2, FIELD_HEIGHT); // Generate from two ranges

                } while (obstacleArray.Any(cord => Math.Abs(cord.X - obstacleX) < 2) || obstacleX == FIELD_WIDTH / 2);

                // Create a vertical line of obstacle
                if (obstacleY < FIELD_HEIGHT / 2)
                {
                    for(int j = 0; j < obstacleY; j++)
                    {
                        obstacleArray.Add(new Point(obstacleX, j));
                    }
                } else
                {
                    for (int j = obstacleY; j < FIELD_HEIGHT; j++)
                    {
                        obstacleArray.Add(new Point(obstacleX, j));
                    }
                }

            }

        }

        private void RenderSquare(Point point, string color)
        {
            Rectangle rect = new Rectangle();

            rect.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(color));
            rect.Width = SQUARE_SIZE;
            rect.Height = SQUARE_SIZE;

            Canvas.SetLeft(rect, point.X * SQUARE_SIZE);
            Canvas.SetTop(rect, point.Y * SQUARE_SIZE);

            GameCanvas.Children.Add(rect);  
        }

        private void RenderField()
        {
            GameCanvas.Children.Clear();

            // Draw fruit
            RenderSquare(fruitPos, FRUIT_COLOR);

            // Draw snake
            for (int i = snakeArray.Count - 1; i >= 0; i--)
            {
                RenderSquare(snakeArray[i], i == 0 ? SNAKE_HEAD_COLOR : SNAKE_BODY_COLOR);
            }

            // Draw obstacles
            for (int i = 0; i < obstacleArray.Count; i++)
            {
                RenderSquare(obstacleArray[i], OBSTACLE_COLOR);
            }

        }

        private void StartGame()
        {
            score = 0;
            ScoreLabel.Content = "Счёт: 0";
            currentDirection = Direction.UP;
            isFruitExist = false;

            level = LevelCombo.SelectedIndex + 1;

            snakeArray.Clear();
            snakeArray.Add(new Point(FIELD_WIDTH / 2, FIELD_HEIGHT / 2));
            snakeArray.Add(new Point(FIELD_WIDTH / 2, FIELD_HEIGHT / 2 + 1));
            snakeArray.Add(new Point(FIELD_WIDTH / 2, FIELD_HEIGHT / 2 + 2));


            LevelCombo.IsEnabled = false;
            ScoreList.IsEnabled = false;
            StartButton.IsEnabled = false;

            GenerateObstacles();

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 100 / level);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void FinishGame()
        {
            LevelCombo.IsEnabled = true;
            ScoreList.IsEnabled = true;
            StartButton.IsEnabled = true;

            GameOverWindow gameOverWindow = new GameOverWindow(score);
            gameOverWindow.ShowDialog();
            GetScoreTable();
            gameTimer.Stop();
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }
    }
}