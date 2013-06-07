using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Trax.Leaderboard
{
    public interface ILeaderboardData : IBaseWindow, INotifyPropertyChanged
    {
        TextElement Title { get; set; }        
        string Judge1 { get; set; }
        string Judge2 { get; set; }
        string Judge3 { get; set; }        
        
        #region Judge boxes
        int JudgeOffset { get; set; }
        Brush BoxJudgeColor { get; set; }
        Element BoxJudgeFont { get; set; }
        int JudgeBoxWidth { get; set; }
        int JudgeBoxHeight { get; set; }
        #endregion

        #region Score boxes        
        Brush BoxScoreColor { get; set; }        
        Element BoxScoreFont { get; set; }
        int ScoreBoxPositionWidth { get; set; }
        int ScoreBoxTeamNameWidth { get; set; }
        int ScoreBoxScoreWidth { get; set; }
        int ScoreBoxJudgeWidth { get; set; }
        int ScoreBoxHeight { get; set; }
        #endregion
        int ListViewGridSize { get; }

        ObservableCollection<TeamData> TeamData { get; set; }
        void UpdateScorePosition();
    }
}
