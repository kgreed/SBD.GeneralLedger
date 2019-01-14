using System;

namespace SBD.GL.Module
{
    public sealed class InstanceWideMemvars
    {
        private InstanceWideMemvars()
        {
        }

        private static InstanceWideMemvars _instance = null;

        public static InstanceWideMemvars Instance => _instance ?? (_instance = new InstanceWideMemvars());
        public DateTime EntryDate { get; set; }
    }
}