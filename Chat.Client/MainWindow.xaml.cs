using System.Windows;
using Chat.Client.ServiceChat;

namespace Chat.Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        private bool _isConnected = false;
        private ServiceChatClient _client;
        private int _userId;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void ConnectUser() 
        { 
            if (!_isConnected) 
            {
                _client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                _userId = _client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                bToggleConnection.Content = "Отключиться";
                _isConnected = true;
            }
        }

        private void DisconnectUser() 
        {
            if (_isConnected)
            {
                _client.Disconnect(_userId);
                _client = null;
                tbUserName.IsEnabled = true;
                bToggleConnection.Content = "Подключиться";
                _isConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_isConnected)
                DisconnectUser();
            else
                ConnectUser();
        }

        public void MessageCallback(string message)
        {
            lbChat.Items.Add(message);

            if (lbChat.AlternationCount > 1)
                lbChat.ScrollIntoView(lbChat.Items[lbChat.AlternationCount - 1]);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter) 
            {
                if (_client != null) 
                { 
                    _client.SendMessage(tbMessage.Text, _userId);
                    tbMessage.Text = string.Empty;
                }
            }
        }
    }
}
