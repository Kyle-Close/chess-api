namespace Chess
{
  public class SquareTests
  {
    [Fact]
    public void GetSquareIndexWithChar_a6_Expect16()
    {
      var res = Square.GetSquareIndex('a', '6');
      Assert.True(res == 16);
    }
  }
}