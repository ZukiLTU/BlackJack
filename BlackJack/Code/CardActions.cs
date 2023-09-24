using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackJack.Code
{
    public class CardActions
    {
        public static void ChangeCardColor(Bitmap bitmap,Form currentForm, List<bool> bools, bool boolToActivate)
        {
            foreach (var item in bools)
            {
                if(item == boolToActivate)
                {
                    item = true;
                }
                else item = false;
            }
            //gets all pictureboxes in the form
            var pictureboxes = currentForm.Controls.OfType<PictureBox>();
            foreach (var item in pictureboxes)
            {
                item.Image = bitmap;
            }
        }
        public static void ChangeCardColor(Bitmap bitmap, Form currentForm)
        {
            //gets all pictureboxes in the form
            var pictureboxes = currentForm.Controls.OfType<PictureBox>();
            foreach (var item in pictureboxes)
            {
                item.Image = bitmap;
            }
        }
    }
}
