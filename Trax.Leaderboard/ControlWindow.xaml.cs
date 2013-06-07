using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Trax.Leaderboard
{
    public partial class ControlWindow : Window
    {
        private ILeaderboardData _leaderboardData;
        private W1024x768 window;

        public ControlWindow()
        {
            InitializeComponent();
            _leaderboardData = LeaderboardContainer.Container.GetInstance<ILeaderboardData>();

            TeamList.ItemsSource = _leaderboardData.TeamData;            
            _leaderboardData.JudgeOffset = 407;
            _leaderboardData.BackgroundColor = new SolidColorBrush(BackgroundColorPicker.SelectedColor);
            _leaderboardData.SetWindowSize(1024, 768);
            _leaderboardData.Title.FontSize = 40;
            _leaderboardData.Title.Color = new SolidColorBrush(Colors.Black);
            Title.FontSize = 40;
            Title.TextColor = new SolidColorBrush(Colors.Black);
            _leaderboardData.JudgeBoxWidth = 150;
            _leaderboardData.JudgeBoxHeight = 40;

            _leaderboardData.ScoreBoxJudgeWidth = 150;
            _leaderboardData.ScoreBoxPositionWidth = 70;
            _leaderboardData.ScoreBoxTeamNameWidth = 300;
            _leaderboardData.ScoreBoxScoreWidth = 70;
            _leaderboardData.ScoreBoxHeight = 40;
            
            _leaderboardData.Title.FontName = "Arial";
            _leaderboardData.BoxJudgeFont.FontName = "Arial";
            _leaderboardData.BoxScoreFont.FontName = "Arial";

            BoxJudgeColorPicker.SelectedColor = Colors.LightCyan;
            BoxScoreColorPicker.SelectedColor = Colors.LightCyan;            
            _leaderboardData.BoxJudgeColor = new SolidColorBrush(Colors.LightCyan);
            _leaderboardData.BoxScoreColor = new SolidColorBrush(Colors.LightCyan);


            for (int i = 100; i >= 0; i--)
            {
                BoxJudgeOpacity.Items.Add(String.Format("{0}%", i));
                BoxScoreOpacity.Items.Add(String.Format("{0}%", i));                
            }
            BoxJudgeOpacity.SelectedIndex = 0;
            BoxScoreOpacity.SelectedIndex = 0;

            JudgeFont.FontSize = 27;
            JudgeFont.TextColor = new SolidColorBrush(Colors.Black);
            _leaderboardData.BoxJudgeFont.FontSize = 27;
            _leaderboardData.BoxJudgeFont.Color = new SolidColorBrush(Colors.Black);

            ScoreFont.FontSize = 27;
            ScoreFont.TextColor = new SolidColorBrush(Colors.Black);
            _leaderboardData.BoxScoreFont.FontSize = 27;
            _leaderboardData.BoxScoreFont.Color = new SolidColorBrush(Colors.Black);


            Title.OnSetButtonClicked += Title_OnSetButtonClicked;
            JudgeFont.OnSetButtonClicked += JudgeFont_OnSetButtonClicked;
            ScoreFont.OnSetButtonClicked += ScoreFont_OnSetButtonClicked;
        }

        void ScoreFont_OnSetButtonClicked(object sender, Control.TextElement.TextElementEventArgs e)
        {
            _leaderboardData.BoxScoreFont.Bold = e.Bold;
            _leaderboardData.BoxScoreFont.Color = e.Color;
            _leaderboardData.BoxScoreFont.FontName = e.FontName;
            _leaderboardData.BoxScoreFont.FontSize = e.FontSize;
            _leaderboardData.BoxScoreFont.Italic = e.Italic;
        }

        void JudgeFont_OnSetButtonClicked(object sender, Control.TextElement.TextElementEventArgs e)
        {
            _leaderboardData.BoxJudgeFont.Bold = e.Bold;
            _leaderboardData.BoxJudgeFont.Color = e.Color;
            _leaderboardData.BoxJudgeFont.FontName = e.FontName;
            _leaderboardData.BoxJudgeFont.FontSize = e.FontSize;
            _leaderboardData.BoxJudgeFont.Italic = e.Italic;
        }

        void Title_OnSetButtonClicked(object sender, Control.TextElement.TextElementEventArgs e)
        {
            _leaderboardData.Title.Bold = e.Bold;
            _leaderboardData.Title.Color = e.Color;
            _leaderboardData.Title.FontName = e.FontName;
            _leaderboardData.Title.FontSize = e.FontSize;
            _leaderboardData.Title.Italic = e.Italic;
            _leaderboardData.Title.Text = e.Text;
        }

        private void AddNewTeam(object sender, RoutedEventArgs e)
        {
            //Random r = new Random();
            //_leaderboardData.TeamData.Add(new TeamData { Name = NewTeamName.Text, PointsJudge1 = r.Next(0, 11), PointsJudge2 = r.Next(0, 11), PointsJudge3 = r.Next(0, 11) });

            _leaderboardData.TeamData.Add(new TeamData { Name = NewTeamName.Text, PointsJudge1 = 0, PointsJudge2 = 0, PointsJudge3 = 0 });
            _leaderboardData.UpdateScorePosition();
        }

        private void ShowUi(object sender, RoutedEventArgs e)
        {
            if (window != null)
                window.Close();

            window = new W1024x768();
            window.Show();
        }

        private void UpdateJudgeUiClick(object sender, RoutedEventArgs e)
        {
            _leaderboardData.Judge1 = Judge1.Text;
            _leaderboardData.Judge2 = Judge2.Text;
            _leaderboardData.Judge3 = Judge3.Text;
        }

        private void SaveTeamStats(object sender, RoutedEventArgs e)
        {
            var selectedTeam = TeamList.SelectedItem as TeamData;
            if (selectedTeam != null)
            {
                var item = _leaderboardData.TeamData.First(x => x.Id.Equals(selectedTeam.Id));
                item.Name = UpdateTeamName.Text;

                int points1;
                int points2;
                int points3;

                if (!Int32.TryParse(Judge1Points.Text, out points1))
                    points1 = 0;
                if (!Int32.TryParse(Judge2Points.Text, out points2))
                    points2 = 0;
                if (!Int32.TryParse(Judge3Points.Text, out points3))
                    points3 = 0;

                if (points1 < 0 || points1 > 999)
                    points1 = 0;
                if (points2 < 0 || points2 > 999)
                    points2 = 0;
                if (points3 < 0 || points3 > 999)
                    points3 = 0;

                item.PointsJudge1 = points1;
                item.PointsJudge2 = points2;
                item.PointsJudge3 = points3;

                _leaderboardData.UpdateScorePosition();
            }
        }

        private void ClearTeams(object sender, RoutedEventArgs e)
        {
            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

            MessageBoxResult rsltMessageBox = MessageBox.Show("Do you want to reset the teams?", "Reset teams", btnMessageBox, icnMessageBox);

            if (rsltMessageBox == MessageBoxResult.Yes)
            {
                _leaderboardData.TeamData.Clear();
                _leaderboardData.UpdateScorePosition();
            }
        }

        private void ClearBackgroundImage(object sender, RoutedEventArgs e)
        {
            _leaderboardData.BackgroundImage = null;
        }

        private void SetBackgroundImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                _leaderboardData.BackgroundImage = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void SetMarginOffset(object sender, RoutedEventArgs e)
        {
            int leftMargin;
            if (!int.TryParse(LeftMarginOffset.Text, out leftMargin))
                leftMargin = 0;
            _leaderboardData.JudgeOffset = leftMargin;
        }

        private void SetBackgroundColor(object sender, RoutedEventArgs e)
        {
            _leaderboardData.BackgroundColor = new SolidColorBrush(BackgroundColorPicker.SelectedColor);
        }

        private void SetWindowSize(object sender, RoutedEventArgs e)
        {
            int width, height;
            if (!Int32.TryParse(WindowWidth.Text, out width))
                width = 1024;
            if (!Int32.TryParse(WindowHeight.Text, out height))
                height = 768;

            if (width < 500 || width > 6000)
            {
                MessageBox.Show("Invalid width");
                return;
            }
            if (height < 500 || height > 2000)
            {
                MessageBox.Show("Invalid height");
                return;
            }

            _leaderboardData.SetWindowSize(width, height);            
        }

        private void SetJudgeBoxColor(object sender, RoutedEventArgs e)
        {
            var color = new SolidColorBrush(BoxJudgeColorPicker.SelectedColor);
            color.Opacity = ConvertPercentToOpacity(BoxJudgeOpacity.SelectedValue.ToString());
            _leaderboardData.BoxJudgeColor = color;
        }

        private void SetScoreBoxColor(object sender, RoutedEventArgs e)
        {
            var color = new SolidColorBrush(BoxScoreColorPicker.SelectedColor);
            color.Opacity = ConvertPercentToOpacity(BoxScoreOpacity.SelectedValue.ToString());
            _leaderboardData.BoxScoreColor = color;
        }

        /// <summary>
        /// Converts a string % value to double to be used for opacity
        /// </summary>
        /// <param name="valueToConvert">Expected a value like: 100% or 5%</param>
        /// <returns>1 or 0.05</returns>
        private double ConvertPercentToOpacity(string valueToConvert)
        {            
            valueToConvert = valueToConvert.Replace("%", "");
            int intValue;
            if (!Int32.TryParse(valueToConvert, out intValue))
                return 1;

            if (intValue == 100)
                return 1;
            else if (intValue == 0)
                return 0;
            
            string stringValue = "0.";
            if (intValue < 10)
                stringValue += "0";
            stringValue += intValue.ToString();

            decimal output = Decimal.Parse(stringValue, CultureInfo.InvariantCulture);
            return (double)output;
        }

        private void SetJudgeBoxSize(object sender, RoutedEventArgs e)
        {
            int width, height;
            if (!Int32.TryParse(JudgeBoxWidth.Text, out width))
                width = 150;
            if (!Int32.TryParse(JudgeBoxHeight.Text, out height))
                height = 40;

            if (width < 10 || width > 1000)
            {
                MessageBox.Show("Invalid width");
                return;
            }
            if (height < 10 || height > 1000)
            {
                MessageBox.Show("Invalid height");
                return;
            }

            _leaderboardData.JudgeBoxWidth = width;
            _leaderboardData.JudgeBoxHeight = height;
        }

        private void SetScoreBoxSize(object sender, RoutedEventArgs e)
        {
            int positionWidth, teamNameWidth, scoreWidth, judgeWidth, height;
            if (!Int32.TryParse(ScoreBoxPositionWidth.Text, out positionWidth))
                positionWidth = 70;
            if (!Int32.TryParse(ScoreBoxTeamNameWidth.Text, out teamNameWidth))
                teamNameWidth = 380;
            if (!Int32.TryParse(ScoreBoxScoreWidth.Text, out scoreWidth))
                scoreWidth = 100;
            if (!Int32.TryParse(ScoreBoxJudgeWidth.Text, out judgeWidth))
                judgeWidth = 70;
            if (!Int32.TryParse(ScoreBoxHeight.Text, out height))
                height = 40;

            if (positionWidth < 10 || positionWidth > 1000)
            {
                MessageBox.Show("Invalid position width");
                return;
            }
            if (teamNameWidth < 10 || teamNameWidth > 1000)
            {
                MessageBox.Show("Invalid team name width");
                return;
            }
            if (scoreWidth < 10 || scoreWidth > 1000)
            {
                MessageBox.Show("Invalid score width");
                return;
            }
            if (judgeWidth < 10 || judgeWidth > 1000)
            {
                MessageBox.Show("Invalid judge width");
                return;
            }
            if (height < 10 || height > 1000)
            {
                MessageBox.Show("Invalid height");
                return;
            }

            _leaderboardData.ScoreBoxJudgeWidth = judgeWidth;
            _leaderboardData.ScoreBoxPositionWidth = positionWidth;
            _leaderboardData.ScoreBoxScoreWidth = scoreWidth;
            _leaderboardData.ScoreBoxTeamNameWidth = teamNameWidth;
            _leaderboardData.ScoreBoxHeight = height;
        }
    }
}
