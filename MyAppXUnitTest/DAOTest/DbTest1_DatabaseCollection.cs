using Xunit;
using MyAppXUnitTest.Fixture;

namespace MyAppTest.DAOTest.SetOfTestsA
{
    public class MyDatabaseTestsX : IClassFixture<DatabaseFixture>
    {
        DatabaseFixture fixture;

        public MyDatabaseTestsX(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        // ... write tests, using fixture.Db to get access to the SQL Server ...
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }

    [Collection("Database collection")]
    public class ResetDatabase_DatabaseCollection
    {
        DatabaseFixture fixture;
        public ResetDatabase_DatabaseCollection(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void ResetDB()
        {
            Assert.Equal(0, 0);
        }

    }

    [Collection("Database collection")]
    public class DbTest1_DatabaseCollection
    {
        DatabaseFixture fixture;

        public DbTest1_DatabaseCollection(DatabaseFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void DbTest1_T1()
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
