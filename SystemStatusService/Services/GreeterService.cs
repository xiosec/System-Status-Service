using Core;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SystemStatusService
{
    public class GreeterService : Greeter.GreeterBase
    {

        private readonly ILogger<GreeterService> _logger;
        private SystemInformation SystemInfo;
        private readonly string Token;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
            Token = Environment.GetEnvironmentVariable("AUTHTOKEN");
            SystemInfo = new SystemInformation();
        }

        public override Task<response> login(login_request request, ServerCallContext context)
        {
            if (request.Token==Token)
            {
                return Task.FromResult(new response
                {
                    Status = true,
                    Message = Environment.UserName
                });
            }
            else
            {
                return Task.FromResult(new response
                {
                    Status = false,
                    Message = "Authentication was not successful"
                });
            }
        }

        public override Task<response> info(login_request request, ServerCallContext context)
        {
            if (request.Token == Token)
            {
                _logger.LogInformation("A request for info was sent");
                return Task.FromResult(new response
                {
                    Status = true,
                    Message = JsonConvert.SerializeObject(SystemInfo.Info())
                });
            }
            else
            {
                return Task.FromResult(new response
                {
                    Status = false,
                    Message = "Authentication was not successful"
                });
            }
        }

        public override Task<response> process(login_request request, ServerCallContext context)
        {
            if (request.Token == Token)
            {
                _logger.LogInformation("A request for process was sent");
                return Task.FromResult(new response
                {
                    Status = true,
                    Message = JsonConvert.SerializeObject(SystemInfo.SystemProcess())
                });
            }
            else
            {
                return Task.FromResult(new response
                {
                    Status = false,
                    Message ="Authentication was not successful"
                });
            }
        }

        public override Task<response> command(command_request request, ServerCallContext context)
        {
            if (request.Token == Token)
            {
                _logger.LogInformation($"A request for command was sent : {request.Cmd}");
                return Task.FromResult(new response
                {
                    Status = true,
                    Message = JsonConvert.SerializeObject(SystemInfo.Command(request.Cmd))
                });
            }
            else
            {
                return Task.FromResult(new response
                {
                    Status = false,
                    Message ="Authentication was not successful"
                });
            }
        }
    }
}
