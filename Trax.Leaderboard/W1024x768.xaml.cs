using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Trax.Leaderboard
{
    public partial class W1024x768 : Window
    {
        public ILeaderboardData _leaderboardData;
        private bool fullScreen;

        public W1024x768()
        {
            _leaderboardData = LeaderboardContainer.Container.GetInstance<ILeaderboardData>();

            InitializeComponent();

            DataContext = _leaderboardData;
            var teamList = CollectionViewSource.GetDefaultView(_leaderboardData.TeamData);
            teamList.SortDescriptions.Add(new SortDescription("Position", ListSortDirection.Ascending));
            TeamList.ItemsSource = teamList;

            _leaderboardData.PropertyChanged += _leaderboardData_PropertyChanged;   
            _leaderboardData.Title.PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
                {
                    if (args.PropertyName.Equals("Text"))
                    {
                        if (string.IsNullOrEmpty(_leaderboardData.Title.Text))
                            WindowTitle.Visibility = Visibility.Collapsed;
                        else
                            WindowTitle.Visibility = Visibility.Visible;
                        this.InvalidateVisual();
                    }
                };
        }

        void _leaderboardData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("TeamData"))
            {
                var teamList = CollectionViewSource.GetDefaultView(_leaderboardData.TeamData);
                teamList.SortDescriptions.Add(new SortDescription("Position", ListSortDirection.Ascending));
                TeamList.ItemsSource = null;
                TeamList.ItemsSource = teamList;
            }
            if (e.PropertyName.Equals("BackgroundImage"))
            {
                if (_leaderboardData.BackgroundImage != null)
                    this.Background = new ImageBrush(_leaderboardData.BackgroundImage);
                else
                    this.Background = _leaderboardData.BackgroundColor;
            }
            if (e.PropertyName.Equals("BackgroundColor"))
            {
                this.Background = _leaderboardData.BackgroundColor;
            }
            if (e.PropertyName.Equals("WindowResized"))
            {
                this.Width = _leaderboardData.WindowWidth;
                this.Height = _leaderboardData.WindowHeight;
                InvalidateVisual();
            }
            
        }

        private void DWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void MouseDouble_Click(object sender, MouseButtonEventArgs e)
        {
            if (!fullScreen)
            {                
                this.WindowState = WindowState.Maximized;
            }
            else
            {                
                this.WindowState = WindowState.Normal;
            }
            fullScreen = !fullScreen;
        }
    }
}
