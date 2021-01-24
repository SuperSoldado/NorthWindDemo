using MyAppXUnitTest.Fixture;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MyAppXUnitTest.DAOTest
{
    /// <summary>
    /// Reset DB. Can't run with other tests DBTest2 and DBTest1.
    /// </summary>
    public class DBTest3_NoFixture : DatabaseFixture
    {
        //[Fact]
        public void DbTest2_T1()
        {
            Assert.Equal(0, 0);
        }
    }
}
