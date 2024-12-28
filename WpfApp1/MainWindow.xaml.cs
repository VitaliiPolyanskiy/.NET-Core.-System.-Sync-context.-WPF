using System;
using System.Windows;
using System.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ThreadFunk()
        {
            try
            {
                Dispatcher.Invoke(() => progressBar1.Minimum = 0);
                Dispatcher.Invoke(() => progressBar1.Maximum = 230);
                Dispatcher.Invoke(() => progressBar1.Value = 0);
                Dispatcher.Invoke(() => button1.IsEnabled = false);

                for (int i = 0; i < 230; i++)
                {
                    Thread.Sleep(50);
                    Dispatcher.Invoke(() => progressBar1.Value = i);
                }
                Dispatcher.Invoke(() => button1.IsEnabled = true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Создание делегата функции, в которой будет работать новый поток
            ThreadStart MethodThread = new ThreadStart(ThreadFunk);
            // Создание объекта потока
            Thread thread = new Thread(MethodThread);
            thread.IsBackground = true;
            // Старт потока
            thread.Start();
        }
    }
}
