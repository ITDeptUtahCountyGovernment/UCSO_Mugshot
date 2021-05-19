/****************************************************************************
While the underlying libraries are covered by LGPL, this sample is released 
as public domain.  It is distributed in the hope that it will be useful, but 
WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
or FITNESS FOR A PARTICULAR PURPOSE.  
*****************************************************************************/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.IO;




namespace SnapShot
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Button button_snap;
        private System.Windows.Forms.PictureBox pictureBox1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.PictureBox pictureBox2;
        private TextBox textBox_default_file_path;
        private Label label1;
        private Button button_close;
        private ListBox listBox2;
        private Label label2;
        private Button button_clear_listbox;
        private Label label3;
        private Capture cam;

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            const int VIDEODEVICE = 0; // zero based index of video capture device to use
            const int VIDEOWIDTH = 320; // Depends on video device caps
            const int VIDEOHEIGHT = 240; // Depends on video device caps
            const int VIDEOBITSPERPIXEL = 24; // BitsPerPixel values determined by device

            cam = new Capture(VIDEODEVICE, VIDEOWIDTH, VIDEOHEIGHT, VIDEOBITSPERPIXEL, pictureBox2);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );

            if (m_ip != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(m_ip);
                m_ip = IntPtr.Zero;
            }
        }

		#region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
      this.button_snap = new System.Windows.Forms.Button();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.textBox_default_file_path = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.button_close = new System.Windows.Forms.Button();
      this.listBox2 = new System.Windows.Forms.ListBox();
      this.label2 = new System.Windows.Forms.Label();
      this.button_clear_listbox = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
      this.SuspendLayout();
      // 
      // button_snap
      // 
      this.button_snap.BackColor = System.Drawing.Color.Lime;
      this.button_snap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button_snap.Location = new System.Drawing.Point(356, 508);
      this.button_snap.Name = "button_snap";
      this.button_snap.Size = new System.Drawing.Size(121, 40);
      this.button_snap.TabIndex = 0;
      this.button_snap.Text = "Snap/Capture";
      this.button_snap.UseVisualStyleBackColor = false;
      this.button_snap.Click += new System.EventHandler(this.button1_Click);
      // 
      // pictureBox2
      // 
      this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox2.Location = new System.Drawing.Point(16, 8);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new System.Drawing.Size(640, 480);
      this.pictureBox2.TabIndex = 2;
      this.pictureBox2.TabStop = false;
      // 
      // pictureBox1
      // 
      this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.pictureBox1.Location = new System.Drawing.Point(16, 508);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(320, 240);
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      // 
      // textBox_default_file_path
      // 
      this.textBox_default_file_path.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox_default_file_path.Location = new System.Drawing.Point(484, 520);
      this.textBox_default_file_path.Name = "textBox_default_file_path";
      this.textBox_default_file_path.Size = new System.Drawing.Size(172, 20);
      this.textBox_default_file_path.TabIndex = 3;
      this.textBox_default_file_path.Text = "c:\\pbapps\\mugshot";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(484, 547);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(173, 13);
      this.label1.TabIndex = 4;
      this.label1.Text = "Default Path = C:\\pbapps\\mugshot";
      // 
      // button_close
      // 
      this.button_close.BackColor = System.Drawing.Color.Red;
      this.button_close.ForeColor = System.Drawing.Color.White;
      this.button_close.Location = new System.Drawing.Point(662, 12);
      this.button_close.Name = "button_close";
      this.button_close.Size = new System.Drawing.Size(102, 23);
      this.button_close.TabIndex = 5;
      this.button_close.Text = "Close Application";
      this.button_close.UseVisualStyleBackColor = false;
      this.button_close.Click += new System.EventHandler(this.button_close_Click);
      // 
      // listBox2
      // 
      this.listBox2.FormattingEnabled = true;
      this.listBox2.Location = new System.Drawing.Point(356, 572);
      this.listBox2.Name = "listBox2";
      this.listBox2.ScrollAlwaysVisible = true;
      this.listBox2.Size = new System.Drawing.Size(300, 160);
      this.listBox2.TabIndex = 7;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(355, 555);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(79, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Saved Pictures";
      // 
      // button_clear_listbox
      // 
      this.button_clear_listbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
      this.button_clear_listbox.Location = new System.Drawing.Point(356, 731);
      this.button_clear_listbox.Name = "button_clear_listbox";
      this.button_clear_listbox.Size = new System.Drawing.Size(137, 23);
      this.button_clear_listbox.TabIndex = 9;
      this.button_clear_listbox.Text = "Clear Saved Pictures List";
      this.button_clear_listbox.UseVisualStyleBackColor = false;
      this.button_clear_listbox.Click += new System.EventHandler(this.button_clear_listbox_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(15, 490);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(106, 13);
      this.label3.TabIndex = 10;
      this.label3.Text = "Snap/Capture Image";
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(776, 762);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.button_clear_listbox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.listBox2);
      this.Controls.Add(this.button_close);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox_default_file_path);
      this.Controls.Add(this.pictureBox1);
      this.Controls.Add(this.pictureBox2);
      this.Controls.Add(this.button_snap);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Name = "Form1";
      this.Text = "UCSO Mugshot Capture";
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
      this.Load += new System.EventHandler(this.Form1_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }
		#endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Form1());
        }

        IntPtr m_ip = IntPtr.Zero;

        private void button1_Click(object sender, System.EventArgs e)
        {
            string csPictureSavePath = "";
            csPictureSavePath = textBox_default_file_path.Text;
            if (common.DoesDirectoryExist(csPictureSavePath) == false)
            {
              string csError = "";
              common.CreateDirectory("c:\\pbapps\\mugshot\\", ref csError);
            }
            Cursor.Current = Cursors.WaitCursor;
            
            // Release any previous buffer
            if (m_ip != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(m_ip);
                m_ip = IntPtr.Zero;
            }

            // capture image
            m_ip = cam.Click();
            Bitmap b = new Bitmap(cam.Width, cam.Height, cam.Stride, PixelFormat.Format24bppRgb, m_ip);

            // If the image is upsidedown
            b.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = b;


            string csPictureFileName = ""; //mmddyyhhmmss.jpg
            DateTime d = DateTime.Now;
            string dateString = d.ToString("MMddyy_HHmmss");
            csPictureFileName = dateString + ".jpg";
            string csPictureFullFilename = csPictureSavePath + "\\" + csPictureFileName;
            pictureBox1.Image.Save(csPictureFullFilename, ImageFormat.Jpeg);
            listBox2.Items.Add(csPictureFullFilename);
            Cursor.Current = Cursors.Default;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            cam.Dispose();
            if (m_ip != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(m_ip);
                m_ip = IntPtr.Zero;
            }
        }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void button_close_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void button_clear_listbox_Click(object sender, EventArgs e)
    {
      listBox2.Items.Clear();
    }
  }
}
