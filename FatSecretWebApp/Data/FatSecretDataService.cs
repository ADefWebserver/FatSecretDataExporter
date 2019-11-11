using FatSecretWebApp.Models.DBContext;
using FatSecretWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System;

namespace FatSecretWebApp.Data
{
    public class FatSecretDataService
    {
        private readonly FatSecretDataExporterContext _context;
        public FatSecretDataService(FatSecretDataExporterContext context)
        {
            _context = context;
        }

        public Task<FatSecretToken> GetFatSecretTokenAsync(string GoogleIdentifier)
        {
            FatSecretToken objFatSecretToken = new FatSecretToken();

            objFatSecretToken = (from FatSecretToken in _context.FatSecretToken
                                 where FatSecretToken.GoogleUserName == GoogleIdentifier
                                 select FatSecretToken).FirstOrDefault();

            return Task.FromResult(objFatSecretToken);
        }

        public Task<bool> CreateFatSecretTokenAsync(FatSecretToken objFatSecretToken)
        {
            objFatSecretToken.TimeStamp = DateTime.Now;

            _context.FatSecretToken.Add(objFatSecretToken);
            _context.SaveChanges();

            return Task.FromResult(true);
        }

        public Task<bool> DeleteFatSecretTokenAsync(FatSecretToken objFatSecretToken)
        {
            var ExistingFatSecretToken =
                _context.FatSecretToken
                .Where(x => x.Id == objFatSecretToken.Id)
                .FirstOrDefault();

            if (ExistingFatSecretToken != null)
            {
                _context.FatSecretToken.Remove(ExistingFatSecretToken);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        public Task<bool> CreateFatSecretLogAsync(FatSecretLog objFatSecretLog)
        {
            objFatSecretLog.TimeStamp = DateTime.Now;

            _context.FatSecretLog.Add(objFatSecretLog);
            _context.SaveChanges();

            return Task.FromResult(true);
        }
    }
}