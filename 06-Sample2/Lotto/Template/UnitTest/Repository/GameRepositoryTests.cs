/*--------------------------------------------------------------
 *				HTBLA-Leonding / Class: 1xHIF
 *--------------------------------------------------------------
 *              Musterlösung
 *--------------------------------------------------------------
 * Description: Robot UnitTest
 *--------------------------------------------------------------
 */

using System;
using System.Linq;
using System.Threading.Tasks;

using Core.Contracts;
using Core.Entities;

using FluentAssertions;

using Persistence;

using Xunit;

namespace UnitTest.Repository;

using FluentAssertions.Extensions;

[Collection(nameof(MyRepositoryTests))]
public sealed class GameRepositoryTests : RepositoryTest
{
    #region crt and overrides

    public GameRepositoryTests(RepositoryTestFixture testFixture) : base(testFixture)
    {
    }

    #endregion

    [Fact]
    public async Task ReadAllTest()
    {
        var uow = GetUnitOfWork();

        var all = await uow.GameRepository.GetAsync((l) => l.DateFrom > DateOnly.FromDateTime(31.December(2023)));

        all.Should().HaveCount(4);

        var game1_6_2024 = all.OrderBy(g => g.DateFrom).First();

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