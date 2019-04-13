namespace DbUp.Migration
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if base string contains keyword using case insensitive comparison
        /// </summary>
        public static bool ContainsKeyword(this string baseString, string keyword)
        {
            return (baseString != null) && (keyword == null || baseString.Trim().ToLowerInvariant().Contains(keyword.Trim().ToLowerInvariant()));
        }
    }
}
