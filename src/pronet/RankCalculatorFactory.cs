namespace ProNet
{
    public class RankCalculatorFactory : IRankCalculatorFactory
    {
        public RankCalculator BuildRankCalculator()
        {
            return new RankCalculator();
        }
    }
}