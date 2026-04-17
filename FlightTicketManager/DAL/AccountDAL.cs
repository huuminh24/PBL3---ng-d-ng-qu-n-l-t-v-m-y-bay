using System.Linq;
using FlightTicketManager.Models;

namespace FlightTicketManager.DAL
{
    public class AccountDAL
    {
        public Account? GetAccountByUsername(string username)
        {
            using (var db = new FlightTicketManagerDbContext())
            {
                return db.Accounts.FirstOrDefault(a => a.Username == username);
            }
        }

        public bool AddAccountWithProfile(Account account, Profile profile)
        {
            using (var db = new FlightTicketManagerDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Accounts.Add(account);
                        db.SaveChanges(); // Lấy AccountId mới generated

                        profile.AccountId = account.AccountId;
                        db.Profiles.Add(profile);
                        db.SaveChanges();

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
    }
}
