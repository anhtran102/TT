using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using Tieptuyen.Api.Security.Crypto;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.DataAccess;
using Tieptuyen.Components.Core.Api.ObjectModel;
using Tieptuyen.Components.Core.Api.Util;
using Tieptuyen.Components.Globalization.Api;

namespace Tieptuyen.Components.Core.Api.Services
{
    /// <inheritdoc/>
    public sealed class AccessControlService : IAccessControlService
    {
        private IAccessControlRepository accessControlRepository;
        private ILanguageManager languageManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessControlService" /> class.
        /// </summary>
        /// <param name="accessControlRepository">The access control repository to use.</param>
        /// <param name="versioningService">The versioning service.</param>
        /// <param name="languageManager">The language manager.</param>
        public AccessControlService(IAccessControlRepository accessControlRepository, ILanguageManager languageManager)
        {
            this.accessControlRepository = accessControlRepository;
            this.languageManager = languageManager;
        }

        /// <inheritdoc/>
        public UserSettings GetUserSettings(string userName,string password)
        {
            if (!string.IsNullOrEmpty(userName) || !string.IsNullOrEmpty(password))
            {
                var passEncrypt = CryptoLibrary.Encrypt(password);
                UserSettings userSettings = this.accessControlRepository.SelectUserSettings(userName, passEncrypt);

                if (userSettings!= null && userSettings.Locked)
                {
                    throw new InvalidOperationException("This user is locked. Please contact Administrator"); 
                }
                return userSettings;
            }
            else
                throw new ArgumentException("The username and password cannot be null.", "userName");
        }    

        /// <inheritdoc/>
        public void UpdateUserSettings(UserSettings userSettings, UserSettingsUpdateOptions options)
        {
            if (userSettings != null)
            {
                this.accessControlRepository.UpdateUserSettings(userSettings, options);
            }
            else
                throw new ArgumentNullException("userSettings", "The user settings cannot be null.");
        }

        /// <inheritdoc/>
        public ICollection<Profile> GetProfiles(string currentUser)
        {
            ICollection<Profile> profiles = this.accessControlRepository.SelectProfiles();
            string lang = this.accessControlRepository.GetPreferredLanguage(currentUser);
            var translatedPermissions = new Dictionary<string, string>();
            foreach (Profile profile in profiles)
            {
                // Translate widget permissions according to current user's language
                foreach (WidgetPermission permission in profile.Permissions)
                {
                    if (!translatedPermissions.ContainsKey(permission.TitleDefaultText))
                    {
                        string translatedPermission;
                        if (string.IsNullOrEmpty(permission.TitleTxID))
                        {
                            translatedPermission = permission.TitleDefaultText;
                        }
                        else
                        {
                            translatedPermission = this.languageManager.GetText(permission.TitleTxID, lang);
                        }

                        if (translatedPermission.Length == 0)
                        {
                            translatedPermission = permission.TitleDefaultText;
                        }

                        translatedPermissions[permission.TitleDefaultText] = translatedPermission;
                    }

                    permission.TitleDefaultText = translatedPermissions[permission.TitleDefaultText];
                }
            }

            return profiles;
        }

        /// <inheritdoc/>
        public void Add(Profile profile)
        {
            if (profile != null)
            {
                if (!string.IsNullOrWhiteSpace(profile.Name))
                    this.accessControlRepository.Insert(profile);
                else
                    throw new InvalidOperationException("The profile must have a name specified.");
            }
            else
                throw new ArgumentNullException("profile", "The profile cannot be null.");
        }

        /// <inheritdoc/>
        public void Update(Profile profile)
        {
            this.Update(profile, ProfileUpdateOptions.None);
        }

        /// <inheritdoc/>
        public void Update(Profile profile, ProfileUpdateOptions options)
        {
            if (profile != null)
            {
                profile.Permissions.Each(x => x.ProfileId = profile.Id);
                this.accessControlRepository.Update(profile, options);
            }
            else
                throw new ArgumentNullException("profile", "The profile cannot be null.");
        }

        /// <inheritdoc/>
        public void Delete(Profile profile)
        {
            if (profile != null)
            {
                using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
                {
                    if (this.accessControlRepository.IsProfileEmpty(profile.Id, unitOfWork))
                    {
                        this.accessControlRepository.Delete(profile, unitOfWork);
                        unitOfWork.Commit();
                    }
                    else
                        throw new InvalidOperationException("The specified profile cannot be deleted as it still has users assigned to it.");
                }
            }
            else
                throw new ArgumentNullException("profile", "The profile cannot be null.");
        }

        /// <inheritdoc/>
        public bool IsProfileEmpty(int profileId)
        {
            return this.accessControlRepository.IsProfileEmpty(profileId);
        }

        /// <inheritdoc/>
        public ICollection<User> GetUsers()
        {
            return this.accessControlRepository.SelectUsers();
        }

        /// <inheritdoc/>
        public User GetUser(int userId)
        {
            return this.accessControlRepository.SelectUser(userId);
        }

        /// <inheritdoc/>
        public User GetUser(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
                return this.accessControlRepository.SelectUser(userName);
            else
                throw new ArgumentException("The usernane cannot be either null or empty.", "userName");
        }

        /// <inheritdoc/>
        public void Add(User user)
        {
            if (user != null)
            {
                if (user.PasswordConfirmation == user.Password)
                {
                    using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
                    {
                        if (!this.IsUserNameInUse(user.UserName, unitOfWork))
                        {
                            user.EncryptedPassword = CryptoLibrary.Encrypt(user.Password);
                            user.LastUpdated = user.Created = DateTime.UtcNow;
                            this.accessControlRepository.Insert(user, unitOfWork);
                            unitOfWork.Commit();
                            user.PasswordConfirmation = user.Password = null;
                        }
                        else
                            throw new InvalidOperationException("A user with the specified username already exists.");
                    }
                }
                else
                    throw new ArgumentException("The password and confirmation do not match.");
            }
            else
                throw new ArgumentNullException("user", "The user cannot be null.");
        }

        /// <inheritdoc/>
        public void Update(User user)
        {
            if (user != null)
            {
                this.accessControlRepository.Update(user);
                user.LastUpdated = DateTime.UtcNow;
            }
            else
                throw new ArgumentNullException("user", "The user cannot be null.");
        }

        /// <inheritdoc/>
        public void Delete(User user, string userName)
        {
            if (user != null && !string.IsNullOrEmpty(userName))
            {
                if (!user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase))
                    this.accessControlRepository.Delete(user);
                else
                    throw new InvalidOperationException("The current user may not delete their own account.");
            }
            else
            {
                if (user == null)
                    throw new ArgumentNullException("user", "The user cannot be null.");
                else
                    throw new ArgumentException("A username must be specified for the user performing the deletion.", "userName");
            }
        }

        /// <inheritdoc/>
        public bool IsUserNameInUse(string userName)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                return this.IsUserNameInUse(userName, unitOfWork);
            }
        }

        /// <inheritdoc/>
        public void ResetPassword(PasswordReset passwordReset, string userName, bool privileged)
        {
            if (!privileged)
            {
                this.CheckExistingPassword(passwordReset);
            }

            if (passwordReset != null && !string.IsNullOrEmpty(userName))
            {
                if (passwordReset.Password == passwordReset.PasswordConfirmation || (privileged && !passwordReset.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
                {
                    if (passwordReset.Password.Contains("|"))
                    {
                        // We cannot allow pipe in the password because it is used in the security token
                        // as a separator!
                        throw new ArgumentException("Widget_User_PasswordCannotUsePipe");
                    }

                    passwordReset.EncryptedPassword = CryptoLibrary.Encrypt(passwordReset.Password);
                    this.accessControlRepository.ResetPassword(passwordReset);
                }
                else
                {
                    if (!privileged && !passwordReset.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)) 
                        throw new ArgumentException("Insufficient privilege to change another user's password.");
                    else
                        throw new ArgumentException("Widget_User_PasswordNoMatch");
                }
            }
            else
            {
                if (passwordReset == null)
                    throw new ArgumentNullException("passwordReset", "The password reset request cannot be null.");
                else
                    throw new ArgumentException("A username must be specified for the user resetting the password.", "userName");
            }
        }             

        /// <inheritdoc/>
        public IEnumerable<PrimaryRoleWidgetMapping> GetPrimaryRoleWidgetMappings(int primaryRoleId)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                return this.accessControlRepository.GetPrimaryRoleWidgetMappings(primaryRoleId, unitOfWork);
            }
        }

        /// <inheritdoc/>
        public IEnumerable<PrimaryRole> GetAllPrimaryRoles()
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                return this.accessControlRepository.GetAllRrimaryRoles(unitOfWork);
            }
        }

        /// <inheritdoc/>                  
        public int UpdateUserMulti(MultiUsers multiUsers)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                var result = this.accessControlRepository.UpdateUserMulti(multiUsers, unitOfWork);
                unitOfWork.Commit();
                return result;
            }
        }

        /// <inheritdoc/>
        public void MultipleUpdate(int[] profileIds, Profile profile)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                this.accessControlRepository.MultipleUpdate(profileIds, profile, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public ICollection<DropDownObject> GetUserFilterValues(int filterTypeId)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                var retVal = this.accessControlRepository.GetUserFilterValues(filterTypeId, unitOfWork);
                unitOfWork.Commit();
                return retVal;
            }
        }         

        /// <inheritdoc/>
        public void Delete(int id)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                this.accessControlRepository.Delete(id, unitOfWork);
                unitOfWork.Commit();
            }
        }                     

        /// <inheritdoc/>
        public MembersOfUserProfile GetMembersOfUserProfile(int profileId)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                return this.accessControlRepository.GetMembersOfUserProfile(profileId, unitOfWork);
            }
        }

        /// <inheritdoc/>
        public int AssignUserToProfile(int userId, int profileid)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                var result = this.accessControlRepository.AssignUserToProfile(userId, profileid, unitOfWork);
                unitOfWork.Commit();
                return result;
            }
        }

        /// <inheritdoc/>
        public Collection<WidgetPermission> GetWidgetPermissionByPrimaryRole(int primaryRoleId, string currentUser)
        {
            string lang = this.accessControlRepository.GetPreferredLanguage(currentUser);
            var translatedPermissions = new Dictionary<string, string>();
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                var result = this.accessControlRepository.GetWidgetPermissionByPrimaryRole(primaryRoleId, unitOfWork);
                unitOfWork.Commit();
                foreach (WidgetPermission permission in result)
                {
                    if (!translatedPermissions.ContainsKey(permission.TitleDefaultText))
                    {
                        string translatedPermission;
                        if (string.IsNullOrEmpty(permission.TitleTxID))
                        {
                            translatedPermission = permission.TitleDefaultText;
                        }
                        else
                        {
                            translatedPermission = this.languageManager.GetText(permission.TitleTxID, lang);
                        }

                        if (translatedPermission.Length == 0)
                        {
                            translatedPermission = permission.TitleDefaultText;
                        }

                        translatedPermissions[permission.TitleDefaultText] = translatedPermission;
                    }

                    permission.TitleDefaultText = translatedPermissions[permission.TitleDefaultText];                   
                }

                return result;
            }
        }

        /// <inheritdoc/>
        public int VerifyWindowsUser(string userName)
        {
            using (IUnitOfWork unitOfWork = this.accessControlRepository.BeginWork())
            {
                return this.accessControlRepository.VerifyWindowsUser(userName, unitOfWork);
            }
        }

        private static bool VersionMatch(string firstVersionToMatch, string secondVersionToMatch, int matchDepth)
        {
            var firstVersion = firstVersionToMatch.Split('.');
            var secondVersion = secondVersionToMatch.Split('.');
            var isMatch = true;

            for (var i = 0; i < matchDepth; i++)
            {
                short firstVersionNumber;
                short secondVersionNumber;

                if ((!short.TryParse(firstVersion[i], out firstVersionNumber)) || (!short.TryParse(secondVersion[i], out secondVersionNumber)))
                {
                    isMatch = false;
                    break;
                }

                if (firstVersionNumber != secondVersionNumber)
                {
                    isMatch = false;
                    break;
                }
            }

            return isMatch;
        }

        private void CheckExistingPassword(PasswordReset passwordReset)
        {
            if (passwordReset == null)
                throw new ArgumentNullException("passwordReset", "The password reset request cannot be null.");

            string storedPassword = this.accessControlRepository.GetPassword(passwordReset.UserName);

            if (storedPassword.StartsWith("!ENC!"))
            {
                storedPassword = storedPassword.Remove(0, 5);
            }
            else
            {
                storedPassword = CryptoLibrary.Decrypt(storedPassword);
            }

            if (passwordReset.ExistingPassword != storedPassword)
            {
                throw new ArgumentException("Widget_User_ExistingPasswordIncorrect");
            }
        }

        private bool IsUserNameInUse(string userName, IUnitOfWork unitOfWork)
        {
            if (!string.IsNullOrEmpty(userName))
            {
                User user = this.accessControlRepository.SelectUser(userName, unitOfWork);
                return user != null;
            }
            else
                return false;
        }
    }
}
