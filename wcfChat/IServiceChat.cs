using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
//интерфейс - что может делать сервис 
namespace wcfChat
{
    // чтобы сервер знал, что на клиентской части есть такой интерфейс 
    [ServiceContract(CallbackContract = typeof(IServiceChatCallback))]                       // договоренность взаимодействия 
    public interface IServiceChat
    {
        [OperationContract]       // атрибут - виден клиенту 
        int Connect(string name);

        [OperationContract]      
        void Disconnect(int id);         // клиент покидает чат - чтобы сообщить сервису 

        [OperationContract (IsOneWay = true)]       
        void SendMsg(string msg, int id);        // клиент ждёт ответа всегда, но иногда не требуется ждать ответ (IsOneWay = true )
        // сервер должен разослась это всем клиентам 
        // на серверной части описываем такой фукнции интерфейс, а на клиентской - реализация 
    }

    public interface IServiceChatCallback
    {
        [OperationContract(IsOneWay = true)]         // без этого сервер будет ожидатиь получился ли клиент сообщение 
        void MsgCallback(string msg);
    }


}
