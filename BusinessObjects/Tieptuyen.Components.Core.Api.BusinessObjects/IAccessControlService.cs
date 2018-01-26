using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// The service for handling operations relating to user access control.
    /// </summary>
    public interface IAccessControlService
    {
        /// <summary>
        /// Gets the user settings for the specified user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The username of the user.</param>
        /// <returns>A <see cref="UserSettings"/> object containing the user's settings.</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="userName"/> is <c>null</c> or empty.</exception>
        UserSettings GetUserSettings(string userName, string password);
       
        /// <summary>
        /// Updates the user settings.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="userSettings" /> is <c>null</c>.</exception>
        void UpdateUserSettings(UserSettings userSettings, UserSettingsUpdateOptions options);

        /// <summary>
        /// Gets all the user profiles.
        /// </summary>
        /// <param name="currentUser">The user currently logged into the API.</param>
        /// <returns>A collection of <see cref="Profile"/> objects.</returns>
        ICollection<Profile> GetProfiles(string currentUser);

        /// <summary>
        /// Adds a profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="profile"/> is <c>null</c>.</exception>
        void Add(Profile profile);

        /// <summary>
        /// Updates the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="profile"/> is <c>null</c>.</exception>
        void Update(Profile profile);

        /// <summary>
        /// Updates the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="profile"/> is <c>null</c>.</exception>
        void Update(Profile profile, ProfileUpdateOptions options);

        /// <summary>
        /// Update multiple profile
        /// </summary>
        /// <param name="profileIds">The list of profile id</param>
        /// <param name="profile">The profile</param>
        void MultipleUpdate(int[] profileIds, Profile profile);

        /// <summary>
        /// Deletes the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="profile"/> is <c>null</c>.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if the specified profile is not empty (i.e. still has users assigned to it).</exception>
        void Delete(Profile profile);

        /// <summary>
        /// Determines whether the specified profile is empty (i.e. currently has no users assigned to it).
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns><c>true</c> if the profile is empty, otherwise <c>false</c>.</returns>
        bool IsProfileEmpty(int profileId);

        /// <summary>
        /// Gets all the users.
        /// </summary>
        /// <returns>A collection of <see cref="User"/> objects.</returns>
        ICollection<User> GetUsers();

        /// <summary>
        /// Gets a single user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The matching <see cref="User"/> object, or <c>null</c> if not match is found.</returns>
        User GetUser(int userId);

        /// <summary>
        /// Gets a single user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The matching <see cref="User"/> object, or <c>null</c> if not match is found.</returns>
        User GetUser(string userName);

        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="user"/> is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if a user already exists with the same username </exception>
        void Add(User user);

        /// <summary>
        /// Updates the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="user"/> is <c>null</c>.</exception>
        void Update(User user);

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="userName">The username of the user who is performing the deletion.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="user"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="userName"/> is either <c>null</c> or empty.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown if the user tries to delete themselves (i.e. when the value of <paramref name="userName"/> matches the <c>UserName</c> property of <paramref name="user"/>).</exception>
        void Delete(User user, string userName);

        /// <summary>
        /// Determines whether the specified username is already in use.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <returns><c>true</c> if the username is already in use, otherwise <c>false</c>.</returns>
        bool IsUserNameInUse(string userName);

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="passwordReset">The password reset request.</param>
        /// <param name="userName">The username of the user who is resetting the password.</param>
        /// <param name="privileged">Whether reset is privileged - i.e. can be done for another user
        /// and does not need existing password</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="passwordReset"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="userName"/> is either <c>null</c> or empty; or the passwords do not match.</exception>
        void ResetPassword(PasswordReset passwordReset, string userName, bool privileged);

        /// <summary>
        /// Gets all the sites for primary role.
        /// </summary>
        /// <returns>A IEnumerable of <see cref="PrimaryRole"/> objects.</returns>
        IEnumerable<PrimaryRole> GetAllPrimaryRoles();

        /// <summary>
        /// The get primary role widget mapping.
        /// </summary>
        /// <param name="primaryRoleId">
        /// The id.
        /// </param>
        /// <returns>
        /// The list primary role widget mapping.
        /// </returns>
        IEnumerable<PrimaryRoleWidgetMapping> GetPrimaryRoleWidgetMappings(int primaryRoleId);

        /// <summary>
        /// Updates the specified user in the database.
        /// </summary>
        /// <param name="multiUsers">The user.</param>
        /// <returns>
        /// number of uses affected
        /// </returns>                        
        int UpdateUserMulti(MultiUsers multiUsers);     

        /// <summary>
        /// Save Filter Users
        /// </summary>
        /// <param name="id">request id</param>
        void Delete(int id);       

        /// <summary>
        /// Gets members of user profile
        /// </summary>
        /// <param name="profileId">The profile id</param>
        /// <returns>Members of user profile</returns>
        MembersOfUserProfile GetMembersOfUserProfile(int profileId);

        /// <summary>
        /// Assign user to profile
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="profileid">The profile id</param>
        /// <returns>The integer value</returns>
        int AssignUserToProfile(int userId, int profileid);

        /// <summary>
        /// Gets widget permissions primary role.
        /// </summary>
        /// <param name="primaryRoleId">The primary role id.</param>
        /// <param name="currentUser">The current user.</param>
        /// <returns>The collection.</returns>
        Collection<WidgetPermission> GetWidgetPermissionByPrimaryRole(int primaryRoleId, string currentUser);

        /// <summary>
        /// Verifies a windows user exists and creates a record in the User table if not.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <returns>The profile ID assigned to this user.</returns>
        int VerifyWindowsUser(string userName);
    }
}