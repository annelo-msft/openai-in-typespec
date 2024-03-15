// <auto-generated/>

#nullable disable

using System;
using System.ClientModel;
using NUnit.Framework;
using OpenAI;

namespace OpenAI.Tests
{
    public partial class ThreadsTests
    {
        [Test]
        public void SmokeTest()
        {
            KeyCredential credential = new KeyCredential(Environment.GetEnvironmentVariable("OpenAIClient_KEY"));
            Threads client = new OpenAIClient(credential).GetThreadsClient();
            Assert.IsNotNull(client);
        }
    }
}
