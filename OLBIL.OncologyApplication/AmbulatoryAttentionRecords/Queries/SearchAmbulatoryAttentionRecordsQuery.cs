using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public sealed class SearchAmbulatoryAttentionRecordsQuery : SearchBase, IRequest<ListModel<AmbulatoryAttentionRecordModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchAmbulatoryAttentionRecordsQuery, ListModel<AmbulatoryAttentionRecordModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<AmbulatoryAttentionRecordModel>> Handle(SearchAmbulatoryAttentionRecordsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<AmbulatoryAttentionRecord, bool>> predicate = i =>
                    EF.Functions.ILike(i.Diagnosis.CompleteDescriptor, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.HealthProfessional.Person.FirstName, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.HealthProfessional.Person.MiddleName, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.HealthProfessional.Person.LastName, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.HealthProfessional.Person.AdditionalLastName, $"%{request.SearchTerm}%")
                    
                    || EF.Functions.ILike(i.OncologyPatient.Person.FirstName, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.OncologyPatient.Person.MiddleName, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.OncologyPatient.Person.LastName, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.OncologyPatient.Person.AdditionalLastName, $"%{request.SearchTerm}%")

                    || EF.Functions.ILike(i.Diagnosis.CompleteDescriptor, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.Diagnosis.CompleteDescriptor, $"%{request.SearchTerm}%");

                return await RetrieveSearchResults<AmbulatoryAttentionRecord, AmbulatoryAttentionRecordModel>(predicate, request, cancellationToken);
            }
        }
    }
}
