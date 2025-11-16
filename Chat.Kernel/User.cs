using System.ServiceModel;

namespace Chat.Kernel
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationContext OperationContext { get; set; }
    }
}
