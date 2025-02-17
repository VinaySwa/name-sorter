namespace NameSort.Domain
{
    /// <summary>
    /// Represents a full name
    /// </summary>
    public class FullName
    {
        /// <summary>
        /// Given name - Max allowed 3 given names separated by space
        /// </summary>
        public string GivenName { get; }

        /// <summary>
        /// Surname
        /// </summary>
        public string SurName { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="givenName"></param>
        /// <param name="surName"></param>
        public FullName(string givenName, string surName)
        {
            GivenName = givenName;
            SurName = surName;
        }

        /// <summary>
        /// Returns the full name - starting with given name followed by surname
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{GivenName} {SurName}";
        }
    }
}
