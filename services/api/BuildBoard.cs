namespace Chess
{
    public class BuildBoardApiPayload
    {
        public string Fen { get; set; }

        public BuildBoardApiPayload(string fen)
        {
            Fen = fen;
        }
    }
}
