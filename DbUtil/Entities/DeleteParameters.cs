using System;
namespace DbUtil.Entities
{
    public class DeleteParameters
    {
        /// <summary>
        /// The name of the table that data should be deleted form
        /// </summary>
        public string table { get; set; }
        /// <summary>
        /// The fields with the criteria for deleting from the table
        /// </summary>
        public string[] args { get; set; }
        /// <summary>
        /// The corresponding values for the fields 
        /// </summary>
        public object[] values { get; set; }
    }
}
