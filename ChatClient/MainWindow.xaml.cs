using System;
using System.Collections.Generic;
using System.Linq;
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
using ChatClient.ServiceChat;
//--------------------------------------------------------------------------------------------------
namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ServiceChat.IServiceChatCallback
    {
        //чиста клиентская часть
        bool isConnected = false;
        ServiceChat.ServiceChatClient  client; // объект типа нашего хоста в нашем клиенте чтобы взаимодейтсвовать с методами 
        int ID;

        public MainWindow()
        {
            InitializeComponent();
        }
           
        //свойство  - загрузка окна 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //создаем клиента выделяе память и передаем 
            client = new ServiceChat.ServiceChatClient(new System.ServiceModel.InstanceContext(this));

        }


        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChat.ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);     // коннектим юзера с textbox; return ID

                tbUserName.IsEnabled = false;
                bConDicon.Content = "Disconnect"; 
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                
                    
                client.Disconnect(ID);                // отключаем 
                client = null;
                tbUserName.IsEnabled = true;
                bConDicon.Content = "Connect";
                isConnected = false;
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
            

        }
        //колбэк метод который отвечает пользователю( то есть функция сервера)
        public void MsgCallback(string msg)
        {
            // вывод на экран служебной инфы от сервера 
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);   // скролл до последнего элемента 
        }


        // при закрытии окна 
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();


        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            // обработка событый нажатия клавиш
            if (e.Key == Key.Enter)
            {
                if (client != null)     // уже подключились
                {
                    if(tbMessage.Text != "")  // хоть что то написано
                    {
                        client.SendMsg(tbMessage.Text, ID);     // отправляем сообщение 
                        tbMessage.Text = string.Empty;
                    }
                    
                }
            }
        }   
    }
}
