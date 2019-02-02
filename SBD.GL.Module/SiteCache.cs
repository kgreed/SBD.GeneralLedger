using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBD.GL.Module
{
    /// <summary>
    /// Sample singleton object.
    /// </summary>
    public sealed class SiteCache
    {
        /// <summary>
        /// This is an expensive resource.
        /// We need to only store it in one place.
        /// </summary>
        // List<HistoryClass> _HistoryClasses = List<HistoryClass>()];

        /// <summary>
        /// Allocate ourselves.
        /// We have a private constructor, so no one else can.
        /// </summary>
        static readonly SiteCache _instance = new SiteCache();

        /// <summary>
        /// Access SiteStructure.Instance to get the singleton object.
        /// Then call methods on that instance.
        /// </summary>
        public static SiteCache Instance => _instance;

       
        public string LocalPath { get; set; }

        /// <summary>
        /// This is a private constructor, meaning no outsiders have access.
        /// </summary>
        private SiteCache()
        {
            // Initialize members here.

             
        }

        public DateTime EntryDate { get; set; }
        public string ConnectionString {
            get
            {
                var path = Path.Combine(Instance.LocalPath, HandyDefaults.APP_NAME);
                return $"Data Source={path}\\sbdgl.sqlite";
            }
        }
    }
}
