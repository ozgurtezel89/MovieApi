using System.Collections.Generic;

namespace Movie.CSVHandler
{
    public interface ICSVService
    {
        /// <summary>
        /// Reads from a CSV file at the give location
        /// </summary>
        /// <typeparam name="T">Object Type to map the CSV file reader results</typeparam>
        /// <param name="fileName">Location of the CSV file</param>
        /// <returns>Data set of given object Type thats read and mapped from given CSV file</returns>
        IEnumerable<T> ReadFromCSV<T>(string fileName);
    }
}
