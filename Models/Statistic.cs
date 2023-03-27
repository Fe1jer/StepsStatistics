namespace TestTask.Models
{
    /// <summary>
    /// Класс Statistic 
    /// Класс модель статистики
    /// </summary>
    public class Statistic
    {
        private int day;
        private int steps;
        private int rank;
        private string status;

        public int Day { get => day; set => day = value; }
        public int Steps { get => steps; set => steps = value; }
        public int Rank { get => rank; set => rank = value; }
        public string Status { get => status; set => status = value; }
    }
}
