namespace CommonCode.Utility
{
    public sealed class SequenceService
    {
        /// <summary>
        ///  Accept one string input of any number of integers separated by a single whitespace 
        ///  and output the longest increasing subsequence (increased by any number) present in that sequence. 
        ///  If more than 1 sequence exists with the longest length, output the earliest one.
        /// </summary>
        /// <param name="sequenceString">A string of integer values, separated by spaces</param>
        /// <returns>The longest sequence, as a space separated string</returns>
        public string FindLongestIncreasingSequence(string sequenceString)
        {
            if (string.IsNullOrWhiteSpace(sequenceString))
                return string.Empty;

            // We'll take it on faith that the values parse correctly, and expect an exception if not.
            int[] sequence = sequenceString
                                .Trim()
                                .Split(' ')
                                .Select(s => int.Parse(s))
                                .ToArray();
            var longestSequence = new SequenceInfo(-1, -1);
            var currentSequence = new SequenceInfo(0, 1);
            int lastValue = sequence[0];

            for (int seqIndex = 1; seqIndex < sequence.Length; seqIndex++)
            {
                int currentValue = sequence[seqIndex];
                if (currentValue < lastValue)
                {
                    if (currentSequence.Length > longestSequence.Length)
                    {
                        longestSequence = currentSequence;
                    }
                    currentSequence = new SequenceInfo(seqIndex, 1);
                }
                else
                    currentSequence.Length++;
                lastValue = currentValue;
            }
            if (currentSequence.Length > longestSequence.Length)
                longestSequence = currentSequence;
            return string.Join(' ', sequence.Skip(longestSequence.StartPosition)
                                            .Take(longestSequence.Length));
        }

        // A private supporting class containing all we nned to know about a sequence
        private class SequenceInfo
        {
            public SequenceInfo(int startPosition, int length)
            {
                StartPosition = startPosition;
                Length = length;
            }

            public int StartPosition { get; set; }
            public  int Length { get; set; }
        }
    }
}
