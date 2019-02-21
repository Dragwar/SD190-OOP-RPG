using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem
{
    public class MenuOption
    {
        public string Title { get; set; }
        public Action Callback { get; set; }

        public MenuOption(string title, Action callback)
        {
            Title = title;
            Callback = callback;
        }



    }
}
