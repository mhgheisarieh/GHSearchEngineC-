namespace GHSearchEngine
/**
* each founded document as a Result;
*/

{
    public class Result
    {

        /**
         * <param index : index of document> </param>
         * <param score : score of document> </param>
         */

        private int index;
        private int score;

        public Result(int index, int score)
        {
            this.index = index;
            this.score = score;
        }

        public void ChangeScore(int change)
        {
            this.score += change;
        }

        public int GetIndex()
        {
            return index;
        }

        public int GetScore()
        {
            return score;
        }
    }
}