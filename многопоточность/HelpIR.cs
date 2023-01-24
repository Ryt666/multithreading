using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multithreading
{
    public static class HelpIR
    {
        public static void helpIR(this Control control,Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
                action();
        }
    }
}
