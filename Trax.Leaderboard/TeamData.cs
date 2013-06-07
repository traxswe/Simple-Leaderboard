using System.ComponentModel;
using System.Runtime.CompilerServices;
using Trax.Leaderboard.Annotations;

namespace Trax.Leaderboard
{
    public class TeamData : INotifyPropertyChanged
    {
        private static int _teamDataId = 0;

        public int Id { get { return _id; } }
        public int Position { get { return _position; } set { _position = value; OnPropertyChanged(); } }
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        public int PointsJudge1 { get { return _pointsJudge1; } set { _pointsJudge1 = value; OnPropertyChanged(); OnPropertyChanged("FinalScore"); } }
        public int PointsJudge2 { get { return _pointsJudge2; } set { _pointsJudge2 = value; OnPropertyChanged(); OnPropertyChanged("FinalScore"); } }
        public int PointsJudge3 { get { return _pointsJudge3; } set { _pointsJudge3 = value; OnPropertyChanged(); OnPropertyChanged("FinalScore"); } }

        private int _id;
        private int _position;
        private string _name;
        private int _pointsJudge1;
        private int _pointsJudge2;
        private int _pointsJudge3;

        public int FinalScore
        {
            get { return PointsJudge1 + PointsJudge2 + PointsJudge3; }
        }

        public TeamData()
        {
            _id = _teamDataId;
            _teamDataId++;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
