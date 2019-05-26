using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OLBIL.Common;
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
            private readonly IDateTimeProvider _datetimeProvider;

            public GetAT1ReportQqueryHandler(IOncologyContext context, IMapper mapper, IDateTimeProvider datetimeProvider) : base(context, mapper) {
                _datetimeProvider = datetimeProvider;
            }

            public async Task<ListModel<AT1ReportItemDTO>> Handle(GetAT1ReportQuery request, CancellationToken cancellationToken)
            {
                FilterSpec healthProfessionalFilter = null;
                request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.HealthProfessionalId), out healthProfessionalFilter, caseSensitive: false);
                
                FilterSpec oncologyPatientFilter = null;
                request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.OncologyPatientId), out oncologyPatientFilter, caseSensitive: false);

                FilterSpec dateFilter = null;
                request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.Date), out dateFilter, caseSensitive: false);

                FilterSpec diagnosisFilter = null;
                request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.DiagnosisId), out diagnosisFilter, caseSensitive: false);
                
                int dateValueFilter = dateFilter == null? DateTime.Now.DayOfYear : DateTime.Parse(dateFilter.SearchTerm).DayOfYear;
                Expression<Func<AmbulatoryAttentionRecord, bool>> predicate = i =>
                            (healthProfessionalFilter == null || i.HealthProfessionalId == int.Parse(healthProfessionalFilter.SearchTerm))
                        && (oncologyPatientFilter == null || i.OncologyPatientId == int.Parse(oncologyPatientFilter.SearchTerm))
                        && (dateFilter == null || i.Date.DayOfYear == dateValueFilter)
                        && (diagnosisFilter == null || i.DiagnosisId == int.Parse(diagnosisFilter.SearchTerm));
                var defaultSort = BuildSortList<AmbulatoryAttentionRecord>(i => i.AmbulatoryAttentionRecordId);

                var bareResults = await RetrieveSearchResults<AmbulatoryAttentionRecord, AT1ReportItemDTO>(predicate, defaultSort, request, cancellationToken);
                return await TapWithAgeValues(bareResults);
            }

            private Task<ListModel<AT1ReportItemDTO>> TapWithAgeValues(ListModel<AT1ReportItemDTO> bareResults)
            {
                var currentDate = _datetimeProvider.Now;
                bareResults.Items.ForEach(item =>
                {
                    if (item.Birthdate == null) return;
                    var ageTuple = _datetimeProvider.CalculateDifference(item.Birthdate.Value, currentDate);
                    item.AgeInYears = ageTuple.Item1;
                    item.AgeInMonthsOverYears = ageTuple.Item2;
                    item.AgeInDaysOverMonths = ageTuple.Item3;
                });
                return Task.FromResult(bareResults);
            }
        }
    }
}
