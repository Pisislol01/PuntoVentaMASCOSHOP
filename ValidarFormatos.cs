using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace MASCOSHOP
{
    class ValidarFormatos
    {
        public static bool ValidarCampoDecimal(TextBox CajaDeTexto)
        {
            try
            {
                decimal d = Convert.ToDecimal(CajaDeTexto.Text);
                return true;
            }
            catch (Exception ex)
            {
                CajaDeTexto.Text = "";
                return false;
            }
        }
        public static bool ValidarCampoInt(TextBox CajaDeTexto)
        {
            try
            {
                int d = Convert.ToInt32(CajaDeTexto.Text);
                return true;
            }
            catch (Exception ex)
            {
                CajaDeTexto.Text = "";
                return false;
            }
        }
    }
}
