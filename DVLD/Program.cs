using DVLD_WithoutUC.Aplications;
using DVLD_WithoutUC.test;
using DVLD_WithoutUC.TestT_ypes;
using DVLD_WithoutUC.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_WithoutUC
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new frmMainScreen());
            //Application.Run(new frmLogin());
            //Application.Run(new frmListTestTypes());
            //Application.Run(new frmManageAplicationTypes());
            //Application.Run(new frmAddPersonWithFilter());
            //Application.Run(new frmAddUpdateUSer());
            //Application.Run(new frmListUsers());
        }
    }
}
