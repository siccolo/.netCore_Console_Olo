using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConfig
{
    public interface IConfig 
    {
        string DataUrl { get;  }
    }

    public class AppConfig:IConfig
    {
        public string DataUrl { get; set; }
    }
}
