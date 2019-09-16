using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestAsyncAndAwait
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var task = await Save();
            MessageBox.Show(task);
        }
        public async Task<string> Save()
        {
            button1.IsEnabled = false;
            var wc = new WebClient();
            var result = await wc.DownloadStringTaskAsync("https://www.baidu.com");
            await Task.Run(() =>
            {
                MessageBox.Show(result);
            });
            button1.IsEnabled = true;

            return "Success！";
        }
    }
}
