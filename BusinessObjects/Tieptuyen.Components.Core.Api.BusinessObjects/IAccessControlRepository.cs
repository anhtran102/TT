using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tieptuyen.Components.Core.Api.DataAccess;

namespace Tieptuyen.Components.Core.Api.BusinessObjects
{
    /// <summary>
    /// The repository for handling data-access operations relating to user access control.
    /// </summary>
    public interface IAccessControlRepository : IRepository
    {
        /// <summary>
        /// Selects the settings for a user from the database.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The username of the user.</param>
        /// <returns>A <see cref="UserSettings"/> object representing the user's settings.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="userName"/> is <c>null</c> or empty.</exception>
        UserSettings SelectUserSettings(string userName, string password);

        /// <summary>
        /// Selects the settings for a user from the database.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="password">The username of the user.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>A <see cref="UserSettings" /> object representing the user's settings.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="userName"/> is <c>null</c> or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        UserSettings SelectUserSettings(string userName, string password, IUnitOfWork unitOfWork);

        /// <summary>
        /// Updates the user settings in the database.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        /// <param name="options">The options.</param>
        /// <exception cref="ArgumentNullException">Thrown if either <paramref name="userSettings" /> or <paramref name="unitOfWork" /> is <c>null</c>.</exception>
        void UpdateUserSettings(UserSettings userSettings, UserSettingsUpdateOptions options);

        /// <summary>
        /// Updates the user settings in the database.
        /// </summary>
        /// <param name="userSettings">The user settings.</param>
        /// <param name="options">The options.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="ArgumentNullException">Thrown if either <paramref name="userSettings" /> or <paramref name="unitOfWork" /> is <c>null</c>.</exception>
        void UpdateUserSettings(UserSettings userSettings, UserSettingsUpdateOptions options, IUnitOfWork unitOfWork);

        /// <summary>
        /// Delete the user search save in the database
        /// </summary>
        /// <param name="id">the user restriction filter id</param>
        /// <param name="unitOfWork">the unit of work</param>
        void Delete(int id, IUnitOfWork unitOfWork);       

        /// <summary>
        /// Selects all the profiles from the database.
        /// </summary>
        /// <returns>A collection of <see cref="Profile"/> objects.</returns>
        ICollection<Profile> SelectProfiles();

        /// <summary>
        /// Selects all the profiles from the database.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>A collection of <see cref="Profile"/> objects.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        ICollection<Profile> SelectProfiles(IUnitOfWork unitOfWork);

        /// <summary>
        /// Inserts the specified profile into database.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="profile"/> is <c>null</c>.</exception>
        void Insert(Profile profile);

        /// <summary>
        /// Inserts the specified profile into database.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="profile"/> or <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        void Insert(Profile profile, IUnitOfWork unitOfWork);

        /// <summary>
        /// Updates the specified profile in the database.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="options">The options to use when updating the profile.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="profile"/> is <c>null</c>.</exception>
        void Update(Profile profile, ProfileUpdateOptions options);

        /// <summary>
        /// Updates the specified profile in the database.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="options">The options to use when updating the profile.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="profile"/> or <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        void Update(Profile profile, ProfileUpdateOptions options, IUnitOfWork unitOfWork);

        /// <summary>
        /// Updates multiple profile in the database
        /// </summary>
        /// <param name="profileIds">list of profile id</param>
        /// <param name="profile">The profile</param>
        /// <param name="unitOfWork">The unit of work</param>
        void MultipleUpdate(int[] profileIds, Profile profile, IUnitOfWork unitOfWork);

        /// <summary>
        /// Deletes the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="profile"/> is <c>null</c></exception>
        void Delete(Profile profile);

        /// <summary>
        /// Deletes the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="profile"/> or <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        void Delete(Profile profile, IUnitOfWork unitOfWork);

        /// <summary>
        /// Determines whether the specified profile is empty (i.e. currently has no users assigned to it).
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <returns><c>true</c> if the profile is empty, otherwise <c>false</c>.</returns>
        bool IsProfileEmpty(int profileId);

        /// <summary>
        /// Determines whether the specified profile is empty (i.e. currently has no users assigned to it).
        /// </summary>
        /// <param name="profileId">The profile ID.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns><c>true</c> if the profile is empty, otherwise <c>false</c>.</returns>
        bool IsProfileEmpty(int profileId, IUnitOfWork unitOfWork);

        /// <summary>
        /// Selects all users.
        /// </summary>
        /// <returns>A collection of <see cref="User"/> objects.</returns>
        ICollection<User> SelectUsers();

        /// <summary>
        /// Selects all users.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>A collection of <see cref="User"/> objects.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        ICollection<User> SelectUsers(IUnitOfWork unitOfWork);

        /// <summary>
        /// Selects a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The matching <see cref="User"/> object, or <c>null</c> if no match is found.</returns>
        User SelectUser(int userId);

        /// <summary>
        /// Selects a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>The matching <see cref="User"/> object, or <c>null</c> if no match is found.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        User SelectUser(int userId, IUnitOfWork unitOfWork);

        /// <summary>
        /// Selects a user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <returns>The matching <see cref="User"/> object, or <c>null</c> if no match is found.</returns>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="userName"/> is either <c>null</c> or empty.</exception>
        User SelectUser(string userName);

        /// <summary>
        /// Selects a user.
        /// </summary>
        /// <param name="userName">The username of the user.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>The matching <see cref="User"/> object, or <c>null</c> if no match is found.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        /// <exception cref="System.ArgumentException">Thrown if <paramref name="userName"/> is either <c>null</c> or empty.</exception>
        User SelectUser(string userName, IUnitOfWork unitOfWork);

        /// <summary>
        /// Inserts the specified user into the database.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="user"/> is <c>null</c>.</exception>
        void Insert(User user);

        /// <summary>
        /// Inserts the specified user into the database.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="user"/> or <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        void Insert(User user, IUnitOfWork unitOfWork);

        /// <summary>
        /// Updates the specified user in the database.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="user"/> is <c>null</c>.</exception>
        void Update(User user);

        /// <summary>
        /// Updates the specified user in the database.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="user"/> or <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        void Update(User user, IUnitOfWork unitOfWork);

        /// <summary>
        /// Deletes the specified user from the database.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="user"/> is <c>null</c>.</exception>
        void Delete(User user);

        /// <summary>
        /// Deletes the specified user from the database.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="user"/> or <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        void Delete(User user, IUnitOfWork unitOfWork);

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="passwordReset">The password reset request.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if <paramref name="passwordReset"/> is <c>null</c>.</exception>
        void ResetPassword(PasswordReset passwordReset);

        /// <summary>
        /// Resets a user's password.
        /// </summary>
        /// <param name="passwordReset">The password reset request.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if either <paramref name="passwordReset"/> or <paramref name="unitOfWork"/> is <c>null</c>.</exception>
        void ResetPassword(PasswordReset passwordReset, IUnitOfWork unitOfWork);

        /// <summary>
        /// Gets the password string for the user.  
        /// </summary>
        /// <param name="userName">The user name of the user to get the password for.</param>
        /// <returns>The password string (may be encrypted). Unencrypted passwords are prefixed !ENC!</returns>
        string GetPassword(string userName);

        /// <summary>
        /// Get the preferred language stored for the user that has the given user name.
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns>The preferred language</returns>
        string GetPreferredLanguage(string userName);        

        /// <summary>
        /// Gets all the sites for primary role.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>A IEnumerable of <see cref="PrimaryRole"/> objects.</returns>
        IEnumerable<PrimaryRole> GetAllRrimaryRoles(IUnitOfWork unitOfWork);

        /// <summary>
        /// The get primary role widget mapping.
        /// </summary>
        /// <param name="primaryRoleId">
        /// The id.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work.
        /// </param>
        /// <returns>
        /// The list primary role widget mapping.
        /// </returns>
        IEnumerable<PrimaryRoleWidgetMapping> GetPrimaryRoleWidgetMappings(int primaryRoleId, IUnitOfWork unitOfWork);

        /// <summary>
        /// Updates the specified user in the database.
        /// </summary>
        /// <param name="multiUsers">The user.</param>
        /// <param name="unitOfWork">The unit of work.</param> 
        /// <returns>
        /// number of uses affected
        /// </returns>               
        int UpdateUserMulti(MultiUsers multiUsers, IUnitOfWork unitOfWork);

        /// <summary>
        /// Gets filter values of user
        /// </summary>
        /// <param name="filterTypeId">The filter type id</param>
        /// <param name="unitOfWork">The unit of work</param>
        /// <returns>a collection</returns>
        ICollection<DropDownObject> GetUserFilterValues(int filterTypeId, IUnitOfWork unitOfWork);       

        /// <summary>
        /// Gets members of user profile
        /// </summary>
        /// <param name="profileId">The profile id</param>
        /// <param name="unitOfWork">The unit of work</param>
        /// <returns>The members of user profile</returns>
        MembersOfUserProfile GetMembersOfUserProfile(int profileId, IUnitOfWork unitOfWork);

        /// <summary>
        /// Assign user to profile
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="profileId">The profile id</param>
        /// <param name="unitOfWork">The unit of work</param>
        /// <returns>The integer value</returns>
        int AssignUserToProfile(int userId, int profileId, IUnitOfWork unitOfWork);

        /// <summary>
        /// Gets widget permissions primary role.
        /// </summary>
        /// <param name="primaryRoleId">The primary role id.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>The collection.</returns>
        Collection<WidgetPermission> GetWidgetPermissionByPrimaryRole(int primaryRoleId, IUnitOfWork unitOfWork);

        /// <summary>
        /// Verifies a windows user exists and creates a record in the User table if not.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <returns>The profile ID assigned to this user.</returns>
        int VerifyWindowsUser(string userName, IUnitOfWork unitOfWork);
    }
}