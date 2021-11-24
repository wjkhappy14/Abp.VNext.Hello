using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging;

namespace Abp.VNext.Hello
{
    public class AuditLoggingService : ApplicationService
    {
        IAuditLogRepository AuditLogRepository { get; }

        public AuditLoggingService(IAuditLogRepository auditLogRepository)
        {
            AuditLogRepository = auditLogRepository;
        }

        //public Task<List<AuditLog>> GetListAsync(string sorting = null, int maxResultCount = 50, int skipCount = 0, DateTime? startTime = null, DateTime? endTime = null, string httpMethod = null, string url = null, Guid? userId = null, string userName = null, string applicationName = null, string correlationId = null, int? maxExecutionDuration = null, int? minExecutionDuration = null, bool? hasException = null, HttpStatusCode? httpStatusCode = null, bool includeDetails = false, CancellationToken cancellationToken = default)
        //{
        //    return AuditLogRepository.GetListAsync(sorting, maxResultCount, skipCount = 0, startTime, endTime, httpMethod, url, userId, userName, applicationName, correlationId, maxExecutionDuration, minExecutionDuration, hasException, true, includeDetails);
        //}

        public Task<long> GetCountAsync(DateTime? startTime = null, DateTime? endTime = null, string httpMethod = null, string url = null, string userName = null, string applicationName = null, string correlationId = null, int? maxExecutionDuration = null, int? minExecutionDuration = null, bool? hasException = null, HttpStatusCode? httpStatusCode = null)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<DateTime, double>> GetAverageExecutionDurationPerDayAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }


        public Task<List<EntityChange>> GetEntityChangeListAsync(string sorting = null, int maxResultCount = 50, int skipCount = 0, Guid? auditLogId = null, DateTime? startTime = null, DateTime? endTime = null, EntityChangeType? changeType = null, string entityId = null, string entityTypeFullName = null, bool includeDetails = false)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetEntityChangeCountAsync(Guid? auditLogId = null, DateTime? startTime = null, DateTime? endTime = null, EntityChangeType? changeType = null, string entityId = null, string entityTypeFullName = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<EntityChangeWithUsername>> GetEntityChangesWithUsernameAsync(string entityId, string entityTypeFullName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<long> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<AuditLog>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false)
        {
            throw new NotImplementedException();
        }
    }
}
