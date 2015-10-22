using Sharp.Knife.Format;
using Xunit;

namespace Sharp.Knife.UnitTests.Format
{
    public class BooleanFormatterTest
    {
        // TODO: Add tests for null and string.Empty

        [Fact]
        public void TrueFalseToYesNo()
        {
            Assert.Equal("Yes", string.Format(new BooleanFormatter("Yes", "No"), "{0}", true));
            Assert.Equal("No", string.Format(new BooleanFormatter("Yes", "No"), "{0}", false));
        }

        [Fact]
        public void TrueFalseToYN()
        {
            Assert.Equal("Y", string.Format(new BooleanFormatter("Y", "N"), "{0}", true));
            Assert.Equal("N", string.Format(new BooleanFormatter("Y", "N"), "{0}", false));
        }

        [Fact]
        public void TrueFalseToPassFail()
        {
            Assert.Equal("Pass", string.Format(new BooleanFormatter("Pass", "Fail"), "{0}", true));
            Assert.Equal("Fail", string.Format(new BooleanFormatter("Pass", "Fail"), "{0}", false));
        }

        [Fact]
        public void TrueFalseToSatisfactoryUnsatisfactory()
        {
            Assert.Equal("Satisfactory", string.Format(new BooleanFormatter("Satisfactory", "Unsatisfactory"), "{0}", true));
            Assert.Equal("Unsatisfactory", string.Format(new BooleanFormatter("Satisfactory", "Unsatisfactory"), "{0}", false));
        }

        [Fact]
        public void TrueFalseToFalseTrue() // oops reversed True/False values
        {
            Assert.Equal("False", string.Format(new BooleanFormatter("False", "True"), "{0}", true));
            Assert.Equal("True", string.Format(new BooleanFormatter("False", "True"), "{0}", false));
        }
    }
}