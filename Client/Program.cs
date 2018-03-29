//using ServiceRecettes;
using Shared;
using ServiceRecettes;
using System;
//using System.Collections.Generic;
//using System.Linq;
using System.ServiceModel;
//using System.Text;
//using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceRecettesReference.IService serviceProxy = new ChannelFactory<ServiceRecettesReference.IService>("BasicHttpBinding_IService").CreateChannel();
            
            Console.WriteLine(serviceProxy.GetData(1));
            //ServiceRecettesReference.ServiceClient proxy = new ServiceRecettesReference.ServiceClient();
            //String s = proxy.GetData(0);
            
            CompositeType composite = new CompositeType
            {
                BoolValue = true,
                StringValue = "<rien>"
            };

            //Shared.CompositeType backCompo = serviceProxy.GetDataUsingDataContract(composite);
            
            //Console.WriteLine(backCompo.StringValue);
            Console.ReadLine();
        }
    }
}
