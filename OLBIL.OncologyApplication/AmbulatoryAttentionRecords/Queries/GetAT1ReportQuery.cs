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
            private readonly IDateTimeCalculationsDomainService _dateTimeCalculationsDomainService;

            public GetAT1ReportQqueryHandler(IOncologyContext context, IMapper mapper,
                IDateTimeProvider datetimeProvider, IDateTimeCalculationsDomainService dateTimeCalculationsDomainService) : base(context, mapper) {
                _datetimeProvider = datetimeProvider;
                _dateTimeCalculationsDomainService = dateTimeCalculationsDomainService;
            }

            public async Task<ListModel<AT1ReportItemDTO>> Handle(GetAT1ReportQuery request, CancellationToken cancellationToken)
            {
                FilterSpec healthProfessionalFilter = null;
                FilterSpec oncologyPatientFilter = null;
                FilterSpec dateFilter = null;
                FilterSpec diagnosisFilter = null;

                if (request.Filters != null)
                {
                    request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.HealthProfessionalId), out healthProfessionalFilter, caseSensitive: false);
                    request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.OncologyPatientId), out oncologyPatientFilter, caseSensitive: false);
                    request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.Date), out dateFilter, caseSensitive: false);
                    request.Filters.TryGetValue(nameof(AmbulatoryAttentionRecord.DiagnosisId), out diagnosisFilter, caseSensitive: false);
                }

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
                    var ageTuple = _dateTimeCalculationsDomainService.CalculateDifference(item.Birthdate.Value, currentDate);
                    item.AgeInYears = ageTuple.Years;
                    item.AgeInMonthsOverYears = ageTuple.Months;
                    item.AgeInDaysOverMonths = ageTuple.Days;
                });
                return Task.FromResult(bareResults);
            }
        }
    }
}
