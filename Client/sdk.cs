using Grpc.Net.Client;
using Newtonsoft.Json;
using System.Collections.Generic;
using SystemStatusService;
namespace Client
{
    class sdk
    {
        private string Token { get; set; }
        private string Address { get; set; }
        private GrpcChannel Channel;
        private Greeter.GreeterClient Client;
        public sdk(string Address, string Token)
        {
            this.Address = Address;
            this.Token = Token;
            this.Channel = GrpcChannel.ForAddress(Address);
            this.Client = new Greeter.GreeterClient(this.Channel);
        }

        public (string message,bool status) login()
        {
            var response = this.Client.login(new login_request
            {
                Token = this.Token
            });
            return (response.Message, response.Status);
        } 

        public (Dictionary<string, string> value,bool status) Info()
        {
            var response = this.Client.info(new login_request
            {
                Token = this.Token
            });
            if (response.Status)
            {
                return (JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Message),true);
            }
            return (null,false);
        }

        public (List<Dictionary<int, string>> value,bool status) Process()
        {
            var response = this.Client.process(new login_request
            {
                Token = this.Token
            });
            if (response.Status)
            {
                return (JsonConvert.DeserializeObject<List<Dictionary<int, string>>>(response.Message),true);
            }
            return (null,false);
        }

        public (Dictionary<string, string> value,bool status) Command(string cmd)
        {
            var response = this.Client.command(new command_request
            {
                Token = this.Token,
                Cmd = cmd
            });
            if (response.Status)
            {
                return (JsonConvert.DeserializeObject<Dictionary<string, string>>(response.Message),true);
            }
            return (null,false);
        }
    }
}
