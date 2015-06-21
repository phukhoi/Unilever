using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unilever.Handle
{
    public class Utils
    {
        public static object Temp { get; set; }

        public static void WakeUp(DevExpress.Xpf.Docking.LayoutPanel lp)
        {
            lp.Visibility = System.Windows.Visibility.Visible;
            lp.Closed = false;
            lp.IsActive = true;
        }
        public static void WakeUp(DevExpress.Xpf.Docking.DocumentPanel dp)
        {
            dp.Visibility = System.Windows.Visibility.Visible;
            dp.Closed = false;
            dp.IsActive = true;
        }    

    }
}
