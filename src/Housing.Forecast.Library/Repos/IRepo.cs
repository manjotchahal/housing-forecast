using System;
using System.Collections.Generic;
using System.Text;
using Housing.Forecast.Library.Models;

namespace Housing.Forecast.Library.Repos
{
    public interface IRepo<T>
    {
        /// <summary>
        /// Find all distinct locations where all rooms are located.
        /// </summary>
        /// <returns>
        /// The method will return a list of City names for where rooms for housing are located in.
        /// </returns>
        IEnumerable<string> GetLocations();

        /// <summary>
        /// Find all stored T within the database.
        /// </summary>
        /// <returns>
        /// The method should return a list of all type T objects within the database.
        /// </returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Find all stored type T objects from the database using the starting point.
        /// </summary>
        /// <param name="datetime">The starting point for the earliest created date for the objects.</param>
        /// <returns>
        /// The method should return a list of all type T objects within the database that were created on/after the provided date.
        /// </returns>
        IEnumerable<T> GetByDate(DateTime datetime);

        /// <summary>
        /// Find all stored type T objects from the database using the range of dates.
        /// </summary>
        /// <param name="Start">The starting point for the earliest created date for the objects.</param>
        /// <param name="End">The objects shouldn't be deleted before this date.</param>
        /// <returns>
        /// The method should return a list of all type T objects within the database that are within the range of dates and aren't deleted.
        /// </returns>
        IEnumerable<T> GetBetweenDates(DateTime Start, DateTime End);

        /// <summary>
        /// Find all stored type T objects from the database using the starting point.
        /// </summary>
        /// <param name="datetime">The starting point for the earliest created date for the objects.</param>
        /// <param name="location">The objects should be located at the specified location.</param>
        /// <returns>
        /// The method should return a list of all type T objects within the database that were created on/after the provided date
        /// and are located at the location provided.
        /// </returns>
        IEnumerable<T> GetByLocation(DateTime datetime, string location);

        /// <summary>
        /// Find all stored type T objects from the database using the range of dates.
        /// </summary>
        /// <param name="Start">The starting point for the earliest created date for the objects.</param>
        /// <param name="End">The objects shouldn't be deleted before this date.</param>
        /// <param name="location">The objects should be located at the specified location.</param>
        /// <returns>
        /// The method should return a list of all type T objects within the database that are within
        /// the range of dates and aren't deleted and are located at the location provided.
        /// </returns>
        IEnumerable<T> GetBetweenDatesAtLocation(DateTime Start, DateTime End, string location);
    }
}