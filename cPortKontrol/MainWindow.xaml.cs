using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Threading;

namespace cPortKontrol
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnControl_Click(object sender, RoutedEventArgs e)
        {
            lblStatus.Foreground = new SolidColorBrush(Colors.Black);
            // Check Port if true then port open
            if (PortControl(txtHost.Text, Convert.ToInt32(txtPort.Text)))
            {
                lblStatus.Foreground = new SolidColorBrush(Colors.Green);
                lblStatus.Content = "Status:\nSucces: Port open!";
            }
            else // Port close
            {
                lblStatus.Foreground = new SolidColorBrush(Colors.Red);
                lblStatus.Content = "Status:\nFail: Port close!";
            }
        }

        public bool PortControl(string host, int port)
        {

            try
            {
                // IF host is URL then get host address
                Uri uri = new Uri(host);
                host = uri.Host;
            }
            catch (Exception) { }

            IPHostEntry hostEntry;
            hostEntry = Dns.GetHostEntry(host);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                var ip = hostEntry.AddressList[0];
                s.Connect(ip, port); 
                return true;
            }
            catch (Exception) 
            {
                return false;
            }
        }

       
    }
}
