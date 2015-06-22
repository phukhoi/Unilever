using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;

namespace Unilever.Handle
{
    public class UnileverError
    {
        public const string CONNECT_DB_ERRORMSG = "Error when connecting database";
        public const string SAVE_DB_ERRORMSG = "Something fail when saving data to database, pls try again";

        public const string ERR_CAPTION = "Error";
        public const string WARN_CAPTION = "Warning";

        public static void Show(string err, string caption,
            System.Windows.Forms.MessageBoxIcon type = System.Windows.Forms.MessageBoxIcon.Error,
            DevExpress.LookAndFeel.UserLookAndFeel lookAndFeel = null)
        {
            //System.Drawing.Icon icon = new System.Drawing.Icon();
            XtraMessageBox.Show(lookAndFeel, err, caption,
                System.Windows.Forms.MessageBoxButtons.OK,
                type,
                DevExpress.Utils.DefaultBoolean.True);
        }
    }
}
