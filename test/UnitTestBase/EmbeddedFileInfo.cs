namespace UnitTestBase
{
    using System;
    using System.IO;
    using Microsoft.Extensions.FileProviders;

    public class EmbeddedFileInfo : IFileInfo
    {
        #region Fields

        private readonly Stream fileStream;

        #endregion Fields

        #region Constructor

        public EmbeddedFileInfo(string name, Stream fileStream)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            this.fileStream = fileStream;

            Exists = true;
            IsDirectory = false;
            Length = fileStream.Length;
            Name = name;
            PhysicalPath = name;
            LastModified = DateTimeOffset.Now;
        }

        #endregion Constructor

        #region Properties

        public bool Exists { get; }
        public bool IsDirectory { get; }
        public long Length { get; }
        public string Name { get; }
        public string PhysicalPath { get; }
        public DateTimeOffset LastModified { get; }

        #endregion Properties

        #region Methods

        public Stream CreateReadStream()
        {
            return this.fileStream;
        }

        #endregion Methods
    }
}
