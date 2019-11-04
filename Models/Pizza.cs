using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Pizza : IDataEntity<string>
    {
        public IEnumerable<string> toppings { get; set; }

        public Pizza()
        {

        }

        public string Combination { 
            get
            {
                if (toppings==null|| !toppings.Any()) { return String.Empty; }
                return String.Join(',', toppings.OrderBy(t=>t));
            }
        }
        public int HowOften{ get;set; }
        public int HowOftenRank { get; set; }

        public static Pizza EmptyPizza
        {
            get
            {
                return new Pizza() { toppings = new List<string>() } ;
            }
        }

        public static List<Pizza> NoPizza
        {
            get
            {
                return new List<Pizza>() { Pizza.EmptyPizza };
            }
        }
    }
}
