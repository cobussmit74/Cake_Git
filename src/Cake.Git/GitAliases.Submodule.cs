using System;
using System.Linq;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using Cake.Git.Extensions;
using LogLevel = Cake.Core.Diagnostics.LogLevel;
using Verbosity = Cake.Core.Diagnostics.Verbosity;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
namespace Cake.Git
{
    // ReSharper disable once PublicMembersMustHaveComments
    public static partial class GitAliases
    {
        /// <summary>
        /// Update a specific submodule.
        /// </summary>
        /// <example>
        /// <code>
        ///     GitSubmoduleUpdate("c:/temp/cake", "icing-sugar", new GitSubmoduleUpdateSettings{ Init = true });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="repositoryDirectoryPath">Path to repository.</param>
        /// <param name="submoduleName">Name of the submodule.</param>
        /// <param name="updateSettings">The update settings.</param>
        /// <exception cref="ArgumentNullException"></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("SubmoduleUpdate")]
        public static void GitSubmoduleUpdate(
            this ICakeContext context,
            DirectoryPath repositoryDirectoryPath,
            string submoduleName,
            GitSubmoduleUpdateSettings updateSettings
            )
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (repositoryDirectoryPath == null)
            {
                throw new ArgumentNullException(nameof(repositoryDirectoryPath));
            }

            if (string.IsNullOrWhiteSpace(submoduleName))
            {
                throw new ArgumentNullException(nameof(submoduleName));
            }

            if (updateSettings == null)
            {
                throw new ArgumentNullException(nameof(updateSettings));
            }
            
            context.UseRepository(
                repositoryDirectoryPath,
                repository => repository.Submodules.Update(submoduleName, updateSettings.ToSubmoduleUpdateOptions())
            );
        }

        /// <summary>
        /// Update all submodules.
        /// </summary>
        /// <example>
        /// <code>
        ///     GitSubmoduleUpdate("c:/temp/cake", new GitSubmoduleUpdateSettings{ Init = true });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="repositoryDirectoryPath">Path to repository.</param>
        /// <param name="updateSettings">The update settings.</param>
        /// <exception cref="ArgumentNullException"></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("SubmoduleUpdate")]
        public static void GitSubmoduleUpdate(
            this ICakeContext context,
            DirectoryPath repositoryDirectoryPath,
            GitSubmoduleUpdateSettings updateSettings
        )
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (repositoryDirectoryPath == null)
            {
                throw new ArgumentNullException(nameof(repositoryDirectoryPath));
            }
            
            var submoduleNames = context.UseRepository(
                repositoryDirectoryPath,
                repository =>
                {
                    return repository
                        .Submodules
                        .Select(s => s.Name)
                        .ToList();
                });

            foreach (var name in submoduleNames)
            {
                GitSubmoduleUpdate(context, repositoryDirectoryPath, name, updateSettings);
            }
        }
    }
}
