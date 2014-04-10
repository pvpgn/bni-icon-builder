using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Drawing;
using BniIconBuilder;

namespace Toolset.Controls
{
    public class CustomDrawListBox : ListBox
    {
        public CustomDrawListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable; // We're using custom drawing.
            this.ItemHeight = 25; // Set the item height.
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // Make sure we're not trying to draw something that isn't there.
            if (e.Index >= this.Items.Count || e.Index <= -1)
                return;

            

            // Get the item object.
            var it = this.Items[e.Index];
            if (!(it is IconItem))
                return;
            IconItem item = (IconItem)it;
            if (item.Image == null)
                return;


            // Draw the background color depending on 
            // if the item is selected or not.
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                // The item is selected.
                // We want a blue background color.
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkSeaGreen), e.Bounds);
            }
            else
            {
                // The item is NOT selected.
                // We want a white background color.
                e.Graphics.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            }

            // icon title
            string text;
            if (item.Tag != null)
                text = item.Tag;
            else
                text = string.Format("0x{0:x8}", item.Flag);
            
            // draw the text.
            var font = new Font (this.Font, FontStyle.Bold);
            SizeF stringSize = e.Graphics.MeasureString(text, font);
            e.Graphics.DrawString(text, font, new SolidBrush(Color.Black),
                new PointF(60, e.Bounds.Y + (e.Bounds.Height - stringSize.Height) / 2));

            // draw the bitmap
            Image image = new Bitmap(item.Image);
            e.Graphics.DrawImage(image, 15, e.Bounds.Y + 6, item.Width, item.Height > this.ItemHeight - 6 ? this.ItemHeight - 6 : item.Height);
            e.Graphics.Save();
        }
    }
}