using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack.Code.States
{
    public class BaseState
    {
        public BaseState() { }
        public BaseState(string name) { }

        protected DialogResult DialogResult{ get; set; }
    }
}
