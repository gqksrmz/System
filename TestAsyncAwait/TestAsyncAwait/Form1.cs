using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAsyncAwait
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            var task = Save();
            await task;
        }
        public async Task Save()
        {
            var wc = new WebClient();
            var result = await wc.DownloadStringTaskAsync("https://www.baidu.com");
            await Task.Run(() =>
            {
                MessageBox.Show(result);

            });
        }
    }
}
