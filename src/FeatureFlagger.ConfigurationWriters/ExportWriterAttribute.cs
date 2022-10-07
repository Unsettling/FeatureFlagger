namespace FeatureFlagger.ConfigurationWriters
{
    using System;
    using System.Composition;

    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportWriterAttribute : ExportAttribute
    {
        public string Writer { get; set; }
    }
}
