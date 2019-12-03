using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ILemmatization
    {
        Task<double> GetIndexOfPositive(string text);
    }
}
