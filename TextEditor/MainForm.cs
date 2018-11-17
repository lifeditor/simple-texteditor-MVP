using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TextEditor
{
    public interface IMainForm
    {
        string filePath { get; }
        string content { get; set; }
        void SetSymbolCount(int count);
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }


    public partial class MainForm : Form, IMainForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public string filePath
        {
            get { return dlgOpenFile.FileName; }
        }

        public string content
        {
            get { return textContent.Text; }
            set { textContent.Text = value; }
        }

        public void SetSymbolCount(int count)
        {
            lblSymbolCount.Text = count.ToString();
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            dlgOpenFile.ShowDialog();
        }

        private void dlgOpenFile_FileOk(object sender, CancelEventArgs e)
        {
            FileOpenClick?.Invoke(this, EventArgs.Empty);
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            FileSaveClick?.Invoke(this, EventArgs.Empty);
        }

        private void numFont_ValueChanged(object sender, EventArgs e)
        {
            textContent.Font = new Font("Calibri", (float)numFont.Value);
        }

        private void textContent_TextChanged(object sender, EventArgs e)
        {
            ContentChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
