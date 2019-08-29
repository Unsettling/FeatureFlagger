namespace FeatureFlagger.ConfigurationReaders
{
    using System;
    using System.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportReaderAttribute : ExportAttribute
    {
        public string Reader { get; set; }
    }
}
