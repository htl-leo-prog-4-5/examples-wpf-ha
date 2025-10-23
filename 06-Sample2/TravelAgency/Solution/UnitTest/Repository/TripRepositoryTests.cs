/*--------------------------------------------------------------
 *				HTBLA-Leonding / Class: 1xHIF
 *--------------------------------------------------------------
 *              Musterlösung
 *--------------------------------------------------------------
 * Description: Robot UnitTest
 *--------------------------------------------------------------
 */

using System;
using System.Threading.Tasks;

using Xunit;

namespace UnitTest.Repository;

using FluentAssertions;
using FluentAssertions.Extensions;

using System.Linq;

[Collection(nameof(MyRepositoryTests))]
public sealed class TripRepositoryTests : RepositoryTest
{
    #region crt and overrides

    public TripRepositoryTests(RepositoryTestFixture testFixture) : base(testFixture)
    {
    }

    #endregion

    [Fact]
    public async Task ReadAllTest()
    {
        var uow = GetUnitOfWork();

        var all = await uow.TripRepository.GetAsync();

        all.Should().HaveCount(4);

        var game1_6_2024 = all.OrderBy(g => g.ArrivalDateTime).First();

        // Check Mapping!

        game1_6_2024.Should().BeEquivalentTo(
            new
            {
                No1 = 3,
                No2 = 4,
                No3 = 13,
                No4 = 33,
                No5 = 37,
                No6 = 38,
                NoX = 7,
            });
    }
}