using System;

namespace SBD.GL.Module
{
    public sealed class InstanceWideMemvars
    {
        private InstanceWideMemvars()
        {
        }

        private static InstanceWideMemvars instance = null;

        public static InstanceWideMemvars Instance => instance ?? (instance = new InstanceWideMemvars());
        public double ValueOne { get; set; }
        public DateTime EntryDate { get; set; }
    }
}