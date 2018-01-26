using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Dapper;
using Tieptuyen.Components.Core.Api.BusinessObjects;
using Tieptuyen.Components.Core.Api.DataAccess.SqlServer;
using Tieptuyen.Components.Core.Api.Util;

namespace Tieptuyen.Components.Core.Api.DataAccess.Repository
{
    /// <summary>
    /// The repository for handling data-access operations relating to user access control.
    /// </summary>
    public class AccessControlRepository : KDataRepository, IAccessControlRepository
    {
        /// <inheritdoc/>
        public UserSettings SelectUserSettings(string userName, string password)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                return this.SelectUserSettings(userName, password, unitOfWork);
            }
        }

        /// <inheritdoc/>
        public UserSettings SelectUserSettings(string userName, string password, IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null && !string.IsNullOrEmpty(userName))
            {
                const string StoredProcedure = "cmn.GetUserSettings";
                CommandType commandType = CommandType.StoredProcedure;
                var parameters = new { UserName = userName, Password = password };
                using (var results = unitOfWork.QueryMultiple(StoredProcedure, parameters, commandType: commandType))
                {
                    UserSettings userSettings = results.Read<UserSettings>().FirstOrDefault();
                    //results.Read().Each(
                    //    x =>
                    //    {
                    //        var descriptionTranslationId = x.DescriptionTranslationID;

                    //        if (x.WidgetID == 31 && userSettings.PrimaryRoleId == 2) // widget data manager and primary role is planning only
                    //        {
                    //            descriptionTranslationId += "_PlanningOnly";
                    //        }

                    //        Widget widget = new Widget()
                    //        {
                    //            WidgetId = x.WidgetID,
                    //            Type = x.Type,
                    //            TitleTxId = x.TitleTxID,
                    //            TitleDefaultText = x.TitleDefaultText,
                    //            DescriptionTranslationId = descriptionTranslationId,
                    //            Obj = x.Obj,
                    //            Order = x.Order,
                    //            Created = x.Created,
                    //            LastUpdated = x.LastUpdated,
                    //            PermissionLevel = (PermissionLevel)x.PermissionLevel
                    //        };

                    //        userSettings.Widgets.Add(widget);
                    //    });
                    return userSettings;
                }
            }
            else
            {
                if (unitOfWork == null)
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
                else
                    throw new ArgumentException("The username must be specified.", "userName");
            }
        }

        /// <inheritdoc/>
        public void UpdateUserSettings(UserSettings userSettings, UserSettingsUpdateOptions options)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                this.UpdateUserSettings(userSettings, options, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public void UpdateUserSettings(UserSettings userSettings, UserSettingsUpdateOptions options, IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null && userSettings != null)
            {
                const string StoredProcedure = "cmn.UpdateUserSettings";
                SqlDynamicParameters parameters = new SqlDynamicParameters(new
                {
                    UserID = userSettings.Id,
                    PreferredLanguage = userSettings.PreferredLanguage,
                    Options = options
                });                

                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            else
            {
                if (unitOfWork == null)
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
                else
                    throw new ArgumentNullException("userSettings", "The user settings cannot be null.");
            }
        }               

        /// <inheritdoc/>
        public ICollection<Profile> SelectProfiles()
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                return this.SelectProfiles(unitOfWork);
            }
        }

        /// <inheritdoc/>
        public ICollection<Profile> SelectProfiles(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                const string StoredProcedure = "cmn.GetProfiles";
                using (var result = unitOfWork.QueryMultiple(StoredProcedure, commandType: CommandType.StoredProcedure))
                {
                    ICollection<Profile> profiles = result.Read<Profile>().ToCollection();
                    
                    result.Read<WidgetPermission>().Each(x => profiles.Single(p => (p.Id == x.ProfileId || x.ProfileId == 0)).Permissions.Add(x));
                                                        
                    return profiles;
                }
            }
            
            throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
        }

        /// <inheritdoc/>
        public void Insert(Profile profile)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                this.Insert(profile, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public void Insert(Profile profile, IUnitOfWork unitOfWork)
        {
            if (profile != null && unitOfWork != null)
            {
                const string StoredProcedure = "cmn.InsertProfile";
                SqlDynamicParameters parameters = new SqlDynamicParameters(new
                {
                    Name = profile.Name
                });

                var permissionsParameter = profile.Permissions.Select(
                    x => new
                    {
                        ProfileID = x.ProfileId,
                        WidgetID = x.WidgetId,
                        PermissionLevel = x.PermissionLevel
                    });
                parameters.AddAsTable("Permissions", permissionsParameter);
                parameters.Add("ProfileID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
                try
                {
                    profile.Id = parameters.Get<int>("ProfileID");
                }
                catch (NullReferenceException)
                {
                    profile.Id = default(int);
                }
            }
            else
            {
                if (profile == null)
                    throw new ArgumentNullException("profile", "The profile cannot be null.");
                else
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }

        /// <inheritdoc/>
        public void Update(Profile profile, ProfileUpdateOptions options)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                this.Update(profile, options, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public void Update(Profile profile, ProfileUpdateOptions options, IUnitOfWork unitOfWork)
        {
            if (profile != null && unitOfWork != null)
            {
                const string StoredProcedure = "cmn.UpdateProfile";
                foreach (var permission in profile.Permissions)
                {
                    permission.CanRead = !permission.CanWrite ? false : true;                    
                }

                bool updatePermissions = (options & ProfileUpdateOptions.UpdatePermissions) == ProfileUpdateOptions.UpdatePermissions;
                SqlDynamicParameters parameters = new SqlDynamicParameters(
                    new
                    {
                        ProfileID = profile.Id,
                        Name = profile.Name,
                        UpdatePermissions = updatePermissions,
                        PrimaryRoleID = profile.PrimaryRoleID,
                        ManageAccountLocks = profile.CanManageAccountLocks
                    });               

                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            else
            {
                if (profile == null)
                    throw new ArgumentNullException("profile", "The profile cannot be null.");
                else
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }

        /// <inheritdoc/>
        public void Delete(Profile profile)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                this.Delete(profile, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public void Delete(Profile profile, IUnitOfWork unitOfWork)
        {
            if (profile != null && unitOfWork != null)
            {
                const string StoredProcedure = "cmn.DeleteProfile";
                var parameters = new { ProfileID = profile.Id };
                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            else
            {
                if (profile == null)
                    throw new ArgumentNullException("profile", "The profile cannot be null.");
                else
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }

        /// <inheritdoc/>
        public bool IsProfileEmpty(int profileId)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                return this.IsProfileEmpty(profileId, unitOfWork);
            }
        }

        /// <inheritdoc/>
        public bool IsProfileEmpty(int profileId, IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                const string Sql = "SELECT cmn.IsProfileEmpty(@ProfileID) AS IsEmpty";
                var parameters = new { ProfileID = profileId };
                dynamic result = unitOfWork.Query(Sql, parameters).Single();
                return result.IsEmpty;
            }
            else
                throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
        }

        /// <inheritdoc/>
        public ICollection<User> SelectUsers()
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                return this.SelectUsers(unitOfWork);
            }
        }

        /// <inheritdoc/>
        public ICollection<User> SelectUsers(IUnitOfWork unitOfWork)
        {
            return this.SelectUsers(null, null, unitOfWork).ToCollection();
        }

        /// <inheritdoc/>
        public User SelectUser(int userId)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                return this.SelectUser(userId, unitOfWork);
            }
        }

        /// <inheritdoc/>
        public User SelectUser(int userId, IUnitOfWork unitOfWork)
        {
            return this.SelectUsers(userId, null, unitOfWork).SingleOrDefault();
        }

        /// <inheritdoc/>
        public User SelectUser(string userName)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                return this.SelectUser(userName, unitOfWork);
            }
        }

        /// <inheritdoc/>
        public User SelectUser(string userName, IUnitOfWork unitOfWork)
        {
            if (!string.IsNullOrEmpty(userName))
                return this.SelectUsers(null, userName, unitOfWork).SingleOrDefault();
            else
                throw new ArgumentException("The username cannot be either null or empty.", "userName");
        }

        /// <inheritdoc/>
        public void Insert(User user)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                this.Insert(user, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public void Insert(User user, IUnitOfWork unitOfWork)
        {
            if (user != null && unitOfWork != null)
            {
                const string StoredProcedure = "cmn.AddUser";
                DynamicParameters parameters = new DynamicParameters(
                    new
                    {
                        ProfileID = user.ProfileId,
                        UserName = user.UserName,
                        FullName = user.FullName,
                        Password = user.EncryptedPassword,
                        PreferredLanguage = user.PreferredLanguage
                    });

                parameters.Add("UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("SiteReviewOrderID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("SearchResultsOrderID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
                try
                {
                    user.Id = parameters.Get<int>("UserID");
                }
                catch (NullReferenceException)
                {
                    user.Id = default(int);
                }
            }
            else
            {
                if (user == null)
                    throw new ArgumentNullException("user", "The user cannot be null.");
                else
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }

        /// <inheritdoc/>
        public void Update(User user)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                this.Update(user, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public void Update(User user, IUnitOfWork unitOfWork)
        {
            if (user != null && unitOfWork != null)
            {
                const string StoredProcedure = "cmn.UpdateUser";
                var parameters = new
                {
                    UserID = user.Id,
                    UserName = user.UserName,
                    ProfileID = user.ProfileId,
                    FullName = user.FullName,
                    PreferredLanguage = user.PreferredLanguage,
                    Locked = user.Locked,
                };

                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            else
            {
                if (user == null)
                    throw new ArgumentNullException("user", "The user cannot be null.");
                else
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }

        /// <inheritdoc/>
        public void Delete(User user)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                this.Delete(user, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public void Delete(User user, IUnitOfWork unitOfWork)
        {
            if (user != null && unitOfWork != null)
            {
                const string StoredProcedure = "cmn.DeleteUser";
                var parameters = new
                {
                    UserID = user.Id
                };

                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            else
            {
                if (user == null)
                    throw new ArgumentNullException("user", "The user cannot be null.");
                else
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }

        /// <inheritdoc/>
        public void ResetPassword(PasswordReset passwordReset)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                this.ResetPassword(passwordReset, unitOfWork);
                unitOfWork.Commit();
            }
        }

        /// <inheritdoc/>
        public void ResetPassword(PasswordReset passwordReset, IUnitOfWork unitOfWork)
        {
            if (passwordReset != null && unitOfWork != null)
            {
                const string StoredProcedure = "cmn.ResetPassword";
                var parameters = new
                {
                    UserName = passwordReset.UserName,
                    Password = passwordReset.EncryptedPassword,
                    IsPreEncrypted = true
                };

                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            else
            {
                if (passwordReset == null)
                    throw new ArgumentNullException("passwordReset", "The password reset request cannot be null.");
                else
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }

        /// <summary>
        /// Get the password stored for the user that has the given user name
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns>The password</returns>
        public string GetPassword(string userName)
        {
            const string Statement = "SELECT Password FROM cmn.[User] WHERE UserName = @UserName";

            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                return unitOfWork.Query<string>(
                        Statement,
                        new { UserName = userName },
                        commandType: CommandType.Text).Single();
            }
        }

        /// <inheritdoc/>
        public string GetPreferredLanguage(string userName)
        {
            using (IUnitOfWork unitOfWork = this.BeginWork())
            {
                const string StoredProcedure = "cmn.GetUserPreferredLanguage";
                var parameter = new { UserName = userName };
                return
                    unitOfWork.Query(StoredProcedure, parameter, commandType: CommandType.StoredProcedure)
                        .Single()
                        .PreferredLanguage;
            }
        }
      

        /// <inheritdoc/>
        public IEnumerable<PrimaryRole> GetAllRrimaryRoles(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }

            const string StoredProcedure = "cmn.GetListPrimaryRoles";
            return unitOfWork.Query<PrimaryRole>(StoredProcedure, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc/>
        public IEnumerable<PrimaryRoleWidgetMapping> GetPrimaryRoleWidgetMappings(int primaryRoleId, IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }

            const string StoredProcedure = "cmn.GetPrimaryRoleWidgetMappings";
            var parameter = new { PrimaryRoleID = primaryRoleId };
            return unitOfWork.Query<PrimaryRoleWidgetMapping>(StoredProcedure, parameter, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc/>
        public int UpdateUserMulti(MultiUsers multiUsers, IUnitOfWork unitOfWork)
        {
            if (multiUsers != null && unitOfWork != null)
            {
                const string StoredProcedure = "cmn.UpdateMultiUser";
                var parameters = new SqlDynamicParameters(
                new
                {
                    ProfileID = multiUsers.ProfileId,
                    PreferredLanguage = multiUsers.PreferredLanguage,
                });

                parameters.AddAsTable("UserIDs", multiUsers.Ids.AsEntities());
                return unitOfWork.Query<int>(StoredProcedure, parameters, commandType: CommandType.StoredProcedure).First();
            }
            else
            {
                if (multiUsers == null)
                    throw new ArgumentNullException("multiUsers", "The user cannot be null.");
                else
                    throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }

        /// <inheritdoc/>
        public void MultipleUpdate(int[] profileIds, Profile profile, IUnitOfWork unitOfWork)
        {
            if (profileIds == null)
            {
                throw new ArgumentNullException("profileIds", "Profile ID can not be null.");
            }

            if (profile == null)
            {
                throw new ArgumentNullException("profile", "Profile can not be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "Unit Of Work can not be null.");
            }

            const string StoredProcedure = "cmn.UpdateProfile";
            foreach (var permission in profile.Permissions)
            {
                permission.CanRead = !permission.CanWrite ? false : true;               
            }

            var notesPermission = profile.Permissions.FirstOrDefault(x => x.WidgetId == (int)WidgetId.NotesWidgetId);
            var hasNotesPermission = notesPermission != null && notesPermission.CanRead;

            foreach (var item in profileIds)
            {
                SqlDynamicParameters parameters = new SqlDynamicParameters(
                    new
                    {
                        ProfileID = item,
                        UpdatePermissions = true,
                        PrimaryRoleID = profile.PrimaryRoleID
                    });
                var permissionsParameter = profile.Permissions.Where(x => x.IsChecked != null).Select(
                    x => new
                    {
                        ProfileID = item,
                        WidgetID = x.WidgetId,
                        PermissionLevel = x.PermissionLevel
                    });
               
                parameters.Add("Name", null);
                unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        /// <inheritdoc/>
        public ICollection<DropDownObject> GetUserFilterValues(int filterTypeId, IUnitOfWork unitOfWork)
        {
            this.ValidateUnitOfWork(unitOfWork);
            const string StoredProcedure = "prc.GetUserFilterValue";
            SqlDynamicParameters parameters = new SqlDynamicParameters(
                new
                {
                    TypeID = filterTypeId
                });
            return unitOfWork.Query<DropDownObject>(StoredProcedure, parameters, commandType: CommandType.StoredProcedure).ToCollection();
        }              

        /// <inheritdoc/>
        public void Delete(int id, IUnitOfWork unitOfWork)
        {
            this.ValidateUnitOfWork(unitOfWork);

            var parameters = new DynamicParameters(
                new
                {
                    @Id = id,
                });

            const string StoredProcedure = "prc.DeleteUserSearchSave";
            unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc/>
        public MembersOfUserProfile GetMembersOfUserProfile(int profileId, IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "The unit Of Work cannot be null.");
            }

            const string StoredProcedure = "cmn.GetUsersByProfile";
            var parameters = new SqlDynamicParameters();
            parameters.Add("ProfileID", profileId);

            using (var result = unitOfWork.QueryMultiple(StoredProcedure, parameters, commandType: CommandType.StoredProcedure))
            {
                IList<User> usersAssigned = result.Read<User>().ToList();
                IList<User> usersUnAssigned = result.Read<User>().ToList();
                return new MembersOfUserProfile()
                {
                    UsersAssigned = usersAssigned,
                    UsersUnAssigned = usersUnAssigned
                };
            }
        }

        /// <inheritdoc/>
        public int AssignUserToProfile(int userId, int profileId, IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "The unit Of Work cannot be null.");
            }

            const string StoredProcedure = "cmn.UpdateUser";
            var parameters = new SqlDynamicParameters();
            parameters.Add("UserID", userId);
            parameters.Add("ProfileID", profileId);
            int result = unitOfWork.Execute(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <inheritdoc/>
        public Collection<WidgetPermission> GetWidgetPermissionByPrimaryRole(int primaryRoleId, IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "The unit Of Work cannot be null.");
            }

            const string StoredProcedure = "cmn.GetWidgetPermissionByPrimaryRole";
            var parameters = new SqlDynamicParameters();
            parameters.Add("PrimaryRoleId", primaryRoleId);
            using (var result = unitOfWork.QueryMultiple(StoredProcedure, parameters, commandType: CommandType.StoredProcedure))
            {
                var permissions = result.Read<WidgetPermission>().ToCollection();
                return permissions;
            }
        }

        /// <inheritdoc/>
        public int VerifyWindowsUser(string userName, IUnitOfWork unitOfWork)
        {
            this.ValidateUnitOfWork(unitOfWork);

            const string StoredProcedure = "cmn.VerifyUser";
            var parameters = new SqlDynamicParameters();
            parameters.Add("UserName", userName);
            var result = unitOfWork.Query<int>(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            unitOfWork.Commit();

            return result.First();
        }        

        private IEnumerable<User> SelectUsers(int? userId, string userName, IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                const string StoredProcedure = "cmn.GetUsers";
                var parameters = new
                {
                    UserID = userId,
                    UserName = userName
                };

                return unitOfWork.Query<User>(StoredProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
            else
                throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
        }

        private void ValidateUnitOfWork(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "The unit of work cannot be null.");
            }
        }        
    }
}
