using System;
using Frapper.Repository.Movies.Command;
using Frapper.Repository.UserMaster.Command;

namespace Frapper.Repository
{
    public class UnitOfWorkEntityFramework : IUnitOfWorkEntityFramework
    {
        private readonly FrapperDbContext _context;
        public IUserMasterCommand UserMasterCommand { get; }
        public IAssignedRolesCommand AssignedRolesCommand { get; }
        public IUserTokensCommand UserTokensCommand { get; }
        public IMoviesCommand MoviesCommand { get; }
        public UnitOfWorkEntityFramework(FrapperDbContext context)
        {
            _context = context;
            AssignedRolesCommand = new AssignedRolesCommand(_context);
            UserMasterCommand = new UserMasterCommand(_context);
            UserTokensCommand = new UserTokensCommand(_context);
            MoviesCommand = new MoviesCommand(_context);
        }

        public bool Commit()
        {
            bool returnValue = true;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = false;
                    transaction.Rollback();
                }
                finally
                {
                    transaction.Dispose();
                }
            }

            return returnValue;
        }

        public void Dispose()
        {
            Dispose(true);
        }


        private bool _disposedValue = false; // To detect redundant calls  

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                _context.Dispose();
            }

            _disposedValue = true;
        }
    }
}