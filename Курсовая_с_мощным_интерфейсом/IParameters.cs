using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсовая_с_мощным_интерфейсом
{
    public interface IParameters
    {
        float Length { get; set; }
        float Width { get; set; }
        float Height { get; set; }
        float Volume { get;}
        ushort Weight { get; set; }
        ushort Current_Floor { get; set; }
    }
}
