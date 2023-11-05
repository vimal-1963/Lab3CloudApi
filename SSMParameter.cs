using Amazon.SimpleSystemsManagement;
using System.Runtime.CompilerServices;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Amazon;

namespace MVCApplication
{
    public static class SSMParameter
    {
       
        


        

        //get accessID parameter
        public static async Task getAccessId()
        {
            
            
            using(var ssmClient = new AmazonSimpleSystemsManagementClient(RegionEndpoint.CACentral1))
            {
                var accessIdRequest = new GetParameterRequest
                {
                    Name = _IAMaccessId
                };
                
                var response = await ssmClient.GetParameterAsync(accessIdRequest);
                if(response != null)
                {
                    Console.WriteLine(response.Parameter.Value.ToString());   

                    //return response.Parameter.Value.ToString();
                }
                else
                {
                    Console.WriteLine("Parameter not found");
                    //return "Parameter not found";
                }
                
            }   
        }
        //get password parameter
        public async Task<string> getAccessIdPassword()
        {
            using (var ssmClient = new AmazonSimpleSystemsManagementClient(RegionEndpoint.CACentral1))
            {
                var accessIdRequest = new GetParameterRequest
                {
                    Name = _IAMaccessParameter
                };

                var response = await ssmClient.GetParameterAsync(accessIdRequest);
                if (response != null)
                {
                    return response.Parameter.Value.ToString();
                }
                else
                {
                    return "Parameter not found";
                }

            }
        }




    }
}
