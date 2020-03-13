using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using thegame.Models;
using thegame.Services;

namespace thegame.should
{
    public class GetCellAtCoords_Should
    {
        [SetUp]
        public void Setup()
        {
            _cells = TestData.AGameDto(new Vec(1, 1));
        } 

        private GameDto _cells;

        [Test]
        public void GetCellAtCoords_ReturnNull_WhenCordsIsOutOfRange()
        {
            var cell = _cells.GetCellAtCoords(500, 500);
            cell.Should().BeNull();
        }

        [Test]
        public void GetCellAtCoords_ReturnCell_WhenCordsIsCorrect()
        {
            var expectedCell = new CellDto("1", new Vec(0, 0), "color1", "", 0);

            _cells.Cells[0] = expectedCell;

            var cell = _cells.GetCellAtCoords(new Vec(0, 0));

            cell.Should().Be(expectedCell);
        }


    }
}