using Core.Interfaces;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Test.Mocks
{
    public class MockExternalVendorRepository
    {
        public static Mock<IExternalVendorRepository> GetMockExternalVendorRepository()
        {
            //var leaveTypes = new List<LeaveType>
            //    {
            //        new LeaveType
            //        {
            //            Id = 1,
            //            DefaultDays = 10,
            //            Name = "Test Vacation"
            //        },
            //        new LeaveType
            //        {
            //            Id = 2,
            //            DefaultDays = 15,
            //            Name = "Test Sick"
            //        },
            //        new LeaveType
            //        {
            //            Id = 3,
            //            DefaultDays = 15,
            //            Name = "Test Maternity"
            //        }
            //    };

            var mockRepo = new Mock<IExternalVendorRepository>();
            //{
            //StatusCode: 200, ReasonPhrase: 'OK', Version: 1.1, Content: System.Net.Http.HttpConnectionResponseContent, Headers:
            //                {
            ////                Date: Sun, 15 Dec 2024 01:06:50 GMT
            ////  Transfer - Encoding: chunked
            ////  Connection: keep - alive
            ////  Access - Control - Allow - Headers: *
            ////  Access - Control - Allow - Origin: *
            ////  X - Content - Type - Options: NOSNIFF
            ////  X - Frame - Options: SAMEORIGIN
            ////  CF - Cache - Status: DYNAMIC
            ////  Report - To: { "endpoints":[{ "url":"https:\/\/a.nel.cloudflare.com\/report\/v4?s=2ffexLM1mK41h6cJfQydyAPIF4kBRgA%2Fv%2FF6t4azUk2xOXMVfLCNAoG0MR5LQbuzMSnnUpPrlP1OxuYJUGGzTkepdnXUE1a4SCVemKrP%2FXA5QgqQDNZ7AhIZH23q7walqq%2FoPYEzypfc"}],"group":"cf-nel","max_age":604800}
            ////                NEL: { "success_fraction":0,"report_to":"cf-nel","max_age":604800}
            ////                Server: cloudflare
            ////  CF - RAY: 8f228888789a55bf - CBR
            ////  Alt - Svc: h3 = ":443"
            ////  Server - Timing: cfL4; desc = "?proto=TCP&rtt=14222&min_rtt=14060&rtt_var=5598&sent=5&recv=6&lost=0&retrans=0&sent_bytes=2852&recv_bytes=703&delivery_rate=275727&cwnd=252&unsent_bytes=0&cid=987e04fa3984ff55&ts=330&x=0"
            ////  Content - Type: application / json
            ////}, Trailing Headers:
            //{
            //                }

            // Arrange

            const string testContent = "test content";
            var mockMessageHandler = new Mock<HttpMessageHandler>();
            mockRepo
                .Setup<Task<HttpResponseMessage>>(r => r.GetExchangeRate())
                //.Setup<Task<HttpResponseMessage>>("GetData", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())

                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(testContent)
                });
            //var underTest = new SiteAnalyzer(new HttpClient(mockMessageHandler.Object));

            //mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);

            //mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
            //    .Returns((LeaveType leaveType) =>
            //    {
            //        leaveTypes.Add(leaveType);
            //        return Task.CompletedTask;
            //    });

            //mockRepo.Setup(r => r.IsLeaveTypeUnique(It.IsAny<string>()))
            //    .ReturnsAsync((string name) => {
            //        return !leaveTypes.Any(q => q.Name == name);
            //    });

            return mockRepo;
        }
    }

}
