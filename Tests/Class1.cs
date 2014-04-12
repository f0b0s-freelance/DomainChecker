using DomainChecker.Core;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ThumbrCheckerTests
    {
        [Test]
        public void NameCheck()
        {
            var checker = new ThumbrChecker();
            Assert.True(checker.Check("tasest234"));
        }

        [Test]
        public void BlogSpot()
        {
            var checker = new BlogspotChecker();
            Assert.False(checker.Check("sergeipopov"));
        }
    }
}
