using System;
using System.ServiceModel;
using Sharp.Knife.Sockets;
using Moq;
using Xunit;

namespace Sharp.Knife.UnitTests.Sockets
{
    public class CommunicationObjectExtensionsTest
    {
        private readonly Mock<IFakeSocket> sut; // system under test

        public CommunicationObjectExtensionsTest()
        {
            sut = new Mock<IFakeSocket>();
            sut.Setup(x => x.GetData()).Returns(1);
        }

        [Fact]
        public void ReturnTest()
        {
            Assert.Equal(1, sut.Object.Execute(client => client.GetData()));
            sut.Verify(mock => mock.Abort(), Times.Never);
            sut.Verify(mock => mock.Close(), Times.Once);
        }

        [Fact]
        public void CommunicationExceptionTest()
        {
            sut.SetupGet(c => c.State).Returns(CommunicationState.Faulted);
            sut.Setup(x => x.GetData()).Throws(new CommunicationException());
            Assert.Throws<CommunicationException>(() => sut.Object.Execute(client => client.GetData()));
            sut.Verify(mock => mock.Abort(), Times.AtLeastOnce);
        }

        [Fact]
        public void TimeoutExceptionTest()
        {
            sut.SetupGet(c => c.State).Returns(CommunicationState.Faulted);
            sut.Setup(x => x.GetData()).Throws(new TimeoutException());
            Assert.Throws<TimeoutException>(() => sut.Object.Execute(client => client.GetData()));
            sut.Verify(mock => mock.Abort(), Times.AtLeastOnce);
        }

        [Fact]
        public void ExceptionTest()
        {
            sut.SetupGet(c => c.State).Returns(CommunicationState.Faulted);
            sut.Setup(x => x.GetData()).Throws(new Exception());
            Assert.Throws<Exception>(() => sut.Object.Execute(client => client.GetData()));
            sut.Verify(mock => mock.Abort(), Times.AtLeastOnce);
        }

        [Fact]
        public void FaultedInFinallyTest()
        {
            sut.SetupGet(c => c.State).Returns(CommunicationState.Faulted);
            sut.Object.Execute(client => client.GetData());
            sut.Verify(mock => mock.Abort(), Times.Once);
        }

        [Fact]
        public void FinallyTest()
        {
            sut.Object.Execute(client => client.GetData());
            sut.Verify(mock => mock.Close(), Times.Once);
        }
    }

    public interface IFakeSocket : ICommunicationObject
    {
        int GetData();
    }
}