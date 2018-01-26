using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.Util.Http;

namespace Tieptuyen.Components.Core.Api.Controllers
{
    /// <summary>
    /// The web API controller for access control functionality.
    /// </summary>
    public class AccessControlController : ApiController
    {
        private IAccessControlService accessControlService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessControlController"/> class.
        /// </summary>
        /// <param name="accessControlService">The access control service.</param>
        public AccessControlController(IAccessControlService accessControlService)
        {
            this.accessControlService = accessControlService;
        }      

        /// <summary>
        /// Gets the user settings.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The username of the user.</param>
        /// <returns>A <see cref="UserSettings"/> object.</returns>
        [HttpGet]
        public UserSettings GetUserSettings(string userName, string password)
        {
            return this.accessControlService.GetUserSettings(userName, password);
        }       

        /// Saves the user settings.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        /// <returns>The value of <paramref name="userSettings"/>.</returns>
        [HttpPost]
        public UserSettings SaveUserSettings(UserSettings userSettings)
        {
            this.accessControlService.UpdateUserSettings(userSettings, UserSettingsUpdateOptions.Basic);
            return userSettings;
        }

        /// <summary>
        /// Saves the site metrics items.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        /// <returns>The value of <paramref name="userSettings"/>.</returns>
        [HttpPost]
        public UserSettings SaveSiteMetricsItems(UserSettings userSettings)
        {
            this.accessControlService.UpdateUserSettings(userSettings, UserSettingsUpdateOptions.SiteMetricsItems);
            return userSettings;
        }

        /// <summary>
        /// Saves the mixing desk items.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        /// <returns>The value of <paramref name="userSettings"/>.</returns>
        [HttpPost]
        public UserSettings SaveMixingDeskItems(UserSettings userSettings)
        {
            this.accessControlService.UpdateUserSettings(userSettings, UserSettingsUpdateOptions.MixingDeskItems);
            return userSettings;
        }

        /// <summary>
        /// Gets all profiles.
        /// </summary>
        /// <returns>A collection of <see cref="Profile"/> objects.</returns>
        [HttpGet]
        public ICollection<Profile> GetAllProfiles()
        {
            return this.accessControlService.GetProfiles(this.GetCurrentUser());
        }

        /// <summary>
        /// Adds a new profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>The <see cref="Profile"/> object.</returns>
        [HttpPost]
        public Profile AddProfile(Profile profile)
        {
            this.accessControlService.Add(profile);
            return profile;
        }

        /// <summary>
        /// Updates the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns>The <see cref="Profile"/> object.</returns>
        [HttpPost]
        public Profile UpdateProfile(Profile profile)
        {
            this.accessControlService.Update(profile, ProfileUpdateOptions.UpdatePermissions);
            return profile;
        }

        /// <summary>
        /// Updates multiple profile
        /// </summary>
        /// <param name="request">Update request</param>
        [HttpPost]
        public void UpdateMultipleProfile(MultipleUpdateUserProfileRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request", "The user can not be null");
            }

            this.accessControlService.MultipleUpdate(request.ProfileIds, request.Profile);
        }

        /// <summary>
        /// Deletes the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        [HttpPost]
        public void DeleteProfile(Profile profile)
        {
            this.accessControlService.Delete(profile);
        }

        /// <summary>
        /// Determines whether the specified profile is empty (i.e. currently has no users assigned to it).
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns><c>true</c> if the profile is empty, otherwise <c>false</c>.</returns>
        [HttpGet]
        public bool IsProfileEmpty(int profileId)
        {
            return this.accessControlService.IsProfileEmpty(profileId);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>A collection of <see cref="User"/> objects.</returns>
        [HttpGet]
        public ICollection<User> GetAllUsers()
        {
            return this.accessControlService.GetUsers();
        }

        /// <summary>
        /// Gets a single user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The matching <see cref="User"/> object or <c>null</c> when no match found.</returns>
        [HttpGet]
        public User GetUser(int userId)
        {
            return this.accessControlService.GetUser(userId);
        }

        /// <summary>
        /// Gets a single user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The matching <see cref="User"/> object or <c>null</c> when no match found.</returns>
        [HttpGet]
        public User GetUser(string userName)
        {
            return this.accessControlService.GetUser(userName);
        }

        /// <summary>
        /// Adds a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The <see cref="User"/> object.</returns>
        [HttpPost]
        public User AddUser(User user)
        {
            this.accessControlService.Add(user);
            return user;
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The <see cref="User"/> object.</returns>
        [HttpPost]
        public User UpdateUser(User user)
        {
            this.accessControlService.Update(user);
            return user;
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="user">The user.</param>
        [HttpPost]
        public void DeleteUser(User user)
        {
            string currentUser = this.GetCurrentUser();
            this.accessControlService.Delete(user, currentUser);
        }

        /// <summary>
        /// Determines whether the specified username is already in use.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <returns><c>true</c> if the username is already in use, otherwise <c>false</c>.</returns>
        [HttpGet]
        public bool IsUserNameInUse(string userName)
        {
            return this.accessControlService.IsUserNameInUse(userName);
        }

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="passwordReset">The password reset.</param>
        [HttpPost]
        public void UserResetPassword(PasswordReset passwordReset)
        {
            string currentUser = this.GetCurrentUser();
            this.accessControlService.ResetPassword(passwordReset, currentUser, false);
        }

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="passwordReset">The password reset.</param>
        //// NOTE! This method needs to be role controlled!
        [HttpPost]
        public void PrivilegedResetPassword(PasswordReset passwordReset)
        {
            string currentUser = this.GetCurrentUser();
            this.accessControlService.ResetPassword(passwordReset, currentUser, true);
        }

        /// <summary>
        /// Gets all primary roles.
        /// </summary>
        /// <returns>A collection of <see cref="PrimaryRole"/> objects.</returns>
        [HttpGet]
        public IEnumerable<PrimaryRole> GetAllPrimaryRoles()
        {
            return this.accessControlService.GetAllPrimaryRoles();
        }

        /// <summary>
        /// The get primary role widget mapping.
        /// </summary>
        /// <param name="primaryRoleId">
        /// The primary role id.
        /// </param>
        /// <returns>
        /// The list primary role widget mapping.
        /// </returns>
        [HttpGet]
        public IEnumerable<PrimaryRoleWidgetMapping> GetPrimaryRoleWidgetMappings(int primaryRoleId)
        {
            return this.accessControlService.GetPrimaryRoleWidgetMappings(primaryRoleId);
        }

        /// <summary>
        /// Gets a empty multi user.
        /// </summary>        
        /// <returns> Empty multi user</returns>
        [HttpGet]
        public MultiUsers GetEmptyMultiUser()
        {
            return new MultiUsers();
        }

        /// <summary>
        /// Updates a user.
        /// </summary>
        /// <param name="users">The user.</param>        
        [HttpPost]
        public void UpdateUserMulti(MultiUsers users)
        {
            if (users == null)
            {
                throw new ArgumentNullException("users", "The user can not be null");
            }

            this.accessControlService.UpdateUserMulti(users);
        }

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="multiUser">User info</param>
        //// NOTE! This method needs to be role controlled!
        [HttpPost]
        public void ResetPasswordUsersMulti(MultiUsers multiUser)
        {
            string currentUser = this.GetCurrentUser();
            if (multiUser == null)
            {
                throw new ArgumentNullException("multiUser", "The user can not be null");
            }

            foreach (var user in multiUser.Users)
            {
                var passwordReset = new PasswordReset();
                passwordReset.UserName = user.UserName;
                passwordReset.Password = multiUser.Password;
                passwordReset.PasswordConfirmation = multiUser.PasswordConfirmation;
                this.accessControlService.ResetPassword(passwordReset, currentUser, true);
            }
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="multiUser">The user.</param>
        [HttpPost]
        public void DeleteUserMulti(MultiUsers multiUser)
        {
            string currentUser = this.GetCurrentUser();
            InvalidOperationException exception = null;
            if (multiUser == null)
            {
                throw new ArgumentNullException("multiUser", "The user can not be null");
            }

            foreach (var user in multiUser.Users)
            {
                try
                {
                    this.accessControlService.Delete(user, currentUser);
                }
                catch (InvalidOperationException ex)
                {
                    exception = ex;
                }
            }

            if (exception != null)
            {
                throw exception;
            }
        }        

        /// <summary>
        /// Gets filter types
        /// </summary>
        /// <returns>a collection</returns>
        [HttpGet]
        public ICollection<DropDownObject> GetUserFilterTypes()
        {
            var retval = new List<DropDownObject> 
            { 
                new DropDownObject() { Id = 1, Name = "User Profiles" },
                new DropDownObject() { Id = 2, Name = "Network" },
                new DropDownObject() { Id = 3, Name = "Area" },
                new DropDownObject() { Id = 4, Name = "Channel of Trade" },
                new DropDownObject() { Id = 5, Name = "Sites" },
            };
            return retval;
        }                  

        /// <summary>
        /// Gets members of user profile
        /// </summary>
        /// <param name="profileId">Profile id</param>
        /// <returns>Members of user profile</returns>
        [HttpGet]
        public MembersOfUserProfile GetMembersOfUserProfile(int profileId)
        {
            return this.accessControlService.GetMembersOfUserProfile(profileId);
        }

        /// <summary>
        /// Assign user to profile
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The integer value</returns>
        [HttpPost]
        public int AssignUserToProfile(AssignUserToProfileRequest request)
        {
            if (request != null)
                return this.accessControlService.AssignUserToProfile(request.UserID, request.ProfileID);
            else
                throw new ArgumentNullException("request", "The request cannot be null.");
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="id">
        /// The cost type.
        /// </param>
        [HttpPost]
        public void Delete(int id)
        {
            this.accessControlService.Delete(id);
        }

        /// <summary>
        /// Gets widget permissions by primary role id.
        /// </summary>
        /// <param name="primaryRoleId">The primary role id.</param>
        /// <returns>The collection of widget permission.</returns>
        [HttpGet]
        public Collection<WidgetPermission> GetWidgetPermissionByPrimaryRole(int primaryRoleId)
        {
            return this.accessControlService.GetWidgetPermissionByPrimaryRole(primaryRoleId, this.GetCurrentUser());
        }

        /// <summary>
        /// Gets the name of the windows user making the request
        /// </summary>
        /// <returns>The windows user details</returns>
        [HttpGet]
        public User VerifyWindowsUser()
        {
            string windowsUsername = this.User.Identity.Name;
            User windowsUser = new User();

            if (!string.IsNullOrEmpty(windowsUsername))
            {
                windowsUser.UserName = windowsUsername;
                windowsUser.ProfileId = this.accessControlService.VerifyWindowsUser(windowsUsername);
            }

            return windowsUser;
        }
    }
}