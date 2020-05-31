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
        public Dictionary<DayOfWeek, int> Value { get; set; }

        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

    }
}
