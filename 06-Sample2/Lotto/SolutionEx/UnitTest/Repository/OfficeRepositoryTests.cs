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

[Collection(nameof(MyRepositoryTests))]
public sealed class OfficeRepositoryTests : RepositoryTest
{
    #region crt and overrides

    public OfficeRepositoryTests(RepositoryTestFixture testFixture) : base(testFixture)
    {
    }

    #endregion

    [Fact]
    public async Task ReadAllTest()
    {
        var uow = GetUnitOfWork();

        var all = await uow.OfficeRepository.GetAsync((l) => l.Name == "Bad Ischl");

        all.Should().HaveCount(1);

        var badIschl = all.Single();

        // Check Mapping!

        badIschl.Should().BeEquivalentTo(
            new
            {
                No      = "132",
                Address = "OOE"
            });
    }
}