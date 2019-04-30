using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.OncologyApplication.DTOs;
using OLBIL.OncologyApplication.Infrastructure;
using OLBIL.OncologyApplication.Interfaces;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyDomain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Queries
{
    public class GetAT1ReportQuery : SearchBase, IRequest<ListModel<AT1ReportItemDTO>>
    {
        public class GetAT1ReportQqueryHandler : SearchHandlerBase, IRequestHandler<GetAT1ReportQuery, ListModel<AT1ReportItemDTO>>
        {
            public GetAT1ReportQqueryHandler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AT1ReportItemDTO>> Handle(GetAT1ReportQuery request, CancellationToken cancellationToken)
            {
                FilterSpec healthProfessionalFilter = null;
                request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.HealthProfessionalId).ToLowerInvariant(), out healthProfessionalFilter);
                
                FilterSpec oncologyPatientFilter = null;
                request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.OncologyPatientId).ToLowerInvariant(), out oncologyPatientFilter);

                FilterSpec dateFilter = null;
                request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.Date).ToLowerInvariant(), out dateFilter);

                FilterSpec diagnosisFilter = null;
                request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.DiagnosisId).ToLowerInvariant(), out diagnosisFilter);

                Expression<Func<AmbulatoryAttentionRecord, bool>> predicate = i =>
                    (healthProfessionalFilter == null || i.HealthProfessionalId == int.Parse(healthProfessionalFilter.SearchTerm))
                    && (oncologyPatientFilter == null || i.OncologyPatientId == int.Parse(oncologyPatientFilter.SearchTerm))
                    //&& (dateFilter == null || EF.Functions.)
                    //&& (dateFilter == null || i.Date == DateTime.Parse(dateFilter.SearchTerm))
                    && (diagnosisFilter == null || i.DiagnosisId == int.Parse(diagnosisFilter.SearchTerm));

                return await RetrieveSearchResults<AmbulatoryAttentionRecord, AT1ReportItemDTO>(predicate, request, cancellationToken);
            }
        }
    }
}
