using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Trax.Leaderboard.Annotations;

namespace Trax.Leaderboard
{
    class LeaderboardData : ILeaderboardData
    {
        private TextElement _title;
        private ObservableCollection<TeamData> _teamData;
        private string _judge1;
        private string _judge2;
        private string _judge3;
        private BitmapImage _backgroundImage;
        private int _judgeOffset;
        private Brush _backgroundColor;
        private int _windowWidth;
        private int _windowHeight;
        private int _windowResized;
        private Brush _boxJudgeColor;
        private Brush _boxScoreColor;
        private Element _boxJudgeFont;
        private Element _boxScoreFont;
        private int _judgeBoxWidth;
        private int _judgeBoxHeight;
        private int _scoreBoxPositionWidth;
        private int _scoreBoxTeamNameWidth;
        private int _scoreBoxScoreWidth;
        private int _scoreBoxJudgeWidth;
        private int _scoreBoxHeight;        

        public string Judge3
        {
            get { return _judge3; }
            set { _judge3 = value; OnPropertyChanged(); }
        }

        public int WindowHeight
        {
            get { return _windowHeight; }            
        }

        public int WindowResized
        {
            get { return _windowResized; }
        }

        public BitmapImage BackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; OnPropertyChanged(); }
        }

        public int JudgeOffset
        {
            get { return _judgeOffset; }
            set { _judgeOffset = value; OnPropertyChanged(); }
        }

        public Brush BoxJudgeColor
        {
            get { return _boxJudgeColor; }
            set { _boxJudgeColor = value; OnPropertyChanged(); }
        }

        public int JudgeBoxHeight
        {
            get { return _judgeBoxHeight; }
            set { _judgeBoxHeight = value; OnPropertyChanged(); }
        }

        public Brush BoxScoreColor
        {
            get { return _boxScoreColor; }
            set { _boxScoreColor = value; OnPropertyChanged(); }
        }

        public Element BoxJudgeFont
        {
            get { return _boxJudgeFont; }
            set { _boxJudgeFont = value; OnPropertyChanged(); }
        }

        public int JudgeBoxWidth
        {
            get { return _judgeBoxWidth; }
            set { _judgeBoxWidth = value; OnPropertyChanged(); }
        }

        public Element BoxScoreFont
        {
            get { return _boxScoreFont; }
            set { _boxScoreFont = value; OnPropertyChanged(); }
        }

        public int ScoreBoxPositionWidth
        {
            get { return _scoreBoxPositionWidth; }
            set { _scoreBoxPositionWidth = value; OnPropertyChanged(); OnPropertyChanged("ListViewGridSize"); }
        }

        public int ScoreBoxTeamNameWidth
        {
            get { return _scoreBoxTeamNameWidth; }
            set { _scoreBoxTeamNameWidth = value; OnPropertyChanged(); OnPropertyChanged("ListViewGridSize"); }
        }

        public int ScoreBoxScoreWidth
        {
            get { return _scoreBoxScoreWidth; }
            set { _scoreBoxScoreWidth = value; OnPropertyChanged(); OnPropertyChanged("ListViewGridSize"); }
        }

        public int ScoreBoxJudgeWidth
        {
            get { return _scoreBoxJudgeWidth; }
            set { _scoreBoxJudgeWidth = value; OnPropertyChanged(); OnPropertyChanged("ListViewGridSize"); }
        }

        public int ScoreBoxHeight
        {
            get { return _scoreBoxHeight; }
            set { _scoreBoxHeight = value; OnPropertyChanged(); }
        }

        public Brush BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; OnPropertyChanged(); }
        }

        public void SetWindowSize(int width, int height)
        {
            _windowWidth = width;
            _windowHeight = height;
            OnPropertyChanged("WindowResized");
        }

        /// <summary>
        /// Returns the size of the grid inside the listview. Between each cell its a space of 10. 5 spaces = 50
        /// </summary>
        public int ListViewGridSize
        {
            get { return _scoreBoxJudgeWidth + _scoreBoxJudgeWidth + _scoreBoxJudgeWidth + _scoreBoxPositionWidth + _scoreBoxScoreWidth + _scoreBoxTeamNameWidth + 65; }
        }

        public ObservableCollection<TeamData> TeamData
        {
            get { return _teamData; }
            set { _teamData = value; }
        }

        public void UpdateScorePosition()
        {
            int position = 1;

            var teamList = new List<TeamData>(_teamData).OrderByDescending(x => x.FinalScore).ToList();
            for (int i = 0; i < teamList.Count(); i++)
            {
                var thisItem = teamList[i];
                TeamData nextItem = null;
                if ((i + 1) < teamList.Count())
                    nextItem = teamList[i + 1];

                thisItem.Position = position;
                if (nextItem != null)
                {
                    if (thisItem.FinalScore == nextItem.FinalScore) { }
                    else
                    {
                        position++;
                    }
                }
            }

            OnPropertyChanged("TeamData");
        }

        public TextElement Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public int WindowWidth
        {
            get { return _windowWidth; }            
        }

        public string Judge1
        {
            get { return _judge1; }
            set { _judge1 = value; OnPropertyChanged(); }
        }

        public string Judge2
        {
            get { return _judge2; }
            set { _judge2 = value; OnPropertyChanged(); }
        }

        public LeaderboardData()
        {
            _teamData = new ObservableCollection<TeamData>();
            _windowWidth = 1024;
            _windowHeight = 768;
            _title = new TextElement();
            _boxJudgeFont = new Element();
            _boxScoreFont = new Element();
        }


        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
