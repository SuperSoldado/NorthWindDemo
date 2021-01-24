using Xunit;
using MyAppXUnitTest.Fixture;

namespace MyAppTest.DAOTest.SetOfTestsB
{
    [CollectionDefinition("Database collection")]
    public class DatabaseCollection2 : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    [Collection("Database collection")]
    public class DbTest2_DatabaseCollection
    {
        DatabaseFixture fixture;

        public DbTest2_DatabaseCollection(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void DbTest2_T1()
        {
            Assert.Equal(0, 0);
        }

        [Fact]
        public void DbTest2_T2()
        {
            Assert.Equal(0, 0);
        }
    }
}
