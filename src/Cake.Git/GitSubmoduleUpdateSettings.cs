using LibGit2Sharp;

namespace Cake.Git
{
    /// <summary>
    /// Contains settings used by GitSubmoduleUpdate.
    /// </summary>
    public class GitSubmoduleUpdateSettings
    {
        /// <summary>
        /// True will initialize the local configuration file.
        /// </summary>
        public bool Init { get; set; }

        internal SubmoduleUpdateOptions ToSubmoduleUpdateOptions()
        {
            return new SubmoduleUpdateOptions
            {
                Init = this.Init
            };
        }
    }
}