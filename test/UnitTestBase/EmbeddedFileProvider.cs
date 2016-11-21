namespace UnitTestBase
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.Primitives;
    using Microsoft.Extensions.FileProviders;

    public class EmbeddedFileProvider : IFileProvider
    {
        #region Fields

        private readonly Assembly assembly;

        #endregion Fields

        #region Constructor

        public EmbeddedFileProvider(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            this.assembly = assembly;
        }

        #endregion Constructor

        #region Methods

        public IFileInfo GetFileInfo(string subpath)
        {
            string fullFileName = $"{this.assembly.GetName().Name}.{subpath}";

            bool isFileEmbedded = this.assembly.GetManifestResourceNames().Contains(fullFileName);

            return isFileEmbedded
                ? new EmbeddedFileInfo(subpath, this.assembly.GetManifestResourceStream(fullFileName))
                : (IFileInfo)new NotFoundFileInfo(subpath);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
