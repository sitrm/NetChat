using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
//описание интерфейса 
namespace wcfChat
{
    // когда клиент образается к сервису, то сервер для каждого клиента солздает свой экземпляр сервиса   
    // но у нас чат - сервис должен знать обо всех клиентах, которые подключились к сервису 
    // все клиенты работают с одним сервисом 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>(); // инициализируем юзеров 
        int nextID = 1;


        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                ID = nextID, 
                Name = name,
                operationContext =  OperationContext.Current

            };
            nextID++; // увеличили ID

            SendMsg(" " + user.Name + " connected to the chat!", 0);

            users.Add(user);            // добавили его в список пользователей  

            return user.ID; 
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(x => x.ID == id);
            if (user != null)  
            { 
                users.Remove(user);
                SendMsg(" " +  user.Name + " disconnected from the chat!", 0);
            }
        }

        public void SendMsg(string msg, int id)
        {

            foreach(var item in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(x => x.ID == id);
                if(user  != null)
                {
                    answer += "---(" + user.Name + "): ";
                }

                answer += msg;    // добавляем сообщение которое хотим отправит

                // а теперь нужно отправить это сообщение user обратно c gjvjom Callback метода 
                // но реализация на стороне клиента 
                // здесь только вызываем 
                item.operationContext.GetCallbackChannel<IServiceChatCallback>().MsgCallback(answer);
            }
            
        }
    }
}
