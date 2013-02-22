using System;
using System.Linq;
using System.Web.Security;
using System.Security.Cryptography;
using sandBoxDEControl.Database;

namespace sandBoxDEControl.Account
{
    public class UserRepository
    {
        private static String   CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
        private static String   CreatePasswordHash(String pwd, String salt)
        {
            String saltAndPwd = String.Concat(pwd, salt);
            String hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "md5");
            return hashedPwd;
        }

        public MembershipUser   CreateUser(String login, String password, String userrole)
        {
            using (RoleMembershipDataContext db = new RoleMembershipDataContext())
            {

                User user = new User
                {
                    Login = login,
                    Password = password,
                    PasswordSalt = CreateSalt(),
                    CreatedDate = DateTime.Now,
                    LastLoginDate = DateTime.Now
                };
                user.Password = CreatePasswordHash(password, user.PasswordSalt);
                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();
                return GetUser(login);
            }
        }
        public MembershipUser   GetUser(String login)
        {
            using (RoleMembershipDataContext db = new RoleMembershipDataContext())
            {
                var result = from u in db.Users where (u.Login == login) select u;

                if (result.Count() == 0)
                {
                    return null;
                }
                
                User dbuser = result.FirstOrDefault();
                MembershipUser user = new MembershipUser("CustomMembershipProvider",
                                                         dbuser.Login,
                                                         dbuser.UserId,
                                                         String.Empty,
                                                         String.Empty,
                                                         String.Empty,
                                                         true,
                                                         false,
                                                         dbuser.CreatedDate,
                                                         DateTime.Now,
                                                         DateTime.Now,
                                                         DateTime.Now,
                                                         DateTime.Now);

                return user;
            }
        }
        public MembershipUser   GetUser(Int32 id)
        {
            using (RoleMembershipDataContext db = new RoleMembershipDataContext())
            {
                var result = from u in db.Users where (u.UserId == id) select u;

                if (result.Count() == 0)
                {
                    return null;
                }

                User dbuser = result.FirstOrDefault();
                MembershipUser user = new MembershipUser("CustomMembershipProvider",
                                                         dbuser.Login,
                                                         dbuser.UserId,
                                                         String.Empty,
                                                         String.Empty,
                                                         String.Empty,
                                                         true,
                                                         false,
                                                         dbuser.CreatedDate,
                                                         DateTime.Now,
                                                         DateTime.Now,
                                                         DateTime.Now,
                                                         DateTime.Now);

                return user;
            }
        }
        public Boolean          ValidateUser(String login, String password)
        {
            using (RoleMembershipDataContext db = new RoleMembershipDataContext())
            {
                var dbuser = db.Users.FirstOrDefault(x => x.Login == login);
                return dbuser != null && dbuser.Password == CreatePasswordHash(password, dbuser.PasswordSalt);
            }
        }

        public object           GetUserId(string username)
        {
            /*UserRepository user = new UserRepository();
            MembershipUser usr = user.GetUser(username);*/
            MembershipUser usr = GetUser(username);
            return usr.ProviderUserKey;
        }

        public IQueryable GetAllUsers()
        {
            using (RoleMembershipDataContext db = new RoleMembershipDataContext())
            {
                var users = from p in db.Users
                            orderby p.Login
                            select p;
                return users;
            }
        }
    }//end class UserRepository
}//end namespace