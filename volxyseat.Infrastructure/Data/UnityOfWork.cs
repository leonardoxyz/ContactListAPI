using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using volxyseat.Domain.Core.Data;

namespace volxyseat.Infrastructure.Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly DataContext _dataContext;

        public UnityOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        //aqui entraria o dispose, caso necessário.

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}
