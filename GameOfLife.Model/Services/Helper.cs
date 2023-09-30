using GameOfLife.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Model.Services
{
    public static class Helper
    {
        public static decimal compareEnergy(int a1, int a2)
        {
            var e1 = Math.Max(a1, a2);
            var e2 = Math.Min(a1, a2);

            decimal percentage = Convert.ToDecimal(Convert.ToDecimal(e2) / (Convert.ToDecimal(e1 + e2)));

            return percentage;
        }
    }
}
