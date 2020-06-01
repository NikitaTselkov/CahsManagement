using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    /// <summary>
    /// Доходы / Расходы
    /// </summary>
    public class IncomeExpenses
    {
        /// <summary>
        /// Название.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Список значений.
        /// </summary>
        public Dictionary<DayOfWeek, int> Value { get; set; } = new Dictionary<DayOfWeek, int>();

        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }



        public override string ToString()
        {
            var rnd = new Random(Guid.NewGuid().ToByteArray().Sum(s => s));

            var RandomValue = rnd.Next(1, 6);

            return String.Format("Id: {0}, Title: {1}, DayOfWeek: {2}, {3}", Id, Title, Value.First(f => f.Key == (DayOfWeek)RandomValue).Key, Value.First(f => f.Key == (DayOfWeek)RandomValue).Value);
        }
    }
}
