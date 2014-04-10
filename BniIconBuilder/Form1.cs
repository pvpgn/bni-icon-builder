using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using BniIconBuilder.Properties;
using FreeImageAPI;
using Toolset.Controls;


namespace BniIconBuilder
{
    public partial class Form1 : Form
    {

        #region Open File Session

        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
                if (string.IsNullOrEmpty(_fileName))
                {
                    this.Text = Helper.ProgramName;
                    customDrawListBox1.Items.Clear();

                    // disable all controls on the form (except main menu)
                    foreach (var control in this.Controls)
                    {
                        if (control is MenuStrip)
                            continue;
                        ((Control) control).Enabled = false;
                    }
                    menuSave.Enabled = menuSaveAs.Enabled = menuExport.Enabled = false;
                }
                else
                {
                    // enable all controls on the form
                    foreach (var control in this.Controls)
                        ((Control)control).Enabled = true;

                    this.Text = Path.GetFileName(_fileName);
                    menuExport.Enabled = menuSaveAs.Enabled = true;
                }
            }
        }
        private string _fileName = null;

        // was there any modifications since save/open?
        public bool HasModified
        {
            get { return _hasModified; }
            set
            {
                _hasModified = value;
                menuSave.Enabled = BtnSave.Enabled = _hasModified;
            }
        }
        private bool _hasModified;
    
        /// <summary>
        /// Previous selected item in the list
        /// </summary>
        private int PrevSelectedIndex = -1;

        #endregion


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            lblWarning.Text = string.Empty;
            txtTag.Left = txtFlag.Left;
            txtTag.Top = txtFlag.Top;
            FileName = null;
            HasModified = false;
        }

        private void customDrawListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listBox = customDrawListBox1;

            // save changes of previous item
            if (PrevSelectedIndex >= 0)
                if ((string) txtTag.Tag != txtTag.Text ||
                    (string)txtFlag.Tag != txtFlag.Text ||
                    (string) txtId.Tag != txtId.Text ||
                    (pictureBoxIcon.Image != null && pictureBoxIcon.Tag != pictureBoxIcon.Image)) // sometimes on drag&drop image = null
                {
                    HasModified = true;
                    _saveCurrentIcon();
                }

            if (listBox.SelectedIndex < 0)
            {
                // disable controls
                txtFlag.Enabled = txtTag.Enabled = txtId.Enabled = radioFlag.Enabled = radioTag.Enabled = btnRemoveItem.Enabled = false;
                pictureBoxIcon.Image = null;
                pictureBoxIcon.BackColor = Color.DarkGray;
                pictureBoxIcon.Cursor = Cursors.Default;

                // clear previous index
                PrevSelectedIndex = -1;
                return;
            }
            // enable controls
            txtFlag.Enabled = txtTag.Enabled = txtId.Enabled = radioFlag.Enabled = radioTag.Enabled = btnRemoveItem.Enabled = true;
            pictureBoxIcon.BackColor = Color.Wheat;
            pictureBoxIcon.Cursor = Cursors.Hand;

            var item = (IconItem)listBox.Items[listBox.SelectedIndex];
            pictureBoxIcon.Image = item.Image;

            txtTag.Text = item.Tag;
            txtFlag.Text = string.Format("{0:x8}", item.Flag);
            txtId.Text = string.Format("{0:x8}", item.Id); 
           
            if (!string.IsNullOrEmpty(item.Tag))
                radioTag.Checked = true;
            else
                radioFlag.Checked = true;

            // save init values of the icon
            txtTag.Tag = txtTag.Text;
            txtFlag.Tag = txtFlag.Text;
            txtId.Tag = txtId.Text;
            pictureBoxIcon.Tag = pictureBoxIcon.Image;

            _validateValues();

            // update previous index
            PrevSelectedIndex = listBox.SelectedIndex;
        }

        private void pictureBoxIcon_Paint(object sender, PaintEventArgs e)
        {
            var picture = (PictureBox) sender;
            if (picture.Image == null)
                return;

            Color color;
            // valid size
            if (picture.Image.Width == 28 && picture.Image.Height == 14)
                color = Color.Tan;
            else
                color = Color.Red;

            // draw image size under icon inside picturebox
            e.Graphics.FillRectangle(new SolidBrush(color), new Rectangle(0, picture.Height - 23, picture.Width, 23));
            using (Font font = new Font("Arial", 7))
                e.Graphics.DrawString(picture.Image.Width + " x " + picture.Image.Height + " (24 bit/pixel)",
                                      font, Brushes.Black, 4, 7 + picture.Height - 26);
        }


        private void radioFlag_CheckedChanged(object sender, EventArgs e)
        {
            if (radioFlag.Checked)
            {
                lblFlagTag.Text = "Flag:";
                txtFlag.Visible = true;
                txtTag.Visible = false;
                _validateValues();
            }
        }

        private void radioTag_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTag.Checked)
            {
                lblFlagTag.Text = "Tag:";
                txtFlag.Visible = false;
                txtTag.Visible = true;
                _validateValues();
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            _validateValues();
        }

        private void txtFlagTag_TextChanged(object sender, EventArgs e)
        {
            _validateValues();
        }


        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            customDrawListBox1.Items.RemoveAt(customDrawListBox1.SelectedIndex);
            HasModified = true;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            var bitmap = _loadBitmapWithDialog();
            if (bitmap == null)
                return;

            // get index to insert new item here
            var index = (PrevSelectedIndex >= 0) ? PrevSelectedIndex+1 : customDrawListBox1.Items.Count;

            customDrawListBox1.Items.Insert(index, new IconItem()
            {
                Image = bitmap,
                Width = bitmap.Width,
                Height = bitmap.Height
            });
            customDrawListBox1.SelectedIndex = index;
            HasModified = true;
        }


        private void customDrawListBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (customDrawListBox1.SelectedItem == null) 
                return;
            customDrawListBox1.DoDragDrop(customDrawListBox1.SelectedItem, DragDropEffects.Move);

            if (PrevSelectedIndex != customDrawListBox1.SelectedIndex)
                customDrawListBox1_SelectedIndexChanged(sender, null);
        }

        private void customDrawListBox1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void customDrawListBox1_DragDrop(object sender, DragEventArgs e)
        {

            Point point = customDrawListBox1.PointToClient(new Point(e.X, e.Y));
            int index = customDrawListBox1.IndexFromPoint(point);
            if (index < 0 || index == customDrawListBox1.SelectedIndex)
                return;

            object data = e.Data.GetData(typeof(IconItem));

            customDrawListBox1.Items.Remove(data);
            customDrawListBox1.Items.Insert(index, data);

            customDrawListBox1.SelectedIndex = index;
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            var d = new OpenFileDialog();
            d.Filter = "BNI Archive (*.bni)|*.bni";
            if (d.ShowDialog() != DialogResult.OK)
                return;

            // set global filename
            if (string.IsNullOrEmpty(FileName = d.FileName))
                return;

            try
            {
                customDrawListBox1.Items.Clear();

                var tga = new Targa();
                var bni = new BniFile();

                // load file to structure
                var bn = bni.Load(FileName);
                // get icon images
                var images = tga.SplitImage(bn.Data, 14);

                if (bn.Icons.Length != images.Length)
                    throw new Exception(String.Format("Icons count ({0}) is not equals with images count ({1})", bn.Icons.Length, images.Length));
                if (bn.Icons.Length == 0)
                    throw new Exception(String.Format("No icons found"));


                // put each image corresponding to it icon in bni structure
                for (int i = 0; i < images.Length; i++)
                {
                    bn.Icons[i].Image = images[i];
                    customDrawListBox1.Items.Add(bn.Icons[i]);
                }
                 
                // set first icon
                customDrawListBox1.SelectedIndex = 0;

                HasModified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileName = null;
            }

        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            _saveCurrentIcon();

            var d = new SaveFileDialog();
            d.Filter = "BNI Archive (*.bni)|*.bni";
            d.CheckPathExists = true;

            if (d.ShowDialog() != DialogResult.OK)
                return;

            var fileName = d.FileName;

            var tga = new Targa();
            try
            {
                var images = _getImagesFromList();

                // generate full tga image
                var data = tga.CreateTga(images);
                // get icons
                var icons = _getIconItemsFromList();

                // save to file
                var bni = new BniFile();
                bni.Save(icons, data, fileName);

                HasModified = false;
                MessageBox.Show(string.Format("File saved to {0}", fileName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            _saveCurrentIcon();

            var tga = new Targa();
            try
            {
                var images = _getImagesFromList();

                // generate full tga image
                var data = tga.CreateTga(images);
                // get icons
                var icons = _getIconItemsFromList();

                // save to file
                var bni = new BniFile();
                bni.Save(icons, data, FileName);

                HasModified = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void menuExport_Click(object sender, EventArgs e)
        {
            string fileName = null;
            string path = null;

            var q = MessageBox.Show("Do you want to export all icons as a single image?", "Export As...", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            // export single image
            if (q == DialogResult.Yes)
            {
                var d = new SaveFileDialog();
                d.Filter = "Targa image (*.tga)|*.tga";
                d.CheckPathExists = true;

                if (d.ShowDialog() != DialogResult.OK)
                    return;

                fileName = d.FileName;
            }
            // export many images
            else
            {
                var d = new FolderBrowserDialog();
                if (d.ShowDialog() != DialogResult.OK)
                    return;

                path = d.SelectedPath;
            }


            var tga = new Targa();
            try
            {
                var images = _getImagesFromList();

                // export image to a file(s)
                if (fileName != null)
                {
                    tga.ExportTga(images, fileName);
                    MessageBox.Show(string.Format("Image saved to {0}", fileName));
                }
                else
                {
                    var icons = _getIconItemsFromList();
                    var files = new string[icons.Length];
                    string _name;
                    for (int i = 0; i < icons.Length; i++)
                    {
                        _name = icons[i].Flag > 0 
                            ? string.Format("0x{0:x8}", icons[i].Flag) 
                            : icons[i].Tag;
                        files[i] = Path.Combine(path, _name + ".tga");
                    }
                        
                    tga.ExportTgaMany(images, files);
                    MessageBox.Show(string.Format("Images saved to {0}", path));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Export icon images from the listbox
        /// </summary>
        /// <returns></returns>
        private Bitmap[] _getImagesFromList()
        {
            // get images from listbox
            var images = new Bitmap[customDrawListBox1.Items.Count];
            for (int i = 0; i < customDrawListBox1.Items.Count; i++)
                images[i] = ((IconItem)customDrawListBox1.Items[i]).Image;

            return images;
        }
        /// <summary>
        /// Export icons from the listbox
        /// </summary>
        /// <returns></returns>
        private IconItem[] _getIconItemsFromList()
        {
            // get images from listbox
            var icons = new IconItem[customDrawListBox1.Items.Count];
            for (int i = 0; i < customDrawListBox1.Items.Count; i++)
                icons[i] = (IconItem)customDrawListBox1.Items[i];

            return icons;
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void pictureBoxIcon_Click(object sender, EventArgs e)
        {
            var bitmap = _loadBitmapWithDialog();
            if (bitmap == null)
                return;
            
            // replace image in picturebox
            pictureBoxIcon.Image = bitmap;
            HasModified = true;
        }


        private Bitmap _loadBitmapWithDialog()
        {
            Bitmap bitmap;

            var d = new OpenFileDialog();
            d.Filter = "Image Files (*.BMP;*ICO;*.JPG;*JIF;*JPEG;*JPE;*.MNG;*PCX;*PNG;*TGA;*TIFF;*PSD;*GIF)|*.BMP;*ICO;*.JPG;*JIF;*JPEG;*JPE;*.MNG;*PCX;*PNG;*TGA;*TIFF;*PSD;*GIF";
            d.CheckFileExists = true;

            if (d.ShowDialog() != DialogResult.OK)
                return null;

            var tga = new Targa();
            try
            {
                // get image bitmap
                bitmap = tga.Open(d.FileName);
                if (bitmap == null)
                    throw new Exception();
            }
            catch
            {
                MessageBox.Show(string.Format("Could not open image {0}", d.FileName), "Unknown image format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return bitmap;
        }



        /// <summary>
        /// Replace current icon changes
        /// </summary>
        /// <param name="image"></param>
        private void _saveCurrentIcon()
        {
            if (PrevSelectedIndex < 0 || !HasModified)
                return;

            // reset init values
            txtFlag.Tag = txtFlag.Text;
            txtTag.Tag = txtTag.Text;
            txtId.Tag = txtId.Text;
            pictureBoxIcon.Tag = pictureBoxIcon.Image;


            var item = (IconItem) customDrawListBox1.Items[PrevSelectedIndex];
            // flag set
            if (radioFlag.Checked)
            {
                item.Flag = Helper.ConvertToInt32(txtFlag.Text);
                item.Tag = null;
            }
            // tag set
            else
            {
                item.Flag = 0;
                // tag must have 4 characters without spaces
                item.Tag = txtTag.Text.Replace(' ', '_').PadRight(4, '_').Substring(0, 4).ToUpper();
            }
            // id set
            item.Id = Helper.ConvertToInt32(txtId.Text);
 

            // replace image in listbox
            item.Image = (Bitmap)pictureBoxIcon.Image;

            // update item
            customDrawListBox1.Items[PrevSelectedIndex] = item;
            customDrawListBox1.Refresh();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            menuSave_Click(null, null);
        }



        /// <summary>
        /// Check values in the textboxes
        /// </summary>
        private void _validateValues()
        {
            lblWarning.Text = "";

            int value;
            // flag
            if (radioFlag.Checked)
            {
                if (!Helper.ConvertToInt32(txtFlag.Text, out value))
                    lblWarning.Text = "Only hex value is allowed for the flag.";
                if (value == 0)
                    lblWarning.Text = "Flag value must be > 0.";
            }
            // tag
            else if (txtTag.Text.Length != 4 || txtTag.Text.Contains(" "))
                lblWarning.Text = "Tag must have 4 characters of length without spaces.";

            // id
            if (!Helper.ConvertToInt32(txtId.Text, out value))
                lblWarning.Text = "Only hex value is allowed for the id.";

            // check values on current item only
            if (customDrawListBox1.SelectedIndex == PrevSelectedIndex)
                if (txtFlag.Text != txtFlag.Tag || txtTag.Text != txtTag.Tag || txtId.Text != txtId.Tag)
                    HasModified = true;
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            new DialogAbout().ShowDialog();
        }

    }
}
