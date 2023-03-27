namespace TestTask.Models
{
    /// <summary>
    /// Класс TestItem 
    /// Класс модель тестовых входных данных
    /// </summary>
    internal class TestItem
    {
        private string user;
        private int rank;
        private int steps;
        private string status;

        public string User { get => user; set => user = value; }
        public int Rank { get => rank; set => rank = value; }
        public int Steps { get => steps; set => steps = value; }
        public string Status { get => status; set => status = value; }
    }
}
