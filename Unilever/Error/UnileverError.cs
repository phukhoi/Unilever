using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;

namespace Unilever.Error
{
   public class UnileverError
    {
       public const string CONNECT_DB_ERR = "Error when connecting database";
       public const string CAPTION_ERROR = "Error";

       public static void Show(string err, string caption, DevExpress.LookAndFeel.UserLookAndFeel lookAndFeel = null)
       {
           XtraMessageBox.Show(lookAndFeel, CONNECT_DB_ERR, CAPTION_ERROR);
       }
    }
}
