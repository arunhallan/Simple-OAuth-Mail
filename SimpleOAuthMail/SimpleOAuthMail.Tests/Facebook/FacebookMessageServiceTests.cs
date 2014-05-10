using System;
using System.IO;
using NUnit.Framework;
using Rhino.Mocks;
using SimpleOAuthMail.OAuthDataConnections;
using SimpleOAuthMail.OAuthDataConnections.Facebook;
using SimpleOAuthMail.OAuthDataConnections.Services;

namespace SimpleOAuthMail.Tests.Facebook
{
    [TestFixture]
    public class FacebookMessageServiceTests
    {
        [Test]
        public void Given_An_HttpService_Return_Valid_Messages()
        {
            IHttpRequestResponseService mockRequestResponseService = MockRepository.GenerateStub<IHttpRequestResponseService>();
            mockRequestResponseService.Stub(x => x.Get(string.Empty, null)).IgnoreArguments().Return(File.ReadAllText("JSONSample.txt"));

            IMessageService facebookMessageService = new FacebookMessageService(mockRequestResponseService);
            var messages = facebookMessageService.GetInboxMailMessages(DateTime.MaxValue);

            Assert.That(messages.Count == 2);
            Assert.That(messages[1].To[1] == "John T");
        }
    }
}
