using System.Collections.Generic;
using System.Linq;

namespace TestTask.Models
{
    /// <summary>
    /// Класс TestItem 
    /// Класс модель пользователя
    /// </summary>
    public class User
    {
        private string name;
        private List<Statistic> statistics;

        public User(string userName, List<Statistic> statistics)
        {
            name = userName;
            this.statistics = statistics;
        }

        public string Name { get => name; set => name = value; }
        public List<Statistic> Statistics { get => statistics; set => statistics = value; }
        public int AvgSteps { get => (int)statistics.Average(p => p.Steps); }
        public int MaxSteps { get => statistics.Max(p => p.Steps); }
        public int MinSteps { get => statistics.Min(p => p.Steps); }
    }
}
