using System.ServiceModel;


namespace wcfChat
{
    internal class ServerUser
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
