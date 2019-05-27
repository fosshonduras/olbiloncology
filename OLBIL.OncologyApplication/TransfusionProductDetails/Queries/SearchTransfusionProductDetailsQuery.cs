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

namespace OLBIL.OncologyApplication.TransfusionProductDetails.Queries
{
    public sealed class SearchTransfusionProductDetailsQuery : SearchBase, IRequest<ListModel<TransfusionProductDetailModel>>
    {
        public class Handler : SearchHandlerBase, IRequestHandler<SearchTransfusionProductDetailsQuery, ListModel<TransfusionProductDetailModel>>
        {
            public Handler(IOncologyContext context, IMapper mapper) : base(context, mapper) { }

            public async Task<ListModel<TransfusionProductDetailModel>> Handle(SearchTransfusionProductDetailsQuery request, CancellationToken cancellationToken)
            {
                Expression<Func<TransfusionProductDetail, bool>> predicate = i =>  EF.Functions.ILike(i.UnitNumber, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.Component, $"%{request.SearchTerm}%")
                    || EF.Functions.ILike(i.Responsible, $"%{request.SearchTerm}%")
                    ;
                var defaultSort = BuildSortList<TransfusionProductDetail>(i => i.TransfusionProductDetailId);

                return await RetrieveSearchResults<TransfusionProductDetail, TransfusionProductDetailModel>(predicate, defaultSort, request, cancellationToken);
            }
        }
    }
}
