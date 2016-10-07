
namespace SecurityConsultantCore.Scoring
{
    public class SubScore
    {
        public SubScore(string name, double score, int weight)
        {
            Name = name;
            Score = score;
            Weight = weight;
        }

        public string Name { get; }
        public double Score { get; }
        public int Weight { get; }
    }
}